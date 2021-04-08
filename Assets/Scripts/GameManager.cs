using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject StartPanel;
    public GameObject RetryPanel;
    public GameObject NextlvlPanel;
    private float waitTime = 0.6f;

    private void Awake()
    {
        StartPanel.SetActive(true);
        RetryPanel.SetActive(false);
        Time.timeScale = 0;
    }

    #region MainUses
    public void play()
    {
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        RetryPanel.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void restartGame()
    {
        SceneManager.LoadSceneAsync("Main");
    }
    #endregion

    #region GameOver
    public void gameEnd()
    {
        StartCoroutine(waitAnim());
    }

    IEnumerator waitAnim()
    {
        yield return new WaitForSeconds(waitTime);
        RetryPanel.SetActive(true);
    }
    #endregion

    #region Nextlvl
    public void nextlvl()
    {
        NextlvlPanel.SetActive(true);
    }

    public void next()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion Nextlvl

    private static SaveData saveData;
    public static SaveData SaveData
    {
        get
        {
            if (saveData == null)
            {
                Debug.LogError("SaveData does not exist in the scene.");
            }
            return saveData;

        }
        set
        {
            saveData = value;
        }
    }
}
