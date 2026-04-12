using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public LayerMask groundLayer;
    public Transform aimIndicator;

    [Header("Interactions")] 
    public float stoppingDistance = 2;
    public float interactDistance = 2.3f;

    [Header("Combat")] 
    public float attackInterval = 1.5f;
    public float attackRange = 2.5f;

    private float attackTimer = 0;
    private Transform currentEnemy;
    private Target target;
    private NavMeshAgent agent;
    private Inventory inventory;
    private Equipment equipment;
    
    void Start()
    {
        equipment = GetComponent<Equipment>();
        inventory = GetComponent<Inventory>();
        agent = GetComponent<NavMeshAgent>();
        attackTimer = attackInterval;
    }

    void Update()
    {
        HandleInput();
        HandleInteractions();
        HandleCombat();
    }

    
    void HandleCombat()
    {
        if (currentEnemy == null) return;
        
        var distance = Vector3.Distance(transform.position, currentEnemy.position);
        
        //if enemy moves away -> chase 
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(currentEnemy.position);
            return;
        }
        
        //stop moving
        agent.isStopped = true;
        
        //face enemy
        var direction = (currentEnemy.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        
        //attack cooldown
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0;

            var enemyHealth = currentEnemy.GetComponent<Enemy>();
            enemyHealth.TakeDamage(equipment.GetDamage());

            if (enemyHealth.IsDead())
            {
                currentEnemy = null;
                target = null;
                agent.isStopped = false;
            }
        }
    }


    void HandleInteractions()
    {
        if (target == null || target.transform == null) return;

        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= interactDistance) 
        {
            switch (target.type)
            {
                case TargetType.Enemy:
                    StartCombat(target.transform);
                    break;
                case TargetType.Item:
                    PickupItem(target.transform);
                    target = null;
                    break;
            }
        }
    }

    private void StartCombat(Transform enemy)
    {
        currentEnemy = enemy;
    }

    private void PickupItem(Transform item)
    {
        //print("Picked up " + item.name);
        var worldItem = item.GetComponent<ItemWorld>();
        inventory.AddItem(worldItem.data);
        Destroy(item.gameObject);
    }


    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //cancel everything
            currentEnemy = null;
            agent.isStopped = false;
                      
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
            {
                if (hit.collider.tag == "Enemy")
                {
                    agent.stoppingDistance = stoppingDistance;
                    SetTarget(hit.collider.transform, TargetType.Enemy);
                }
                else if (hit.collider.tag == "Item")
                {
                    agent.stoppingDistance = stoppingDistance;
                    SetTarget(hit.collider.transform, TargetType.Item);
                }
                else
                {
                    agent.stoppingDistance = 0;
                    SetTarget(null, TargetType.Ground);
                    agent.SetDestination(hit.point);
                    aimIndicator.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                    aimIndicator.forward = hit.normal;
                    aimIndicator.gameObject.SetActive(true);
                }
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            aimIndicator.gameObject.SetActive(false);
        }
    }

    
    void SetTarget(Transform t, TargetType type)
    {
        target = new Target{transform = t, type = type};

        if (t != null)
        {
            agent.SetDestination(t.position);
        }
    }
}
