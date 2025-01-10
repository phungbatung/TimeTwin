using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour, IResettable
{
    private PlayerMovement player;
    public List<RecordData> recordData { get; private set; }
    private bool isRecording = false;
    private bool isTimeLimited;
    [SerializeField] private float recordRate;
    private float recordDuration;
    private float recordTimer;
    private float recordTimeFixed;

    private float replayStartTime;
    public bool hasNextRecordData { get; private set; }
    public Timer_UI timerUI;
    private void Start()
    {
        recordData = new();
    }
    //private float currentIndex;
    private void Update()
    {
        if (isRecording)
            Record();
    }

    public void StartRecord(PlayerMovement _player , LevelData _levelData)
    {
        isTimeLimited = _levelData.isTimeLimited;
        recordDuration = _levelData.recordDuration;
        timerUI.gameObject.SetActive(isTimeLimited);
        timerUI.Setup(recordDuration);
        player = _player;
        recordTimeFixed = recordRate;
        recordTimer = recordDuration;
        //currentIndex = 0;
        isRecording = true;
        
    }    

    public void Record()
    {
        
        recordTimer -= Time.deltaTime;
        timerUI.UpdateTimeLeft(recordTimer);
        recordTimeFixed += Time.deltaTime;
        if (recordTimeFixed>=recordRate)
        {
            recordData.Add(new RecordData(player.transform.position, player.facingRight, recordDuration - recordTimer));
            recordTimeFixed -= recordRate;
        }
        if (isTimeLimited)
        {
            if (recordTimer <= 0)
                GameManager.Instance.Replay();
        }
    }

    public void EndRecord()
    {
        isRecording = false;
        hasNextRecordData = true;
        replayStartTime = Time.time;
        timerUI.gameObject.SetActive(false);
    }

    public RecordData GetRecordData()
    {
        if (recordData == null || recordData.Count == 0)
        {
            hasNextRecordData = false;
            return null;
        }
        RecordData res = recordData[0];
        float timeStamp = Time.time - replayStartTime;
        if (timeStamp >= recordData[recordData.Count - 1].timeStamp)
        {
            res = recordData[recordData.Count - 1];
            hasNextRecordData = false;
        }
        else
        {
            for (int i = 0; i < recordData.Count - 1; i++)
            {
                if (timeStamp < recordData[i + 1].timeStamp)
                {
                    res = recordData[i];
                    break;
                }
            }
        }
        return res;
    }

    public void ResetToDefault()
    {
        player = null;
        recordData.Clear();
        isRecording = false;
        hasNextRecordData = false;
        timerUI.gameObject.SetActive(false);
    }
}
