using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultiStartLaoding : MonoBehaviour
{
    public Text StageText;
    public GameObject InitTimeImage;
    public GameObject TimeImage;
    public GameObject StartBackGround;
    public GameObject Boundary;
    public GameObject Booster;
    public GameObject number;

    private GameObject Character;

    public Sprite number1;
    public Sprite number2;
    public Sprite number3;

    float CheckTime = 0;

    Coroutine StartLoading;

    public SoundManager soundManager;
    //private GameObject Sound;

    // Use this for initialization
    void Start()
    {
        soundManager.SetBGM2();
        //Sound = GameObject.FindGameObjectWithTag("BGM");

        InitTimeImage.SetActive(true);
        StartBackGround.SetActive(true);
        //Sound.SetActive (false);
        StartLoading = StartCoroutine(startLoadingControl());
    }

    IEnumerator startLoadingControl()
    {
        yield return new WaitForSeconds(2.0f);
        StartBackGround.SetActive(false);
        number.SetActive(true);
        number.GetComponent<Image>().sprite = number3;
        //Sound.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        number.GetComponent<Image>().sprite = number2;

        yield return new WaitForSeconds(1.0f);
        number.GetComponent<Image>().sprite = number1;

        yield return new WaitForSeconds(1.0f);
        Boundary.SetActive(false);
        number.SetActive(false);
        Booster.SetActive(true);
        InitTimeImage.SetActive(false);
        TimeImage.SetActive(true);
        Booster.GetComponent<Button>().enabled = true;

        if (StageManager.ItemChecked[2] == true)
        {
            yield return new WaitForSeconds(5.0f);
            StageManager.ItemChecked[2] = false;
        }

        StopCoroutine(StartLoading);
    }
}