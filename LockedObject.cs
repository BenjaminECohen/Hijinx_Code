using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : MonoBehaviour
{
    //This script requires the object to have a collider set to trigger
    public string nameOfKeyToUnlock;
    [System.Serializable]
    public class GameObjectToggle
    {
        public bool functionActivated = false;
        public GameObject beforeGameObject;
        public GameObject afterGameObject;

    }
    [System.Serializable]
    public class SpriteSwap
    {
        public bool functionActivated = false;
        public Sprite newSprite;
    }
    [System.Serializable]
    public class DeleteObject
    {
        public bool functionActivated = false;
        public GameObject gameObjectToDestroyOnUnlock;

    }
    [System.Serializable]
    public class DisableCollider
    {
        public bool functionActivated = false;
        public Collider2D collider;
    }

    public GameObjectToggle gameObjectToggle;
    public SpriteSwap spriteSwap;
    public DeleteObject deleteObject;
    public DisableCollider disableCollider;

    private KeyManager_GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyManager_GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameController.findKey(nameOfKeyToUnlock))
        {
            gameController.removeKey(nameOfKeyToUnlock);
            //For GameObject swap
            if (gameObjectToggle.functionActivated)
            {
                gameObjectToggle.beforeGameObject.SetActive(false);
                gameObjectToggle.afterGameObject.SetActive(true);
            }
            //For Sprite Swap
            if (spriteSwap.functionActivated)
            {
                this.GetComponent<SpriteRenderer>().sprite = spriteSwap.newSprite;
            }
            //For Delete Object
            if (deleteObject.functionActivated)
            {
                Destroy(deleteObject.gameObjectToDestroyOnUnlock);
            }
            //For Removing a collider
            if (disableCollider.functionActivated)
            {
                disableCollider.collider.enabled = false;
            }
        }
        
    }
}
