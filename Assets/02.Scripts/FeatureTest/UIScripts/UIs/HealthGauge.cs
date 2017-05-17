using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthGauge : GaugeManager
{
    void Awake()
    {
        slider = GetComponent<Slider>();
        gaugeScript = GetComponent<Gauges>();
    }
    void Update()
    {
        slider.value = PlayerPrefs.GetInt("Health");
        ColorChange();
    }

}
