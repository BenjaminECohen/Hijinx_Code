using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is to be attached to the player
public class KeyInventory : MonoBehaviour
{
    public KeyManager_GameController GameController;
    public PUpManager_GameManager powerupManager;
    public int keyAmount = 10;
    private GameObject[] keys;
    // Start is called before the first frame update
    void Start()
    {
        keys = new GameObject[keyAmount];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            collectKey(collision.gameObject);
            GameController.addNewKeyToUI(collision.gameObject.GetComponent<KeyInfo>());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Powerup"))
        {
            powerupManager.pickupPowerUp();
            Destroy(collision.gameObject);
        }
    }

    //Called on Key Pickup
    public void collectKey(GameObject newKey)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if(keys[i] == null)
            {
                keys[i] = newKey;
                break;
            }
        }
    }
    public void useKey()
    {

    }
    public void useKey(string keyName)
    {
        GameController.removeKey(keyName);
    }

}
