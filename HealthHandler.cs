using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    //ATTACH THIS SCRIPT TO THE GAME MANAGER
    public Image healthBar;
    public Text healthPercentage;
    public bool showPercentage = false;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthPercentage.gameObject.SetActive(showPercentage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthBar(float health, float damageDealt)
    {
        health -= damageDealt;
        healthBar.fillAmount -= damageDealt / maxHealth;
        float healthDecimal = health / maxHealth;
        float percent = healthDecimal * 100.0f;
        healthPercentage.text = percent.ToString() + "%";
    }

    public void setMaxHealth(float input)
    {
        maxHealth = input;
    }
}
