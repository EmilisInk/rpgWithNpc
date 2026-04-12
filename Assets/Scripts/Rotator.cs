using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float amplitude = 2;
    public float sinSpeed = 2;
    public float rotationSpeed = 50;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time * sinSpeed) * amplitude, transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 0);
        
        
    }
}
