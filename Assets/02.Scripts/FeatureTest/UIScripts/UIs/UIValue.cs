using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIValue : MonoBehaviour
{
    public int value;

    protected Text valueText;

    // Use this for initialization
    void Awake()
    {
        valueText = GetComponent<Text>();
    }
    
}
