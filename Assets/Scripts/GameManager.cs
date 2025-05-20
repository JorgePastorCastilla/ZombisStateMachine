using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public int enemiesAlive;

    public int round = 0;
    
    public GameObject[] spawnPoints;

    public GameObject enemyPrefab;

    public Image HpBar;
    public TextMeshProUGUI RoundText;
    
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive <= 0)
        {
            NextWave();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverPanel.activeSelf)
        {
            if (pausePanel.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void NextWave()
    {
        int min = 0;
        int max = spawnPoints.Length - 1;

        round++;
        RoundText.text = round.ToString();
        for (int i = 0; i < round; i++)
        {
            int index = Random.Range(min, max);
        
            GameObject selectedSpawn = spawnPoints[index];

            GameObject zombi = Instantiate(enemyPrefab, selectedSpawn.transform.position, Quaternion.identity);
            
            zombi.GetComponent<EnemyManager>().gameManager = this;
            enemiesAlive++;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
