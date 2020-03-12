using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public float tempPlayerBulletDmg = 3;
    public Image healthBar;
    public Text healthPercentage;
    public bool showPercentage = false;
    private GameObject currentPhase;

    [System.Serializable]
    public class Phases
    {
        public GameObject[] phases;
        
    }
    private Phases phase;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthPercentage.gameObject.SetActive(showPercentage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Attack")
        {
            //Take damage
            health -= tempPlayerBulletDmg;
            healthBar.fillAmount -= tempPlayerBulletDmg / maxHealth;
            float healthDecimal = health / maxHealth;     
            float percent = healthDecimal * 100.0f;
            healthPercentage.text = percent.ToString() + "%";
        }
    }
}
