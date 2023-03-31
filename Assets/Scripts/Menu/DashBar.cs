using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider;
    public Color barColor;
    public Image fill;

    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;

        fill.color = barColor;
    }

    public void SetEnergy(float energy)
    {
        slider.value = energy;

        fill.color = barColor;
    }
}
