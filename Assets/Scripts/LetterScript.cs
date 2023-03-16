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

    private static char[] sLetters = { 'T', 'L' };

    // Start is called before the first frame update
    void Start()
    {
        int index = GameContext.Instance.NextIndex++;
        LetterInfo[] letters = GameContext.Instance.LettersToSpawn;
        LetterInfo info = letters[index];

        RectTransform tc = GetComponent<RectTransform>();
        tc.position = new Vector3(info.Position.x, info.Position.y, 0.0f);
        tc.rotation = Quaternion.Euler(0.0f, 0.0f, info.Angle);

        TextMeshPro text = transform.GetComponent<TextMeshPro>();
        text.text = info.Angle == 0.0f ? sLetters[0].ToString() : sLetters[1].ToString();

        if (index == 4)//Swap characters for next test
        {
            char temp = sLetters[0];
            sLetters[0] = sLetters[1];
            sLetters[1] = temp;
        }
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
