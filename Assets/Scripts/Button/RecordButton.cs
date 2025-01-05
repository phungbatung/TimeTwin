using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordButton : ButtonBase
{
    [SerializeField] private Sprite endRecordSprite;
    private Image image;
    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }
    public override void OnClickAction()
    {
        RecordStatus stt = GameManager.Instance.recordStatus;
        if (stt == RecordStatus.NotStarted)
        {
            GameManager.Instance.Record();
            image.sprite = endRecordSprite;
        }
        else if (stt == RecordStatus.Recording)
        {
            GameManager.Instance.Replay();
            gameObject.SetActive(false);
        }
    }
}
