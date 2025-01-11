using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordButton : ButtonBase, IResettable
{
    [SerializeField] private Sprite startRecordSprite;
    [SerializeField] private Sprite endRecordSprite;
    private Image image;
    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }
    private void Start()
    {
        GameManager.Instance.OnReplay += Disable;
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
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void ResetToDefault()
    {
        image.sprite = startRecordSprite;
        gameObject.SetActive(true);
    }
}
