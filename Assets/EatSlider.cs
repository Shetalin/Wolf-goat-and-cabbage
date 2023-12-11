using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatSlider : MonoBehaviour
{
    public Slider slider;

    public void RolesNormal()
    {
        slider.value = 0;
    }

    public void RolesReversed()
    {
        slider.value = 1;
    }
}
