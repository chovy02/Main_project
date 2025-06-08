using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Health_bar : MonoBehaviour
{


    public Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void  setHealth(int health)
    {
        slider.value = health;


    }
}
