using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach this script to the key object
public class KeyInfo : MonoBehaviour
{
    [System.Serializable]
    public class Key
    {
        [Tooltip("Make sure that the name is unique. Do not have two keys share the same exact name or bad things can happen!")]
        public string name;
        [Tooltip("If left empty, the script will autofill them with the gameObjects current sprite renderer values.")]
        public Sprite sprite;
        [Tooltip("Set as true if you want to apply the color in the sprite renderer to the UI Image")]
        public bool applyColor = false;
        public Color color;
    }
    public Key key;
    // Start is called before the first frame update
    private void Start()
    {
        if (key.sprite == null)
        {
            key.sprite = this.GetComponent<SpriteRenderer>().sprite;
        }
        if (key.applyColor)
        {
            key.color = this.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            key.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

}
