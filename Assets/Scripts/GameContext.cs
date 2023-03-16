using System.Collections.Generic;
using UnityEngine;

struct BarInfo
{
    public Color color;
    public float Angle;

    public BarInfo(Color color)
    {
        this.color = color;
        Angle = 0.0f;
    }

    public BarInfo(float angle)
    {
        this.color = Color.white;
        Angle = angle;
    }

    public BarInfo(Color color, float angle)
    {
        this.color = color;
        Angle = angle;
    }
}

public struct LetterInfo
{
    public Vector2 Position;
    public float Angle;

    public LetterInfo(Vector2 pos, float angle)
    {
        Position = pos;
        Angle = angle;
    }
}

enum SearchType
{
    None,
    Color,
    Orientation,
    Conjuction,
    Dual
}

class GameContext : MonoBehaviour
{
    public GameObject Countdown;

    public GameObject ButtonColorSearch;
    public GameObject ButtonOrientationSearch;
    public GameObject ButtonConjuctionSearch;
    public GameObject ButtonDualSearch;
    public GameObject StartButton;
    public GameObject NextButton;

    public GameObject SaveButton;
    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject PlayerNumber;

    public Transform PlayerInput;

    public int Participant = 1;
    public int Iterations = 6;

    public int ColorAccuracy = 0;
    public int OrientationAccuracy = 0;
    public int ConjuctionAccuracy = 0;
    public int DualAccuracy = 0;
    public int PeripheryAccuracy = 0;

    public List<bool> Differences = new List<bool>();
    public List<bool> PeripheryDifferences = new List<bool>();

    public List<float> ColorReactions = new List<float>();
    public List<float> OrientationReactions = new List<float>();
    public List<float> ConjuctionReactions = new List<float>();
    public List<float> DualReactions = new List<float>();
    public List<float> PeripheryReactions = new List<float>();

    public bool HasDifference { set; get; }
    public bool HasPeripheryDiff { set; get; }
    public BarInfo[] BarsToSpawn { set; get; }
    public LetterInfo[] LettersToSpawn { set; get; }
    public int NextIndex { set; get; }
    public SearchType CurrentSearch { set; get; }

    private static GameContext sInstance = null;

    public void Awake()
    {
        CurrentSearch = SearchType.None;
        sInstance = this;
    }

    public static GameContext Instance { get { return sInstance; } }

}
