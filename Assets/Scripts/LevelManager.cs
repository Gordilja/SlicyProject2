using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform levelHolder;
    public List<Levels> levels = new List<Levels>();
    public Levels activeLevel;

    private void Awake()
    {
        GameManager.LevelManager = this;
        PopulateLevels();
    }

    public void PopulateLevels()
    {
        foreach (Transform t in levelHolder)
        {
            Levels level = t.gameObject.GetComponent<Levels>();
            levels.Add(level);
        }
    }

    public void SpawnLevel()
    {
        foreach (Levels lvl in levels)
        {
            lvl.gameObject.SetActive(false);
        }

        if (GameManager.SaveData.level > levels.Count)
        {
            GameManager.SaveData.ResetLevels();
        }
        activeLevel = levels[GameManager.SaveData.level - 1];
        activeLevel.gameObject.SetActive(true);
    }
}
