using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LetterScript : MonoBehaviour
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

        float x = Random.Range(-1.85f, 1.85f);
        float y = Random.Range(-1.85f, 1.85f);
        Debug.Log(""+ index + " [" + x + ", " + y + "]");
        transform.position = new Vector3(x, y, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, info.Angle);

        TextMeshPro text = transform.GetComponent<TextMeshPro>();
        text.text = info.Angle == 0.0f ? "T" : "L"; 
    }

    // Update is called once per frame
    void Update()
    {
        float lifetime = mLifeTime / 1000.0f;
        mAccumulator += Time.deltaTime;
        if (mAccumulator >= lifetime)
        {
            Destroy(gameObject);
            if (--GameContext.Instance.NextIndex == 0)
            {
                GameContext.Instance.Countdown.SetActive(true);
                GameContext.Instance.Countdown.transform.GetComponentInChildren<CountdownScript>().Reset();
            }
        }
    }
}
