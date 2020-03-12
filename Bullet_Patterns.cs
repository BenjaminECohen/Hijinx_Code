using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet_Patterns : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem[] partSystems;
    private Rotator rotator;

    private bool useRotation = false;
    private float rotationModifier = 0.0f;

    [System.Serializable]
    private class OriginalPartSettings
    {
        public int maxParticles = 0;
        public float startSpeed = 0;
        public float emissionRate = 0;
        public float startLifetime = 0;
    }
    private OriginalPartSettings[] origPartSettings;

    [System.Serializable]
    public class Spiral
    {
        [Tooltip("Size: Number of emitters you want to be active for the formation.\nElements: Enter in the emitters that you want" +
                " to be active. Emitters follow clock time conventions starting at 0 (0:00/12:00) to 11.5 (11.30).")]
        public float[] emitterArray;
        [Tooltip("Size: Number of emitters you want to give a color.\nElements: Enter in the emitters that you want" +
            " to be colored. Emitters follow clock time conventions starting at 0 (0:00/12:00) to 11.5 (11.30).")]
        public float[] coloredEmitters;
        [Tooltip("True if you want the pattern to rotate")]
        public bool useRotation = false;
        [Tooltip("Positive values to rotate counterclockwise, negative to rotate clockwise, use zero to disable rotation")]
        public float rotationModifier = 0.0f;
        [Tooltip("Set the starting angle of the emission (Based on z axis rotation)")]
        public float startAngle = 0.0f;
        [Range(0.0f, 1.0f)]
        public float red = 1.0f;
        [Range(0.0f, 1.0f)]
        public float blue = 1.0f;
        [Range(0.0f, 1.0f)]
        public float green = 1.0f;

        [Tooltip("How many particles per emitter can be on the screen at once")]
        public int maxParticles = 100;
        [Tooltip("Velocity of the particles at the start of their lives")]
        public float startSpeed = 5.0f;
        [Tooltip("X amount of particles emitted per second")]
        public float emissionRate = 5.0f;
        [Tooltip("Particles will live for X seconds")]
        public float startLifetime = 5.0f;
    }

    public Spiral spiral1;
    public Spiral spiral2;

    [System.Serializable]
    public class WaveBlast
    {
        [Tooltip("Size: Number of emitters you want to be active for the formation.\nElements: Enter in the emitters that you want" +
        " to be active. Emitters follow clock time conventions starting at 0 (0:00/12:00) to 11.5 (11.30).")]
        public float[] emitterArray;
        [Tooltip("Size: Number of emitters you want to give a color.\nElements: Enter in the emitters that you want" +
            " to be colored. Emitters follow clock time conventions starting at 0 (0:00/12:00) to 11.5 (11.30).")]
        public float[] coloredEmitters;
        [Tooltip("True if you want the pattern to rotate")]
        public bool useRotation = false;
        [Tooltip("Positive values to rotate counterclockwise, negative to rotate clockwise, use zero to disable rotation")]
        public float rotationModifier = 0.0f;
        [Tooltip("Set the starting angle of the emission (Based on z axis rotation)")]
        public float startAngle = 0.0f;
        [Range(0.0f, 1.0f)]
        public float red = 1.0f;
        [Range(0.0f, 1.0f)]
        public float blue = 1.0f;
        [Range(0.0f, 1.0f)]
        public float green = 1.0f;

        [Tooltip("How many particles per emitter can be on the screen at once")]
        public int maxParticles = 100;
        [Tooltip("Velocity of the particles at the start of their lives")]
        public float startSpeed = 5.0f;
        [Tooltip("X amount of particles emitted per second")]
        public float emissionRate = 5.0f;
        [Tooltip("Particles will live for X seconds")]
        public float startLifetime = 5.0f;
        [Tooltip("How long to wait between wave blasts (seconds)")]
        public float secondsBetweenBlasts = 3.0f;
        [Tooltip("How long the emitter will emit its particles (seconds)")]
        public float blastDuration = 1.0f;
        [HideInInspector]
        public bool controlSwitch = true;
        [HideInInspector]
        public bool waveBlastOn = false;
    }
    public WaveBlast waveBlast;

    //Changing variable that keeps track of  time
    private float bulletIntervalCounter = 0.0f;
    //The interval between bullet spawning
    private float bulletIntervalTime = 0.0f;
    //The length of time that bullets are being spawned
    private float bulletPlayTime = 0.0f;
    //The bool that determines that it is time for bullets to be emitted
    private bool bulletPlay = true;



    void Start()
    {
        //int[] output = new int[input.Length];
        //filters particle systems into an array
        partSystems = GetComponentsInChildren<ParticleSystem>();


        //saves the original particle settings of the systems.
        origPartSettings = new OriginalPartSettings[partSystems.Length];


        //Array has null values. Populate the array with its own individual settings
        for (int i = 0; i < origPartSettings.Length; i++)
        {
            origPartSettings[i] = new OriginalPartSettings();
        }
        
        
        for (int i = 0; i < partSystems.Length; i++)
        {
            origPartSettings[i].maxParticles = partSystems[i].maxParticles;
            origPartSettings[i].startSpeed = partSystems[i].startSpeed;
            origPartSettings[i].emissionRate = partSystems[i].emissionRate;
            origPartSettings[i].startLifetime = partSystems[i].startLifetime;
        }

        rotator = GetComponent<Rotator>();

        for (int i = 0; i < partSystems.Length; i++)
        {
            Debug.Log("Stopping");
            partSystems[i].Stop();
        }
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        //===================================================
        //INPUT USED FOR TESTING 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSpiral1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSpiral2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWaveBlast();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBullets();
        }
        //===================================================
        
        if (useRotation)
        {
            rotator.Rotation(rotationModifier);
        }
        else
        {
            rotator.Rotation(0.0f);
        }

        //Calls the waveblast during the correct intervals and for the assigned amount of time
        if (waveBlast.waveBlastOn)
        {
            WaveBlastControl(ArrayConversion(waveBlast.emitterArray));
        }
    }

    public void ActivateSpiral1()
    {
        ResetEmitters(); //Stops the emmition of any current emitters
        rotator.EmitterRotationSet(spiral1.startAngle);
        useRotation = spiral1.useRotation;
        rotationModifier = spiral1.rotationModifier;
        ChangeParticleColor(spiral1.coloredEmitters, spiral1.red, spiral1.blue, spiral1.green); //Edit colors of emitters
        SpiralPattern(ArrayConversion(spiral1.emitterArray), spiral1);  //Starts the emitters chosen   
    }

    public void ActivateSpiral2()
    {
        ResetEmitters(); //Stops the emmition of any current emitters
        rotator.EmitterRotationSet(spiral2.startAngle);
        useRotation = spiral2.useRotation; //If a rotation is used
        rotationModifier = spiral2.rotationModifier; //New Rotition modifier
        ChangeParticleColor(spiral2.coloredEmitters, spiral2.red, spiral2.blue, spiral2.green); //Edit colors of emitters
        SpiralPattern(ArrayConversion(spiral2.emitterArray), spiral2); //Starts the emitters chosen
    }

    public void ActivateWaveBlast()
    {
        ResetEmitters();
        rotator.EmitterRotationSet(waveBlast.startAngle);
        useRotation = waveBlast.useRotation;
        rotationModifier = waveBlast.rotationModifier;
        bulletPlayTime = waveBlast.blastDuration;
        bulletIntervalTime = waveBlast.secondsBetweenBlasts;
        ChangeParticleColor(waveBlast.coloredEmitters, waveBlast.red, waveBlast.blue, waveBlast.green);
        waveBlast.waveBlastOn = true; //This is used instead of directly emitting the particles since waveblast occurs and is based on the length of time between blasts and how long blasts happen
    }

    public void ResetBullets()
    {
        ResetEmitters();
        useRotation = false;
        rotationModifier = 0.0f;
        rotator.StopRotation();
    }


    //convert an inputed array in the form of clock coordinates to respective clock
    //position of the particle system in the partSystems array
    int[] ArrayConversion(float[] input)
    {
        int[] output = new int[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = (int)(input[i] * 2);
        }

        return output;
    }

    void ResetEmitters()
    {
        //Resets all the timing variables for certain patterns to default values
        BulletIntervalReset();
        //Booleans calling specific patterns are turned off
        WaveBlastValueReset(waveBlast);

        rotator.ResetRotation();
        ResetColor();
        ResetEmissionProperties();
        for (int i = 0; i < partSystems.Length; i++)
        {
            partSystems[i].Stop();           
        }
    }

    void ChangeParticleColor(float[] selectSystems, float r, float g, float b)
    {
        int[] systems = ArrayConversion(selectSystems);
        for (int i = 0; i< systems.Length; i++)
        {
            partSystems[systems[i]].startColor = new Color(r, b, g);
        }
    }

    void ResetColor()
    {
        for (int i = 0; i < partSystems.Length; i++)
        {
            partSystems[i].startColor = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    void ResetEmissionProperties()
    {
        for (int i = 0; i < partSystems.Length; i++)
        {
            partSystems[i].maxParticles = origPartSettings[i].maxParticles;
            partSystems[i].startSpeed = origPartSettings[i].startSpeed;
            partSystems[i].emissionRate = origPartSettings[i].emissionRate;
            partSystems[i].startLifetime = origPartSettings[i].startLifetime;
        }
    }

    //Spiral Pattern
    void SpiralPattern(int[] systems, Spiral spiral)
    {
        SpiralEmission(systems, spiral);
        for (int i = 0; i < systems.Length; i++)
        {
            partSystems[systems[i]].Play();
        }
    }

    //Waveblast Pattern
    void WaveBlastControl(int[] systems)
    {
        bulletIntervalCounter += Time.fixedDeltaTime;
        //If the play counter is less than total play time and it is play time. Shoot a waveblast
        if (bulletIntervalCounter <= bulletPlayTime && bulletPlay)
        {
            if (waveBlast.controlSwitch)
            {
                WaveBlastEmission(systems, bulletPlay);              
                waveBlast.controlSwitch = false;
            }
        }
        else if (bulletIntervalCounter > bulletPlayTime && bulletPlay)
        {
            bulletPlay = false;
            bulletIntervalCounter = 0.0f;
        }
        else if (bulletIntervalCounter > bulletIntervalTime)
        {
            bulletPlay = true;
            bulletIntervalCounter = 0.0f;
            waveBlast.controlSwitch = true;
        }
        else
        {
            WaveBlastEmission(systems, bulletPlay);
        }
    }
    void WaveBlastEmission(int[] systems, bool playBullets)
    {
        
        //Play Systems
        for (int i = 0; i < systems.Length; i++)
        {     
            partSystems[systems[i]].maxParticles = waveBlast.maxParticles;
            partSystems[systems[i]].startSpeed = waveBlast.startSpeed;
            partSystems[systems[i]].emissionRate = waveBlast.emissionRate;
            partSystems[systems[i]].startLifetime = waveBlast.startLifetime;
            if (playBullets)
            {
                partSystems[systems[i]].Play();
            }
            else
            {
                partSystems[systems[i]].Stop();
            }
            
        }

    }

    void SpiralEmission(int[] systems, Spiral spiral)
    {

        //Play Systems
        for (int i = 0; i < systems.Length; i++)
        {
            partSystems[systems[i]].maxParticles = spiral.maxParticles;
            partSystems[systems[i]].startSpeed = spiral.startSpeed;
            partSystems[systems[i]].emissionRate = spiral.emissionRate;
            partSystems[systems[i]].startLifetime = spiral.startLifetime;
        }
    }

    //Resets the special switches and values of a wave blast
    void WaveBlastValueReset(WaveBlast obj)
    {
        obj.waveBlastOn = false;
        obj.controlSwitch = true;
    }

    //Resets all the timing used in patterns to default values
    void BulletIntervalReset()
    {
        bulletPlay = true;
        bulletIntervalCounter = 0.0f;
        bulletIntervalTime = 0.0f;
        bulletPlayTime = 0.0f;
    }
}

