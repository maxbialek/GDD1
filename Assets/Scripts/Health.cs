using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.tag == "Alien" ||
            gameObject.tag == "Alien" && collision.tag == "Bullet")
        {
            if (gameObject.tag == "Player" && collision.tag == "Alien")
                Destroy(collision.gameObject);
            health -= 1;
            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
