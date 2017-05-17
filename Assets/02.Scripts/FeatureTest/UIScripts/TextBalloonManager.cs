using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBalloonManager : MonoBehaviour
{
    Canvas itself;
    Text textInfo;

    void Awake()
    {
        itself = GetComponent<Canvas>();
        textInfo = transform.FindChild("Text").gameObject.GetComponent<Text>();
    }

    public void Play(string str)
    {
        textInfo.text = str;
        Invoke("toggleBalloon",0f);
        Invoke("toggleBalloon", 1f);

    }

    void toggleBalloon()
    {
        itself.enabled = !itself.enabled;
    }
}
