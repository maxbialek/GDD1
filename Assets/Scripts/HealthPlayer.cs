using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    private int health;
    private DataManager dataManager;
    public HealthBar healthBar;

    private void Awake()
    {
        GameObject go = GameObject.Find("DataManager");
        dataManager = go.GetComponent<DataManager>();
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.tag == "Alien")
        {
            Destroy(collision.gameObject);
            health--;
            healthBar.UpdateHealthBar(maxHealth, health);
            if (health <= 0)
            {
                Destroy(gameObject);
                dataManager.GameOver();
            }
        }
    }

}
