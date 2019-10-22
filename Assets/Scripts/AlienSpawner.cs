using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public Transform alienPrefab;
    public float spawnRate = 2.0f;
    public float spawnCooldown;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = 0f;
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        SpriteRenderer alienSR = alienPrefab.GetComponent<SpriteRenderer>();
        objectWidth = alienSR.bounds.extents.x;
        objectHeight = alienSR.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCooldown > 0f)
        {
            spawnCooldown -= Time.deltaTime;
        }
        else
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        spawnCooldown = spawnRate;
        var alien = Instantiate(alienPrefab) as Transform;
        float yPosStart = Random.Range(screenBounds.y * -1 + objectWidth, screenBounds.y - objectWidth);
        float yPosEnd = Random.Range(screenBounds.y * -1 + objectWidth, screenBounds.y - objectWidth);
        float xPosStart = screenBounds.x + 1;
        float xPosEnd = screenBounds.x * -1 -1;
        Vector2 direction = new Vector2(xPosEnd - xPosStart, yPosEnd - yPosStart).normalized;
        alien.position = new Vector2(xPosStart, yPosStart);
        Move move = alien.gameObject.GetComponent<Move>();
        move.isEnemy = true;
        move.direction = direction;
    }
}
