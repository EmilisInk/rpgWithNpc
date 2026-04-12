using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interaction : MonoBehaviour
{
    private GameObject Zone;
    public Canvas Canvas;

    private void Start()
    {
        Zone = GetComponent<GameObject>();
        Canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Canvas.enabled = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Canvas.enabled = false;
        }
    }
}
