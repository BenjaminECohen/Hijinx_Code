using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    List<ParticleCollisionEvent> collisionEvents;

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    //Particle triggers
    void OnParticleTrigger()
    {


        int partEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int partInside = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        

        for (int i = 0; i < partEnter; i++)
        {
            //DO DAMAGE!!!!
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit the player");
            //other.GetComponent<Player_Health_Segmented>().TakeDamage();
        }
    }
}
