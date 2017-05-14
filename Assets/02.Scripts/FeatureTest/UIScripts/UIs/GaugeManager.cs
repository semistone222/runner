using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeManager : MonoBehaviour
{
    protected Slider slider;
    protected Gauges gaugeScript;

    protected void ColorChange()
    {
        if(slider.value > 50)
        {
            slider.fillRect.gameObject.GetComponent<Image>().sprite = gaugeScript.gauges[0];
        }
        else if(slider.value <= 50 && slider.value >= 20)
        {
            slider.fillRect.gameObject.GetComponent<Image>().sprite = gaugeScript.gauges[1];
        }
        else if(slider.value < 20)
        {
            slider.fillRect.gameObject.GetComponent<Image>().sprite = gaugeScript.gauges[2];
        }
    }
}
