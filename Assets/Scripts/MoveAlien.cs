using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlien : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = new Vector2(1, 0);
    public Vector2 movement;
    public bool sinusMovement = false;
    public float frequency = 0f;
    public float magnitude = 0f;
    private Rigidbody2D rb2d;

    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = direction * speed;
    }

    private void FixedUpdate()
    {
        if (sinusMovement)
        {
            movement.y = transform.up.y * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        rb2d.velocity = movement;
    }

    private void LateUpdate()
    {
        if (rb2d.position.x < screenBounds.x * -1 - 1)
            Destroy(gameObject);
    }

    public void SetSinusMovement(float frequency, float magnitude)
    {
        this.frequency = frequency;
        this.magnitude = magnitude;
    }
}
