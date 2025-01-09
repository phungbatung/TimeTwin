using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelSelector levelPrefabs;
    [SerializeField] private string levelScenePath;
    [SerializeField] private Transform parent;

    private void Start()
    {
        int count = 0;
        count = Resources.LoadAll(levelScenePath).Length;
        LevelSelector lvl;
        for (int i = 0; i < count; i++)
        {
            lvl = Instantiate(levelPrefabs);
            lvl.transform.SetParent(parent, false);
            lvl.SetLevel(i, i <=GameManager.Instance.levelProgress);
        }
    }


}
