using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlienSpawner : MonoBehaviour
{
    private DataManager dataManager;

    public Transform alienPrefab;
    public float spawnRate = 2.0f;
    public float spawnCooldown;
    public bool startSpawning = false;

    public enum MovementType { Straight, Diagonal, Sinus };
    public MovementType movementType;
    // for sinus movement
    private float frequency;
    private float magnitude;


    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;
    private float panelHeight;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        GameObject go = GameObject.Find("DataManager");
        if (go != null)
        {
            dataManager = go.GetComponent<DataManager>();
            Debug.Log("Data Manager found in Player");
        }
    }


    void Start()
    {
        spawnCooldown = 5f;
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        SpriteRenderer alienSR = alienPrefab.GetComponent<SpriteRenderer>();
        objectWidth = alienSR.bounds.extents.x;
        objectHeight = alienSR.bounds.extents.y;
    }

    void Update()
    {
        if (startSpawning)
        {
            if(spawnCooldown > 0f)
                spawnCooldown -= Time.deltaTime;
            else
                Spawn();
        }
    }

    private void Spawn()
    {
        spawnCooldown = UnityEngine.Random.Range(0.5f, 1.5f);

        float doubleSpawn = UnityEngine.Random.Range(1f, 100f);
        int numAliens = doubleSpawn < 50f ? 2 : 1;
        
        for (int i = 0; i < numAliens; i++)
        {
            var alien = Instantiate(alienPrefab) as Transform;

            int enumLength = Enum.GetValues(typeof(MovementType)).Length;
            movementType = (MovementType)UnityEngine.Random.Range(0, enumLength);

            float yPosStart, yPosEnd, xPosStart, xPosEnd;
            yPosStart = UnityEngine.Random.Range(screenBounds.y * -1 + objectHeight + panelHeight * 2, screenBounds.y - objectHeight);
            xPosStart = screenBounds.x + 1;
            xPosEnd = screenBounds.x * -1 - 1;

            MoveAlien move = alien.gameObject.GetComponent<MoveAlien>();
            if (movementType == MovementType.Straight || movementType == MovementType.Sinus)
            {
                yPosEnd = yPosStart;
                if (movementType == MovementType.Sinus)
                {
                    frequency = UnityEngine.Random.Range(1f, 10f);
                    magnitude = UnityEngine.Random.Range(4f, 8f);

                    if (yPosStart - magnitude / 2 <= -screenBounds.y + panelHeight * 2)
                        yPosStart = yPosEnd = -screenBounds.y + magnitude / 2 + panelHeight * 2;
                    else if (yPosStart + magnitude / 2 >= screenBounds.y)
                        yPosStart = yPosEnd = screenBounds.y - magnitude / 2;

                    move.sinusMovement = true;
                    move.SetSinusMovement(frequency, magnitude);
                }
            }
            else
                yPosEnd = UnityEngine.Random.Range(screenBounds.y * -1 + objectWidth + panelHeight * 2, screenBounds.y - objectWidth);

            alien.position = new Vector3(xPosStart, yPosStart, -9f);
            Vector2 direction = new Vector2(xPosEnd - xPosStart, yPosEnd - yPosStart).normalized;
            move.direction = direction;
            move.speed = UnityEngine.Random.Range(3f, 6f);
        }
    }

    public void SetPanelHeightBottom(float height)
    {
        panelHeight = height;
    }
}
