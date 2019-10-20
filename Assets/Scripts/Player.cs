using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite ship;
    public string charName;
    public DataManager dataManager;

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
        ship = dataManager.charSprite;
        charName = dataManager.charName;
    }

    void Update()
    {
        
    }
}
