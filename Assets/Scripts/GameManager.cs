using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecordStatus
{
    NotStarted,
    Recording,
    Complete
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerMovement player;
    public ReplaySystem record { get; private set; }
    [SerializeField] private GameObject clonePrefab;

    public RecordStatus recordStatus { get; private set; }

    public Action OnRestart;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        record = GetComponent<ReplaySystem>();
    }

    private void Update()
    {
        if (recordStatus == RecordStatus.Recording)
        {

        }
    }


    public void Record()
    {
        //Record
        record.StartRecord(player);
        recordStatus = RecordStatus.Recording;
    }

    public void Replay()
    {
        player.ResetPostion();
        Instantiate(clonePrefab).GetComponent<CloneMovement>().SetupData(record);
        record.EndRecord();

    }

    public void Restart()
    {
        //Restart
        OnRestart?.Invoke();
        recordStatus = RecordStatus.NotStarted;
    }

    public void Victory()
    {

    }    

    public void OpenMenu()
    {

    }
}
