using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;
    private DataManager dataManager;

    public void Awake()
    {
        GameObject go = GameObject.Find("DataManager");
        if (go != null)
        {
            dataManager = go.GetComponent<DataManager>();
            Debug.Log("Data Manager found in GameOverScreen");
        }
    }

    private void Start()
    {
        finalScore.text = "Score: " + dataManager.score;
        highScore.text = "Highscore: " + dataManager.maxScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
