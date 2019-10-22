using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Player player;

    void Update()
    {
        if (player == null)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
