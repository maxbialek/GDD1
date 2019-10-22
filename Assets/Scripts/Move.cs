using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = new Vector2(1, 0);
    public Vector2 movement;
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public bool isEnemy = false;

    public enum MovementType { Straight, Diagonal, Sinus };
    public MovementType movementType;

    private float frequency;
    private float magnitude;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "Alien")
        {
            int enumLength = Enum.GetValues(typeof(MovementType)).Length;
            movementType = (MovementType) UnityEngine.Random.Range(0, enumLength);
            if(movementType == MovementType.Straight)
            {
                direction.y = 0f;
            }
            else if(movementType == MovementType.Sinus)
            {
                frequency = UnityEngine.Random.Range(3f, 6f);
                magnitude = UnityEngine.Random.Range(4f, 8f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = direction * speed;

    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Alien")
        {
            if (movementType == MovementType.Sinus)
                movement.y = transform.up.y * Mathf.Sin(Time.time * frequency) * magnitude;

        }
        rb2d.velocity = movement;
    }

    private void LateUpdate()
    {
        if (rb2d.position.x > screenBounds.x + 1 && !isEnemy)
        {
            Destroy(gameObject);
        }
        else if (rb2d.position.x < screenBounds.x * -1 - 1 && isEnemy)
        {
            Destroy(gameObject);
        }
    }
}
