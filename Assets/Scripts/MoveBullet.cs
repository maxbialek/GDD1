using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = new Vector2(1, 0);
    public Vector2 movement;
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = direction * speed;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement;
    }

    private void LateUpdate()
    {
        if (rb2d.position.x > screenBounds.x + 1)
            Destroy(gameObject);
    }
}
