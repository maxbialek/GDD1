using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite debug;
    public string charName;
    public DataManager dataManager;
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;
    public float speed;
    private Rigidbody2D rb2d;
    private Vector2 movement;

    public TextMeshProUGUI scoreText;
    public RectTransform hud;
    public Canvas canvas;
    private float panelHeight;

    public GameObject WeaponObject;

    public void Awake()
    {
        GameObject go = GameObject.Find("DataManager");
        if (go != null)
        {
            dataManager = go.GetComponent<DataManager>();
            Debug.Log("Data Manager found in Player");
        }
    }

    public float GetObjectHeight()
    {
        return objectHeight;
    }

    public float GetObjectWidth()
    {
        return objectWidth;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (dataManager != null)
        {
            spriteRenderer.sprite = dataManager.selectedObject.sprite;
            charName = dataManager.selectedObject.charName;
            speed = dataManager.selectedObject.speed;
        }
        rb2d = GetComponent<Rigidbody2D>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = spriteRenderer.bounds.extents.x;
        objectHeight = spriteRenderer.bounds.extents.y;

        dataManager.AddPlayer(this);
        dataManager.score = 0;
        scoreText.faceColor = new Color32(0, 0, 0, 255);

        //panelWidth = screenBounds.x * hud.rect.size.x / canvas.pixelRect.size.x;
        panelHeight = screenBounds.y * hud.rect.size.y / canvas.pixelRect.size.y;
        dataManager.SetPanelHeightBottom(panelHeight);
        dataManager.ActivateAlienSpawner();
        WeaponObject.SetActive(true);
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);
        movement *= speed;

        bool shoot2 = Input.GetButton("Fire1");
        if(shoot2)
        {
            Weapon weapon = GetComponentInChildren<Weapon>();
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
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight + panelHeight * 2, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + dataManager.score;
    }
}
