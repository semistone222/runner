using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class CatManager : MonoBehaviour
{
    public Canvas uiGauge;
    public Canvas uiTextBalloon;
    public float timeInterval;
    public float decreaseInterval;
    public int decreaseAmount;

    GameObject gaugeHunger;
    GameObject gaugeAffine;
    GameObject gaugeHealth;
    Strings strings;
    TextBalloonManager tbMng;
    AudioSource catAudio;
    bool isUiOn;
    float time;
    float decreaseTime;

    void Awake()
    {
        time = 0f;
        decreaseTime = 0f;

        uiGauge.enabled = false;
        uiTextBalloon.enabled = false;
        isUiOn = false;

        catAudio = GetComponent<AudioSource>();
        strings = GetComponent<Strings>();
        tbMng = uiTextBalloon.GetComponent<TextBalloonManager>();
        gaugeHunger = uiGauge.gameObject.transform.FindChild("GaugeHunger").gameObject;
        gaugeAffine = uiGauge.gameObject.transform.FindChild("GaugeAffine").gameObject;
        gaugeHealth = uiGauge.gameObject.transform.FindChild("GaugeHealth").gameObject;
    }

    void Update()
    {
        //상태따라 대사 입력(3초마다)
        time += Time.deltaTime;
        decreaseTime += Time.deltaTime;

        if(time > timeInterval)
        {
            time = 0f;

            int[] state = StateReturn();
            int index = state[0] + state[1] * 3 + state[2] * 9;
            tbMng.Play(strings.stateStrings[index]);
            catAudio.Play();
        }
        if(decreaseTime > decreaseInterval)
        {
            decreaseTime = 0f;

            PlayerPrefs.SetInt("Affine", PlayerPrefs.GetInt("Affine") - decreaseAmount);
            PlayerPrefs.SetInt("Hunger", PlayerPrefs.GetInt("Hunger") - decreaseAmount);
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - decreaseAmount);

            /*
            gaugeHunger.GetComponent<Slider>().value
                = gaugeHunger.GetComponent<Slider>().value - decreaseAmount;
            gaugeAffine.GetComponent<Slider>().value
                = gaugeAffine.GetComponent<Slider>().value - decreaseAmount;
            gaugeHealth.GetComponent<Slider>().value
                = gaugeHealth.GetComponent<Slider>().value - decreaseAmount;*/
        }
    }

    int[] StateReturn()
    {
        int[] state = new int[3];
        state[2] = StateCheck(gaugeHunger);
        state[1] = StateCheck(gaugeAffine);
        state[0] = StateCheck(gaugeHealth);

        return state;
    }

    int StateCheck(GameObject gauge)
    {
        int val = (int)gauge.GetComponent<Slider>().value;
        if(val < 20)
        {
            return 0;
        }
        else if(val >= 20 && val <= 50)
        {
            return 1;
        }
        else if(val > 50 && val <= 100)
        {
            return 2;
        }
        return 3;
    }

    public void OnMouseUp()
    {
        isUiOn = !isUiOn;
        if(isUiOn)
        {
            catAudio.Play();
        }
        uiGauge.enabled = !uiGauge.enabled;
    }
    
}
