using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public string charName;
    public DataManager dataManager;
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;
    public float speed;
    public Rigidbody2D rb2d;
    private Vector2 movement;

    public void Awake()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        if (dataManager != null)
        {
            Debug.Log("Data Manager found in Player");
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dataManager.charSprite;
        charName = dataManager.charName;
        rb2d = GetComponent<Rigidbody2D>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = spriteRenderer.bounds.extents.x;
        objectHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);
        movement *= speed;

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if(shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            weapon.Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
