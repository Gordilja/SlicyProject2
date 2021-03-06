using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject RetryPanel;
    public GameObject NextlvlPanel;
    public GameObject levelHolder;
    public GameObject RestartPanel;
    private float waitTime = 0.6f;
    public int i = 0;

    private void Start()
    {
        //Transform[] levelHolder;
        SpawnLevel();
        //levelHolder = gameObject.GetComponentsInChildren<Transform>();
        //Debug.Log(levelHolder.Length);
    }

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

    /*
    public void quit()
    {
        Application.Quit();
    }
    */

    public void retry()
    {
        StartCoroutine(retrySpawn());
        RetryPanel.SetActive(false);
    }

    IEnumerator retrySpawn() 
    {
        GameObject obj = GameObject.FindWithTag("Player");
        Destroy(obj);
        yield return new WaitForSeconds(0.1f);
        StartPanel.SetActive(true);
        Time.timeScale = 0;
        FindObjectOfType<SpawnPlayer>().spawnPlayer();    
    }

    IEnumerator restartGame()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        Destroy(obj);
        levelHolder.transform.GetChild(i).gameObject.SetActive(false);
        NextlvlPanel.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.2f);
        i = 0;
        levelHolder.transform.GetChild(i).gameObject.SetActive(true);
        FindObjectOfType<SpawnPlayer>().spawnPlayer();
        StartPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void restartG() 
    {
        StartCoroutine(restartGame());
        RestartPanel.SetActive(false);
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

    IEnumerator changelevel() 
    {
        GameObject obj = GameObject.FindWithTag("Player");
        Destroy(obj);
        levelHolder.transform.GetChild(i).gameObject.SetActive(false);
        NextlvlPanel.SetActive(false); 
        yield return new WaitForSeconds(0.2f);
        i++;
        levelHolder.transform.GetChild(i).gameObject.SetActive(true);
        FindObjectOfType<SpawnPlayer>().spawnPlayer();
        StartPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void next()
    {   
        if (i < 3)
        {
            StartCoroutine(changelevel());
        }
        else 
        {   
            StartCoroutine(restartGame());
        }
    }

    public void SpawnLevel()
    {   
            levelHolder.transform.GetChild(i).gameObject.SetActive(true);
            FindObjectOfType<SpawnPlayer>().spawnPlayer();
    }
    #endregion Nextlvl

    #region Test level changer
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
    #endregion
}
