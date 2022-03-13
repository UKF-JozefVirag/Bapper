using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timer = 0.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI reviveText;
    public TextMeshProUGUI CountDownText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI TapToStartText;
    public GameObject GameOverPanel;
    public GameObject PauseMenuPanel;
    public GameObject Player;
    int seconds;
    private int orbsNeeded = 100;
    private bool isPaused = false;
    [SerializeField] Material[] skins = new Material[5];
    private void Start()
    {
        SetSkin();
        Time.timeScale = 0f;
    }

    private void SetSkin()
    {
        int skin = PlayerPrefs.GetInt("Skin");
        Player.GetComponent<MeshRenderer>().material = skins[skin];
    }

    public void WaitUntilTap()
    {
        TapToStartText.text = "";
        Time.timeScale = 1f;
    }

    void Update()
    {
        timer += Time.deltaTime;        
        int seconds = (int)(timer % 60);
        int orbs = PlayerPrefs.GetInt("Currency", 0);
        scoreText.text = "SCORE : " + seconds*10;
        PlayerPrefs.SetInt("Score", seconds * 10);
        currencyText.text = "ORBS: " + orbs;
        
        // ak sa na scnee objavi gameoverpanel, hra skoncila, skontroluje sa score.. ak je vacsie ako predosle highscore, tak sa prepise
        if (GameOverPanel.activeSelf)
        {
            int lasthighscore = PlayerPrefs.GetInt("HighScore", 0);
            if (seconds > lasthighscore)
            {
                PlayerPrefs.SetInt("HighScore", seconds*10);
                HighScoreText.text = PlayerPrefs.GetInt("HighScore")+"";
            }
        }
    }

    public int getSeconds()
    {
        return seconds;
    }


    public void Revive(){
        if (PlayerPrefs.GetInt("Currency") > orbsNeeded)
        {
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - orbsNeeded);
            Time.timeScale = 1f;
            orbsNeeded *= 2;
            GameOverPanel.SetActive(false);
            reviveText.text = "USE " + orbsNeeded + " ORBS";
        }
    }
    public void PlayAgain(){
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause()
    {
        if (isPaused){
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 0;
            isPaused = false;
            Time.timeScale = 1f; 
        } else {
            PauseMenuPanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
    }

    IEnumerator CountDown(int time)
    {
        int count = time;
        while (count > 0)
        {
            CountDownText.text = count + "";
            yield return new WaitForSeconds(1);
            count--;
        }
        CountDownText.text = "";
    }

}
