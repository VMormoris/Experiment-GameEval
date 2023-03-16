using UnityEngine;
using UnityEngine.Serialization;

public class PeripheryScript : MonoBehaviour
{
    [FormerlySerializedAs("Life Time")]
    [SerializeField]
    private int mLifeTime = 300;

    private float mAccumulator = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        bool diff = GameContext.Instance.HasPeripheryDiff;
        Transform tc;
        if (diff) tc = transform.GetChild(0).GetChild(1);
        else tc = transform.GetChild(0).GetChild(0);

        tc.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float lifetime = mLifeTime / 1000.0f;
        mAccumulator += Time.deltaTime;
        if (mAccumulator >= lifetime)
            Destroy(gameObject);
    }
}
