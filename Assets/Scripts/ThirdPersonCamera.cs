using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float zoomSpeed = 2f;
    public float rotationSpeed = 200f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    
    private float currentYaw = 0f;

    private void LateUpdate()
    {
        //zoom
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        
        //rotate
        if (Input.GetMouseButton(1))
        {
            currentYaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            //print(currentYaw);
        }
        
        var rotation = Quaternion.Euler(20f, currentYaw, 0);
        var offset = rotation * Vector3.forward * -distance;
        
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
