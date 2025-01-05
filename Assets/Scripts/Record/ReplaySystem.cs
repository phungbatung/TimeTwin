using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ReplaySystem : MonoBehaviour
{
    private PlayerMovement player;
    public List<RecordData> recordData;
    private bool isRecording = false;
    private bool isTimeLimited;
    [SerializeField] private float recordRate;
    private float recordDuration;
    private float recordTimer;
    private float recordTimeFixed;

    private float replayStartTime;
    public bool hasNextRecordData { get; private set; }
    private void Awake()
    {
        recordData = new List<RecordData>();
    }
    private void Start()
    {
        GameManager.Instance.OnRestart += ResetToDefault;
    }
    //private float currentIndex;
    private void Update()
    {
        if (isRecording)
            Record();
    }

    public void StartRecord(PlayerMovement _player ,float _recorduration=-1)
    {
        if(_recorduration==-1)
            isTimeLimited = false;
        else
            recordDuration = _recorduration;
        player = _player;
        recordTimeFixed = recordRate;
        recordTimer = 0f;
        //currentIndex = 0;
        isRecording = true;
    }    

    public void Record()
    {
        
        recordTimer += Time.deltaTime;
        recordTimeFixed += Time.deltaTime;
        if (recordTimeFixed>=recordRate)
        {
            recordData.Add(new RecordData(player.transform.position, player.facingRight, recordTimer));
            recordTimeFixed -= recordRate;
        }
        if (isTimeLimited)
        {
            if(recordTimer >= recordDuration)
                EndRecord();
        }
    }

    public void EndRecord()
    {
        isRecording = false;
        hasNextRecordData = true;
        replayStartTime = Time.time;
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
        recordData = null;
        isRecording = false;
        hasNextRecordData = false;
    }
}
