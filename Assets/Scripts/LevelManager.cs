using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public float levelDuration = 30f;
    public Text timerText;
    public Text scoreText;
    public Text gameText;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    public string nextLevel;

    public static float score = 0;
    public static bool isGameOver = false;

    float countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = levelDuration;
        isGameOver = false;
        setTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (countDown > 0) countDown -= Time.deltaTime;
            else
            {
                countDown = 0f;
                LevelLost();
            }

            setTimerText();
        }
    }

    void setTimerText()
    {
        timerText.text = countDown.ToString("f2");
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int scoreValue)
    {
        if (countDown >= (levelDuration / 2))
        {
            score += scoreValue * 2;
        }
        else
        {
            score += scoreValue;
        }
        setScoreText();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "LEVEL FAILED";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelWon()
    {
        isGameOver = true;
        gameText.text = "LEVEL COMPLETE";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 2;
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel)) Invoke("LoadNextLevel", 2);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
