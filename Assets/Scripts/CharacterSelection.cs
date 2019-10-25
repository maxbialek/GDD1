using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharIndex;
    private Color desiredColor;
    public DataManager dataManager;

    public CharSelectionObject selectedObject;

    [Header("List of Characters")]
    [SerializeField] private List<CharSelectionObject> charList = new List<CharSelectionObject>();

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private Image charSplash;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI attackRateText;
    [SerializeField] private TextMeshProUGUI ammunitionText;
    [SerializeField] private TextMeshProUGUI reloadTimeText;


    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSound;
    [SerializeField] private AudioClip charSelectSound;

    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 10.0f;

    public void Awake()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        if (dataManager != null)
        {
            Debug.Log("Data Manager accessed");
        }
    }

    private void Start()
    {
        UpdateCharSelectionUI();
    }

    private void Update()
    {
        backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }

    public void LeftArrow()
    {
        selectedCharIndex--;
        if (selectedCharIndex < 0)
        {
            selectedCharIndex = charList.Count - 1;
        }

        UpdateCharSelectionUI();
    }

    public void RightArrow()
    {
        selectedCharIndex++;
        if (selectedCharIndex == charList.Count)
        {
            selectedCharIndex = 0;
        }

        UpdateCharSelectionUI();
    }

    public void Confirm()
    {
        selectedObject = charList[selectedCharIndex];
        dataManager.selectedObject = selectedObject;
        SceneManager.LoadScene("Game");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    private void UpdateCharSelectionUI()
    {
        CharSelectionObject obj = charList[selectedCharIndex];
        charSplash.sprite = obj.sprite;
        charName.text = obj.charName;
        desiredColor = obj.charColor;
        speedText.text = "Speed: " + obj.speed;
        attackRateText.text = "Attack Rate: " + obj.attackRate;
        ammunitionText.text = "Ammunition: " + obj.ammunition;
        reloadTimeText.text = "Reload Time: " + obj.reloadTime + "sec.";
    }

    [System.Serializable]
    public class CharSelectionObject
    {
        public Sprite sprite;
        public string charName;
        public Color charColor;
        public float speed;
        public float attackRate;
        public int ammunition;
        public float reloadTime;
    }
}
