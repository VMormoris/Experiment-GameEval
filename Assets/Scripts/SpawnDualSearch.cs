using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnDualSearch : MonoBehaviour
{
    [FormerlySerializedAs("BarPrefab")]
    [SerializeField]
    Transform mLetterPrefab;

    [FormerlySerializedAs("PeripheryPrefab")]
    [SerializeField]
    Transform mPeriphery;

    private static float[] Rotations = { 45.0f, 90.0f, 135.0f, 180.0f, 225.0f }; 
    public void Spawn()
    {
        int index = Random.Range(0, GameContext.Instance.Differences.Count);
        GameContext.Instance.HasDifference = GameContext.Instance.Differences[index];
        GameContext.Instance.Differences.RemoveAt(index);

        index = Random.Range(0, 5);
        GameContext.Instance.NextIndex = 0;//Not need it but just to make sure

        LetterInfo[] letters = new LetterInfo[5];
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-1.85f, 1.85f);
            float y = Random.Range(-1.85f, 1.85f);
            Vector2 pos = new Vector2(x, y);
            float angle = Rotations[Random.Range(0, Rotations.Length)];
            if (index == i && GameContext.Instance.HasDifference)
                angle = 0.0f;

            for (int k = 0; k < 5; k++)//Iterate all previous letters to push them apart
            {
                for (int j = 0; j < i; j++)
                {
                    if (SquareDistance(pos, letters[j].Position) <= 0.25f)
                    {
                        Vector2 dir = (letters[j].Position - pos).normalized;
                        pos += dir * 1.0f;
                    }
                }
            }
            letters[i] = new LetterInfo(pos, angle);
        }

        GameContext.Instance.LettersToSpawn = letters;

        for (int i = 0; i < 5; i++)
            Instantiate(mLetterPrefab, transform);

        index = Random.Range(0, GameContext.Instance.Differences.Count);
        GameContext.Instance.HasPeripheryDiff = GameContext.Instance.PeripheryDifferences[index];
        GameContext.Instance.PeripheryDifferences.RemoveAt(index);
        float rot = Random.Range(0.0f, 360.0f);
        Instantiate(mPeriphery, new Vector3(0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, rot), transform);
    }

    float SquareDistance(Vector2 v0, Vector2 v1)
    {
        Vector2 temp = v0 - v1;
        return temp.x * temp.x + temp.y * temp.y;
    }

}
