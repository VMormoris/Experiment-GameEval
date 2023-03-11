using TMPro;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    float mAccumulator = 10.0f;

    // Update is called once per frame
    void Update()
    {
        mAccumulator += Time.deltaTime;
        int time = (int)(3 - mAccumulator) + 1;

        TextMeshPro text = transform.GetComponentInChildren<TextMeshPro>();
        text.text = time.ToString();

        if(mAccumulator >= 3.0f)
        {
            GameContext.Instance.Countdown.SetActive(false);
            GameContext.Instance.PlayerInput.GetComponent<PlayerInputScript>().Stop();

            SearchType curr = GameContext.Instance.CurrentSearch;
            if (curr == SearchType.Color && GameContext.Instance.ColorReactions.Count == GameContext.Instance.Iterations)
                Finish();
            else if (curr == SearchType.Orientation && GameContext.Instance.OrientationReactions.Count == GameContext.Instance.Iterations)
                Finish();
            else if (curr == SearchType.Conjuction && GameContext.Instance.ConjuctionReactions.Count == GameContext.Instance.Iterations)
                Finish();
            else if (curr == SearchType.Dual && GameContext.Instance.DualReactions.Count == GameContext.Instance.Iterations)
                Finish();
            else
                GameContext.Instance.NextButton.SetActive(true);
        }
    }

    private void Finish()
    {
        GameContext.Instance.SaveButton.SetActive(true);
        GameContext.Instance.UpButton.SetActive(true);
        GameContext.Instance.DownButton.SetActive(true);
        GameContext.Instance.PlayerNumber.SetActive(true);
        GameContext.Instance.PlayerNumber.GetComponent<TextMeshProUGUI>().text = GameContext.Instance.Participant.ToString();
    }

    public void Reset() { mAccumulator = 0.0f; }
}
