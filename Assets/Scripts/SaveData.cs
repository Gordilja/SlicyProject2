using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveData : MonoBehaviour
{
    /*[BoxGroup("Data")]*/ public int level = 1;
    /*[BoxGroup("Data")]*/public int achievedLevel = 1;
    public int score;
    public TextMeshProUGUI scoreNum;

    private void Awake()
    {
        GameManager.SaveData = this;
        achievedLevel = PlayerPrefs.GetInt("AchievedLevel", 1);
        level = PlayerPrefs.GetInt("Level", 1);
    }

    #region Save data
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", level);
    }

    public void SaveAchievedLevel()
    {
        PlayerPrefs.SetInt("AchievedLevel", achievedLevel);
    }

    public void scoreUp() 
    {
        scoreNum.text = score.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }
    #endregion

    #region Load data
    public void LoadLevel()
    {
        PlayerPrefs.GetInt("Level", level);
    }

    public void LoadAchievedLevel()
    {
        PlayerPrefs.GetInt("AchievedLevel", achievedLevel);
    }

    public void LoadScore()
    {
        PlayerPrefs.GetInt("Score", score);
        
    }
    #endregion

    #region Functions
    public void IncrementLevel()
    {
        level++;
        SaveLevel();
    }

    public void IncrementAchievedLevel()
    {
        achievedLevel++;
        SaveAchievedLevel();
    }

    public void ResetLevels()
    {
        level = 1;
        SaveLevel();
    }
    #endregion
}
