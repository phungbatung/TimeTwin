using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RecordStatus
{
    NotStarted,
    Recording,
    Complete
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject clonePrefab;
    public LevelsDataSO levelsData;
    public PlayerMovement player { get; set; }
    public ReplaySystem record;

    public RecordStatus recordStatus { get; private set; }

    public Action OnRestart { get; set; }
    public Action OnReplay { get; set; }
    public GameObject menuScreen;
    public GameObject victoryScreen;


    public int currentLevel { get; set; }
    public int levelProgress { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        player = GameObject.Find("Player")?.GetComponent<PlayerMovement>();
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        LoadProgress();
        FindAllResettableObject();
        Physics2D.callbacksOnDisable = false;
        UnityEngine.Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 60;
    }
    private void OnDestroy()
    {
        Debug.Log("PLayer was destroyed!");
    }
    private void FindAllResettableObject()
    {
        IEnumerable < IResettable > resettables = FindObjectsOfType<MonoBehaviour>().OfType<IResettable>();
        List<IResettable> resettableObjects = new List<IResettable>(resettables);
        foreach(var obj in resettableObjects)
        {
            OnRestart += obj.ResetToDefault;
        }
    }
    public void Record()
    {
        //Record
        record.StartRecord(player, levelsData[currentLevel]);
        recordStatus = RecordStatus.Recording;
    }

    public void Replay()
    {
        OnReplay?.Invoke();
        Instantiate(clonePrefab).GetComponent<CloneMovement>().SetupData(record);
        record.EndRecord();

    }

    public void Restart()
    {
        //Restart
        OnRestart?.Invoke();
        recordStatus = RecordStatus.NotStarted;
    }

    public void LoadLevelMenuScene()
    {
        SceneManager.LoadScene("LevelMenu");
    }    

    public void Victory()
    {
        FreezeTime();
        victoryScreen.SetActive(true);
        if (currentLevel + 1 > levelProgress)
        {
            levelProgress++;
            SaveProgress();
        }
    }    

    public void OpenMenu()
    {
        FreezeTime();
        menuScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        UnfreezeTime();
        menuScreen.SetActive(false);
    }
    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoToNextLevel()
    {
        LoadSceneAtLevel(currentLevel + 1);
    }

    public void LoadSceneAtLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        UnfreezeTime();
        SceneManager.LoadScene($"Level{level}");
    }

    public void FreezeTime()
    {
        Time.timeScale = 0f;
    }    
    public void UnfreezeTime()
    {
        Time.timeScale = 1.0f;
    }

    [ContextMenu("Save progress")]
    public void SaveProgress()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "ProgressData.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(levelProgress.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }   
    public void LoadProgress()
    {
        //dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        string path = Path.Combine(Application.persistentDataPath, "ProgressData.txt");
        if (File.Exists(path))
        {
            try
            {
                
                string dataToLoad = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                levelProgress = int.Parse(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        else 
        { 
            levelProgress = 0; 
        }

    }
    [ContextMenu("ResetSaveData")]
    public void ResetData()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "ProgressData.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write("0");
                }
            }

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }    
}
