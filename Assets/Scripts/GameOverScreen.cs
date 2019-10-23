using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
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
        finalScore.faceColor = new Color32(255, 255, 255, 255);
        finalScore.text = "Score: " + dataManager.score;
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
