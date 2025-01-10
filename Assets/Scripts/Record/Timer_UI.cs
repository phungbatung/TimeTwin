using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer_UI : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI timeLeft;
    private float duration;
    private void Awake()
    {
        image = GetComponent<Image>();
        timeLeft = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void UpdateTimeLeft(float _timeLeft)
    {
        if (duration <= 0)
            return;
        timeLeft.text = Math.Round(_timeLeft, 2).ToString();
        image.fillAmount = _timeLeft / duration;
    }
    public void Setup(float _duration)
    {
        duration = _duration;
    }
}
