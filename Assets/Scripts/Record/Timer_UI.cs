using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer_UI : MonoBehaviour
{
    private Image img;
    private TextMeshProUGUI timeLeft;
    private float duration;

    private void Awake()
    {
        img = GetComponent<Image>();
        timeLeft = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateTimer(float _timeLeft)
    {
        if (duration <= 0)
            return;
        img.fillAmount = _timeLeft/duration;
        timeLeft.text = Math.Round(_timeLeft, 2).ToString();
    }

    public void Setup(float _duration)
    {
        duration = _duration;
    }
}
