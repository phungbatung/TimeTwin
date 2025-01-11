using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName ="LevelsData")]
public class LevelsDataSO : ScriptableObject
{
    public List<LevelData> levelsData;
    public int levelsCount
    {
        get { return levelsData.Count; }
    }    
    public LevelData this[int index]
    {
        get
        {
            if(index<levelsData.Count) 
                return levelsData[index];
            Debug.LogError("An error occurred while trying to get data from LevelsDataSO. Index out of range!");
            return null;
        }
    }    
}
