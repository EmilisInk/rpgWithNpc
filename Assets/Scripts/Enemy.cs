using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Transform healthbar;
    public int health = 50;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ChangeDirection());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        health = Mathf.Clamp(health, 0, 100);
        healthbar.localScale = new Vector3(health / 50f, healthbar.localScale.y, 1);

        if (health <= 0)
        {
            //TODO: death animation
            //TODO: drop loot
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    IEnumerator ChangeDirection()
    {
        //set random x, z
        var destination = new Vector3();
        destination.x = Random.Range(5, 45);
        destination.y = 500;
        destination.z = Random.Range(5, 45);
        
        //find surface
        if (Physics.Raycast(destination, Vector3.down, out RaycastHit hit))
        {
            destination.y = hit.point.y;
        }
        agent.SetDestination(destination);

        yield return new WaitForSeconds(Random.Range(15, 30));
    }
}
