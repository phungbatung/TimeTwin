using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private TextMeshProUGUI levelTMP;
    private int level;
    private bool hasBeenUnlocked;
    private void Awake()
    {
        image = GetComponent<Image>();
        levelTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLevel(int _level, bool _hasBeenUnlocked)
    {
        level = _level;
        levelTMP.text = level.ToString();
        hasBeenUnlocked = _hasBeenUnlocked;
        if (!hasBeenUnlocked)
            image.color = new Color(0.5f, .5f, .5f);
           
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hasBeenUnlocked)
            GameManager.Instance.LoadSceneAtLevel(level);
    }
}
