using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Player player;
    public Transform bulletPrefab;
    public float shootingRate = 0.25f;
    public float shootCooldown;
    public int ammunitionMax = 30;
    public int ammunitionCurrent;
    public float reloadTime = 1f;
    public float reloadCooldown;

    public TextMeshProUGUI ammoCurrent;
    public RectTransform reloadBar;
    public GameObject reloadBarObject;
    public Image reloadFillBar;

    public ParticleSystem fireEffect;

    public AudioSource gunSound;

    void Start()
    {
        gunSound = GetComponent<AudioSource>();
        gunSound.volume *= 0.5f;
        shootCooldown = 0f;
        reloadCooldown = 0f;
        ammunitionCurrent = ammunitionMax;
        UpdateAmmoCurrent();
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (reloadCooldown > 0)
        {
            reloadFillBar.fillAmount = 1f - reloadCooldown / reloadTime;
            reloadBarObject.SetActive(true);
            reloadCooldown -= Time.deltaTime;
        }
        else
        {
            reloadBarObject.SetActive(false);
            UpdateAmmoCurrent();
        }
    }

    private void LateUpdate()
    {
        Vector3 barPos = Camera.main.WorldToScreenPoint(player.transform.position);
        reloadBar.transform.position = new Vector3(barPos.x, barPos.y - 25f, barPos.z);
    }

    public void Shoot()
    {
        if (shootCooldown <= 0 && reloadCooldown <= 0)
        {
            instantiate(fireEffect, transform.position);
            gunSound.Play();
            ammunitionCurrent--;
            UpdateAmmoCurrent();
            if (ammunitionCurrent == 0)
            {
                reloadCooldown = reloadTime;
                ammunitionCurrent = ammunitionMax;
            }
            shootCooldown = shootingRate;
            var shotTransform = Instantiate(bulletPrefab) as Transform;
            shotTransform.position = new Vector3(transform.position.x + player.GetObjectWidth(), transform.position.y, transform.position.z + 0.1f);
            MoveBullet move = shotTransform.gameObject.GetComponent<MoveBullet>();
            move.direction = new Vector2(1, Random.Range(-0.05f, 0.05f));
        }
    }

    public void UpdateAmmoCurrent()
    {
        ammoCurrent.text = "Ammo: " + ammunitionCurrent.ToString("D2") + "/" + ammunitionMax;
    }

    public void ReloadWeapon()
    {
        reloadCooldown = reloadTime;
        ammunitionCurrent = ammunitionMax;
    }

    public bool IsReloading()
    {
        return reloadCooldown > 0 ? true : false; 
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);
        return newParticleSystem;
    }
}
