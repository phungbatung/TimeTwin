using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelsData", menuName = "LevelsData")]
public class LevelDataManager : ScriptableObject
{
    public List<LevelData> datas;
    public int LevelCount
    {
        get { return datas.Count; }
    }
    public LevelData this[int index]
    { 
        get 
        {
            if(index < datas.Count)
                return datas[index];
            Debug.LogError("Index out of range");
            return null;
        } 
    }
}
