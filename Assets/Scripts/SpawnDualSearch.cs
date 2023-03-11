using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnDualSearch : MonoBehaviour
{
    [FormerlySerializedAs("BarPrefab")]
    [SerializeField]
    Transform mLetterPrefab;

    private static float[] Rotations = { 45.0f, 90.0f, 135.0f, 180.0f, 225.0f }; 
    public void Spawn()
    {
        int index = Random.Range(0, GameContext.Instance.Differences.Count);
        GameContext.Instance.HasDifference = GameContext.Instance.Differences[index];
        GameContext.Instance.Differences.RemoveAt(index);

        index = Random.Range(0, 5);
        GameContext.Instance.NextIndex = 0;//Not need it but just to make sure

        BarInfo[] letters = new BarInfo[25];
        for (int i = 0; i < 5; i++)
        {
            if (index == i && GameContext.Instance.HasDifference)
                letters[i] = new BarInfo(Color.white, 0.0f);
            else
            {
                float rot = Rotations[Random.Range(0, Rotations.Length)];
                letters[i] = new BarInfo(rot);
            }
        }
        GameContext.Instance.BarsToSpawn = letters;

        for (int i = 0; i < 5; i++)
            Instantiate(mLetterPrefab, transform);
    }

}
