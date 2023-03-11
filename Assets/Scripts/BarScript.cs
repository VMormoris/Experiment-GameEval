using UnityEngine;
using UnityEngine.Serialization;

public class BarScript : MonoBehaviour
{   
    [FormerlySerializedAs("Life Time")]
    [SerializeField]
    private int mLifeTime = 300;

    private float mAccumulator = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        int index = GameContext.Instance.NextIndex++;
        BarInfo[] bars = GameContext.Instance.BarsToSpawn;
        BarInfo info = bars[index];

        Vector2 coords = GetCoordinates(index % 5, index / 5);
        transform.position = new Vector3(coords.x, coords.y , 0.0f);
        transform.rotation = Quaternion.Euler(0, 0, info.Angle);

        transform.GetComponent<SpriteRenderer>().color = info.color;
    }

    // Update is called once per frame
    void Update()
    {
        float lifetime = mLifeTime / 1000.0f;
        mAccumulator += Time.deltaTime;
        if(mAccumulator >= lifetime)
        {
            Destroy(gameObject);
            if(--GameContext.Instance.NextIndex == 0)
            {
                GameContext.Instance.Countdown.SetActive(true);
                GameContext.Instance.Countdown.transform.GetComponentInChildren<CountdownScript>().Reset();
            }
        }
    }

    Vector2 GetCoordinates(int i, int j) { return new Vector2(((int)i - 2) * 3.0f, ((int)j - 2) * 2.0f); }
}
