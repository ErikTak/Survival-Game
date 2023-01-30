using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxExp(float experience)
    {
        slider.maxValue = experience;
        slider.value = experience;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetExp(float experience)
    {
        slider.value = experience;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
