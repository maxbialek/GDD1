using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAlien : MonoBehaviour
{
    public int health;
    public ParticleSystem hitEffect;
    public ParticleSystem deathEffect;
    private DataManager dataManager;

    public AudioSource oof;


    private void Awake()
    {
        GameObject go = GameObject.Find("DataManager");
        dataManager = go.GetComponent<DataManager>();
    }

    private void Start()
    {
        oof = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Alien" && collision.tag == "Bullet")
        {
            AudioClip oofSound = oof.clip;
            oof.PlayOneShot(oofSound);
            Vector3 alienPos = gameObject.transform.position;
            alienPos.z += 0.1f;
            Destroy(collision.gameObject);
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
                dataManager.UpdateScore(1);
                instantiate(deathEffect, alienPos);
            }
            else
            {
                instantiate(hitEffect, alienPos);
            }
        }
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);
        return newParticleSystem;
    }
}
