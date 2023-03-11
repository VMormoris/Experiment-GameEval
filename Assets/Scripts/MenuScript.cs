using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuScript : MonoBehaviour
{
    [FormerlySerializedAs("Color Search Spawner")]
    [SerializeField]
    private GameObject mSpawnColorSearch;
    [FormerlySerializedAs("Orientation Search Spawner")]
    [SerializeField]
    private GameObject mSpawnOrientationSearch;
    [FormerlySerializedAs("Conjuction Search Spawner")]
    [SerializeField]
    private GameObject mSpawnConjuctionSearch;
    [FormerlySerializedAs("Conjuction Search Spawner")]
    [SerializeField]
    private GameObject mSpawnDualSearch;

    [FormerlySerializedAs("Differences")]
    [SerializeField]
    private bool[] mDifferences = { true, false, true, false, true, false };

    // Start is called before the first frame update
    public void StartColorSearch()
    {
        DisableMenu();
        GameContext.Instance.CurrentSearch = SearchType.Color;
        GameContext.Instance.StartButton.SetActive(true);
        
        GameContext.Instance.Differences.Clear();
        for (int i = 0; i < mDifferences.Length; i++)
            GameContext.Instance.Differences.Add(mDifferences[i]);
    }

    public void StartOrientationSearch()
    {
        DisableMenu();
        GameContext.Instance.CurrentSearch = SearchType.Orientation;
        GameContext.Instance.StartButton.SetActive(true);

        GameContext.Instance.Differences.Clear();
        for (int i = 0; i < mDifferences.Length; i++)
            GameContext.Instance.Differences.Add(mDifferences[i]);
    }

    public void StartConjuctionSearch()
    {
        DisableMenu();
        GameContext.Instance.CurrentSearch = SearchType.Conjuction;
        GameContext.Instance.StartButton.SetActive(true);

        GameContext.Instance.Differences.Clear();
        for (int i = 0; i < mDifferences.Length; i++)
            GameContext.Instance.Differences.Add(mDifferences[i]);
    }

    public void StartDualSearch()
    {
        DisableMenu();
        GameContext.Instance.CurrentSearch = SearchType.Dual;
        GameContext.Instance.StartButton.SetActive(true);

        GameContext.Instance.Differences.Clear();
        for (int i = 0; i < mDifferences.Length; i++)
            GameContext.Instance.Differences.Add(mDifferences[i]);
    }

    public void StartExperiment()
    {
        if (GameContext.Instance.CurrentSearch == SearchType.Color)
            mSpawnColorSearch.GetComponent<SpawnFSC>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Orientation)
            mSpawnOrientationSearch.GetComponent<SpawnFSO>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction)
            mSpawnConjuctionSearch.GetComponent<SpawnConjuction>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
            mSpawnDualSearch.GetComponent<SpawnDualSearch>().Spawn();
        GameContext.Instance.StartButton.SetActive(false);
        GameContext.Instance.PlayerInput.gameObject.SetActive(true);

        GameContext.Instance.ColorReactions.Clear(); GameContext.Instance.ColorAccuracy = 0;
        GameContext.Instance.OrientationReactions.Clear(); GameContext.Instance.OrientationAccuracy = 0;
        GameContext.Instance.ConjuctionReactions.Clear(); GameContext.Instance.ConjuctionAccuracy = 0;
        GameContext.Instance.DualReactions.Clear(); GameContext.Instance.DualAccuracy = 0;

        PlayerInputScript script = GameContext.Instance.PlayerInput.GetComponent<PlayerInputScript>();
        if (script)
            script.Restart();
    }

    public void GoToNext()
    {
        if (GameContext.Instance.CurrentSearch == SearchType.Orientation)
            mSpawnOrientationSearch.GetComponent<SpawnFSO>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Color)
            mSpawnColorSearch.GetComponent<SpawnFSC>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction)
            mSpawnConjuctionSearch.GetComponent<SpawnConjuction>().Spawn();
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
            mSpawnDualSearch.GetComponent<SpawnDualSearch>().Spawn();
        GameContext.Instance.NextButton.SetActive(false);
        GameContext.Instance.PlayerInput.gameObject.SetActive(true);
        GameContext.Instance.PlayerInput.GetComponent<PlayerInputScript>().Restart();
    }

    public void AddPlayerNumber()
    {
        GameContext.Instance.Participant++;
        GameContext.Instance.PlayerNumber.GetComponent<TextMeshProUGUI>().text = GameContext.Instance.Participant.ToString();
    }

    public void SubtractPlayerNumber()
    {
        GameContext.Instance.Participant--;
        GameContext.Instance.PlayerNumber.GetComponent<TextMeshProUGUI>().text = GameContext.Instance.Participant.ToString();
    }

    private void DisableMenu()
    {
        GameContext.Instance.ButtonColorSearch.SetActive(false);
        GameContext.Instance.ButtonOrientationSearch.SetActive(false);
        GameContext.Instance.ButtonConjuctionSearch.SetActive(false);
        GameContext.Instance.ButtonDualSearch.SetActive(false);
    }

    public void Save()
    {
        const string format = "Accuracy: {0}\nReactions: {1}, {2}, {3}, {4}, {5}, {6}";
        SearchType curr = GameContext.Instance.CurrentSearch;
        if (curr == SearchType.Color)
        {
            List<float> results = GameContext.Instance.ColorReactions;
            string filepath = string.Format("Color-{0}.out", GameContext.Instance.Participant);
            File.WriteAllText(filepath, string.Format(format, GameContext.Instance.ColorAccuracy, results[0], results[1], results[2], results[3], results[4], results[5]));
            
        }
        else if(curr == SearchType.Orientation)
        {
            List<float> results = GameContext.Instance.OrientationReactions;
            string filepath = string.Format("Orientation-{0}.out", GameContext.Instance.Participant);
            File.WriteAllText(filepath, string.Format(format, GameContext.Instance.OrientationAccuracy, results[0], results[1], results[2], results[3], results[4], results[5]));
        }
        else if(curr == SearchType.Conjuction)
        {
            List<float> results = GameContext.Instance.ConjuctionReactions;
            string filepath = string.Format("Conjuction-{0}.out", GameContext.Instance.Participant);
            File.WriteAllText(filepath, string.Format(format, GameContext.Instance.ConjuctionAccuracy, results[0], results[1], results[2], results[3], results[4], results[5]));
        }
        else if(curr == SearchType.Dual)
        {
            List<float> results = GameContext.Instance.DualReactions;
            string filepath = string.Format("Dual-{0}.out", GameContext.Instance.Participant);
            File.WriteAllText(filepath, string.Format(format, GameContext.Instance.DualAccuracy, results[0], results[1], results[2], results[3], results[4], results[5]));
        }

        GameContext.Instance.SaveButton.SetActive(false);
        GameContext.Instance.UpButton.SetActive(false);
        GameContext.Instance.DownButton.SetActive(false);
        GameContext.Instance.PlayerNumber.SetActive(false);

        GameContext.Instance.ButtonColorSearch.SetActive(true);
        GameContext.Instance.ButtonOrientationSearch.SetActive(true);
        GameContext.Instance.ButtonConjuctionSearch.SetActive(true);
        GameContext.Instance.ButtonDualSearch.SetActive(true);
    }
}
