using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is to be attached to a GameController/Manager
public class KeyManager_GameController : MonoBehaviour
{
    public GameObject keyListEmpty;
    public Image keyImagePrefabStandin;

    public KeyInventory playerKeyInventory;

    public string nameOfKeyToRemove;

    private Image[] keyUIList;
    private string[] keyUIListNames;
    // Start is called before the first frame update
    void Start()
    {
        keyUIList = new Image[playerKeyInventory.keyAmount];
        keyUIListNames = new string[playerKeyInventory.keyAmount];
    }

    //TEST UPDATE
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            removeKey("Key1");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            removeKey("Key2");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            removeKey("Key3");
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            removeKey(nameOfKeyToRemove);
        }
    }


    //Called on Key Pickup 
    public void addNewKeyToUI(KeyInfo info)
    {
        for (int i = 0; i < keyUIList.Length; i++)
        {
            if (keyUIList[i] == null)
            {            
                Image newKey = Instantiate(keyImagePrefabStandin, keyListEmpty.transform);
                newKey.transform.SetParent(keyListEmpty.transform, false);
                newKey.gameObject.name = info.key.name;
                newKey.sprite = info.key.sprite;
                newKey.color = info.key.color;
                
                Vector3 newPosition = new Vector3(newKey.GetComponent<RectTransform>().rect.width * i,
                    newKey.GetComponent<RectTransform>().localPosition.y, newKey.GetComponent<RectTransform>().localPosition.z);

                newKey.GetComponent<RectTransform>().localPosition = newPosition;
                keyUIList[i] = newKey;
                keyUIListNames[i] = info.key.name;
                break;
            }
        }
    }

    public bool findKey(string keyName)
    {
        for (int i = 0; i < keyUIListNames.Length; i++)
        {
            if (keyUIListNames[i] == keyName)
            {
                return true;
            }
        }
        return false;

    }

    public void removeKey(string keyName)
    {
        for (int i = 0; i < keyUIList.Length; i++)
        {
            if (keyUIListNames[i] == null)
            {
                Debug.Log("No such key is being held");
                break;
            }
            if (keyUIListNames[i] == keyName)
            {         
                keyUIListNames[i] = null;
                Destroy(keyUIList[i].gameObject);
                keyUIList[i] = null;
                rearrangeKeys();     
                break;
            }
        }
    }

    private void rearrangeKeys()
    {
        for(int i = 0; i < keyUIListNames.Length; i++)
        {
            if(keyUIListNames[i] == null && i != keyUIListNames.Length - 1)
            {
                for (int j = i + 1; j < keyUIListNames.Length; j++)
                {
                    if (keyUIListNames[j] != null)
                    {
                        
                        keyUIListNames[i] = keyUIListNames[j];
                        keyUIList[i] = keyUIList[j];
                        // 0 => 0, 1 => 200, 2 ==> 400
                        keyUIList[i].gameObject.GetComponent<RectTransform>().localPosition = 
                            new Vector3((keyUIList[i].gameObject.GetComponent<RectTransform>().rect.width * i)
                            , keyUIList[i].gameObject.GetComponent<RectTransform>().localPosition.y,
                            keyUIList[i].gameObject.GetComponent<RectTransform>().localPosition.z);

                        keyUIListNames[j] = null;
                        keyUIList[j] = null;
                        break;
                    }
                }
            }
        }
    }
    
}
