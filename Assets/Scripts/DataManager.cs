using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public CharacterSelection.CharSelectionObject selectedObject;

    private Player player;
    public int score = 0;

    private float panelHeightBottom;

    private GameObject alienSpawnerObject;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(Player playerRef)
    {
        player = playerRef;
    }

    public void UpdateScore(int addPoints)
    {
        score += addPoints;
        player.UpdateScoreText();
    }

    public void GameOver()
    {
        GameOver gameOver = GameObject.Find("GameController").GetComponent<GameOver>();
        gameOver.GameOverTransition();
    }

    public void SetPanelHeightBottom(float panelHeight)
    {
        panelHeightBottom = panelHeight;
    }

    public void ActivateAlienSpawner()
    {
        alienSpawnerObject = GameObject.Find("AlienSpawner");
        AlienSpawner alienSpawner = alienSpawnerObject.GetComponent<AlienSpawner>();
        alienSpawner.SetPanelHeightBottom(panelHeightBottom);
        alienSpawner.startSpawning = true;
    }
}
