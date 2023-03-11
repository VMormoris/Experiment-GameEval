using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputScript : MonoBehaviour
{
    bool mHasRegister = false;
    float mStart = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    void Update()
    {
        float now = Time.realtimeSinceStartup;
        float epoch = now - mStart;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.P))
        {
            if(GameContext.Instance.HasDifference)
                AdvanceCounter();
            RegisterRT(epoch);
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.I))
        {
            if (!GameContext.Instance.HasDifference)
                AdvanceCounter();
            RegisterRT(epoch);
        }
        else
            return;
    }

    void AdvanceCounter()
    {
        if (GameContext.Instance.CurrentSearch == SearchType.Color)
            GameContext.Instance.ColorAccuracy++;
        else if (GameContext.Instance.CurrentSearch == SearchType.Orientation)
            GameContext.Instance.OrientationAccuracy++;
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction)
            GameContext.Instance.ConjuctionAccuracy++;
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
            GameContext.Instance.DualAccuracy++;
    }

    void RegisterRT(float rt)
    {
        if (GameContext.Instance.CurrentSearch == SearchType.Color)
            GameContext.Instance.ColorReactions.Add(rt);
        else if (GameContext.Instance.CurrentSearch == SearchType.Orientation)
            GameContext.Instance.OrientationReactions.Add(rt);
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction)
            GameContext.Instance.ConjuctionReactions.Add(rt);
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
            GameContext.Instance.DualReactions.Add(rt);
        mHasRegister = true;
        gameObject.SetActive(false);
    }

    public void Stop()
    {
        if (mHasRegister)
            return;
        
        if (GameContext.Instance.CurrentSearch == SearchType.Color)
            GameContext.Instance.ColorReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Orientation)
            GameContext.Instance.OrientationReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction)
            GameContext.Instance.ConjuctionReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
            GameContext.Instance.DualReactions.Add(-1.0f);
    }

    public void Restart()
    {
        mHasRegister = false;
        mStart = Time.realtimeSinceStartup;
    }

}
