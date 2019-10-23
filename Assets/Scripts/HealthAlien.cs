using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAlien : MonoBehaviour
{
    public int health;
    private DataManager dataManager;

    private void Awake()
    {
        GameObject go = GameObject.Find("DataManager");
        dataManager = go.GetComponent<DataManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Alien" && collision.tag == "Bullet")
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
                dataManager.UpdateScore(1); 
            }
        }
    }
}
