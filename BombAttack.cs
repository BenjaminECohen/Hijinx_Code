using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    private BombController controller;
    public AudioClip boomSound;

    private void Start()
    {
        controller = FindObjectOfType<BombController>();
        ++controller.currBombCount;
    }
    public void destroyObject()
    {
        --controller.currBombCount;
        Destroy(this.gameObject);
    }
    public void playSound()
    {
        this.GetComponent<AudioSource>().PlayOneShot(boomSound);
    }
    
}
