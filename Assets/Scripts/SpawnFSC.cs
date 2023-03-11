using UnityEngine;
using UnityEngine.Serialization;

public class SpawnFSC : MonoBehaviour
{
    [FormerlySerializedAs("BarPrefab")]
    [SerializeField]
    Transform mBarPrefab;

    public void Spawn()
    {
        int index = Random.Range(0, GameContext.Instance.Differences.Count);
        GameContext.Instance.HasDifference = GameContext.Instance.Differences[index];
        GameContext.Instance.Differences.RemoveAt(index);

        index = Random.Range(0, 25);
        GameContext.Instance.NextIndex = 0;//Not need it but just to make sure

        BarInfo[] bars = new BarInfo[25];
        for (int i = 0; i < 25; i++)
        {
            if (index == i && GameContext.Instance.HasDifference)
                bars[i] = new BarInfo(Color.red);
            else
                bars[i] = new BarInfo(Color.white, 0.0f);
        }
        GameContext.Instance.BarsToSpawn = bars;

        for (int i = 0; i < 25; i++)
            Instantiate(mBarPrefab, transform);
        
        //Enable Player Input
    }

}
