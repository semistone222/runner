using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelText : MonoBehaviour {

    Sprites sprites;
    Image img;
    int exp;

    void Awake()
    {
        sprites = GetComponent<Sprites>();
        img = GetComponent<Image>();
    }
	// Update is called once per frame
	void Update ()
    {
        exp = PlayerPrefs.GetInt("EXP");
        exp = Mathf.FloorToInt(exp / 100) + 1;
        if (exp > 10)
        {
            exp = 10;
        }
        else if (exp <= 0)
        {
            exp = 1;
        }
        exp--;
        img.sprite = sprites.stateSprites[exp];

    }
}
