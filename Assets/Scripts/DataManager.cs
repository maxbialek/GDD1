using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Sprite charSprite;
    public string charName;
    public int score;
    public int health;
    public int lives;

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

        health = 1;

        DontDestroyOnLoad(gameObject);
    }
}
