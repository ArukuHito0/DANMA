using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class GaugeUIBar : MonoBehaviour
{
    [SerializeField]
    private Image gaugeImage;

    public virtual void UpdateFillAmount(float rate)
    {
        gaugeImage.fillAmount = rate;
    }
}
