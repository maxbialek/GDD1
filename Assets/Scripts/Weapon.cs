﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public Transform bulletPrefab;
    public float shootingRate = 0.25f;
    private float shootCooldown;
    // Start is called before the first frame update
    void Start()
    {
        shootCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        shootCooldown = shootingRate;
        var shotTransform = Instantiate(bulletPrefab) as Transform;
        shotTransform.position = transform.position;
        MoveBullet move = shotTransform.gameObject.GetComponent<MoveBullet>();
        move.direction = new Vector2(1, Random.Range(-0.05f, 0.05f));
    }
}
