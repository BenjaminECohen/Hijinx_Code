using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotation(float modifier)
    {
        transform.Rotate(0.0f, 0.0f, Time.fixedDeltaTime * modifier);
    }

    public void StopRotation()
    {
        transform.Rotate(0.0f, 0.0f, 0.0f);
    }

    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void EmitterRotationSet(float z)
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, z);
    }
}
