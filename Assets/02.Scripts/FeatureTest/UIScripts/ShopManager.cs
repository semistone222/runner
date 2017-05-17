using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public GameObject textBox;
    public bool isTextBox = false; 

    void Awake()
    {
        isTextBox = false;
        textBox.SetActive(isTextBox);
    }

    public void btnToggleTextBox()
    {
        isTextBox = !isTextBox;
        textBox.SetActive(isTextBox);
    }
}
