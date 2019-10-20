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
    public CharSelectionObject selectedChar;
    public DataManager dataManager;

    [Header("List of Characters")]
    [SerializeField] private List<CharSelectionObject> charList = new List<CharSelectionObject>();

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private Image charSplash;
    [SerializeField] private Image backgroundColor;

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
        selectedChar = charList[selectedCharIndex];
        Debug.Log("Selected ship: " + selectedChar.charName);
        dataManager.charSprite = selectedChar.sprite;
        dataManager.charName = selectedChar.charName;
        SceneManager.LoadScene("Game");
    }

    private void UpdateCharSelectionUI()
    {
        charSplash.sprite = charList[selectedCharIndex].sprite;
        charName.text = charList[selectedCharIndex].charName;
        desiredColor = charList[selectedCharIndex].charColor;
    }

    [System.Serializable]
    public class CharSelectionObject
    {
        public Sprite sprite;
        public string charName;
        public Color charColor;
    }
}
