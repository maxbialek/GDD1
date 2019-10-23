using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barImage;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        barImage.fillAmount = currentHealth / maxHealth;
    }
}
