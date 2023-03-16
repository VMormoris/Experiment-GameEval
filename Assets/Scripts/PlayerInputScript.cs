using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputScript : MonoBehaviour
{
    bool mHasRegisterCentral = false;
    bool mHasRegisterPeriphery = false;

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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
        {
            if (GameContext.Instance.HasPeripheryDiff)
                AdvancePeripheryCounter();
            RegisterPeripheryRT(epoch);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.J))
        {
            if (!GameContext.Instance.HasPeripheryDiff)
                AdvancePeripheryCounter();
            RegisterPeripheryRT(epoch);
        }
    }

    private void RegisterPeripheryRT(float rt)
    {
        if (GameContext.Instance.CurrentSearch != SearchType.Dual)
            return;

        GameContext.Instance.PeripheryReactions.Add(rt);
        mHasRegisterPeriphery = true;
        if (mHasRegisterCentral)
            gameObject.SetActive(false);
    }

    private void AdvancePeripheryCounter()
    {
        if (GameContext.Instance.CurrentSearch != SearchType.Dual)
            return;
        GameContext.Instance.PeripheryAccuracy++;
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

        mHasRegisterCentral = true;
        if(mHasRegisterPeriphery)
            gameObject.SetActive(false);
    }

    public void Stop()
    {
        if (GameContext.Instance.CurrentSearch == SearchType.Color && !mHasRegisterCentral)
            GameContext.Instance.ColorReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Orientation && !mHasRegisterCentral)
            GameContext.Instance.OrientationReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Conjuction && !mHasRegisterCentral)
            GameContext.Instance.ConjuctionReactions.Add(-1.0f);
        else if (GameContext.Instance.CurrentSearch == SearchType.Dual)
        {
            if(!mHasRegisterCentral)
                GameContext.Instance.DualReactions.Add(-1.0f);
            if (!mHasRegisterPeriphery)
                GameContext.Instance.PeripheryReactions.Add(-1.0f);
        }
    }

    public void Restart()
    {
        mHasRegisterCentral = false;
        mHasRegisterPeriphery = false;
        mStart = Time.realtimeSinceStartup;
    }

}
