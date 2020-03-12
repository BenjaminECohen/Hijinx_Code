using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUpManager_GameManager : MonoBehaviour
{
    public Text countText;
    private int powerUpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "x0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickupPowerUp()
    {
        powerUpCount++;
        changeUICount();
    }
    public bool usePowerUp()
    {
        if (powerUpCount > 0)
        {
            powerUpCount--;
            changeUICount();
            return true;
        }
        return false;
    }

    private void changeUICount()
    {
        countText.text = "x" + powerUpCount.ToString();
    }
}
