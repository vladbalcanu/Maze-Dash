using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{

    public NavMeshAgent agent;
    public EnemyAnimatorManager animatorManager;
    public Transform player;

    public LayerMask isGround, isPlayer;
    public HealthBar healthBar;

    public int health;

    // Patrula inamic
    public Vector3 walkPoint, firecrackerPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float delay;
    public DateTime dateTime;

    // Atac al inamicului
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange = 15, attackRange = 5;
    public bool playerInAttackRange, playerInFov;
    public bool isPatroling, isChasing, isAttacking, isHearingFirecracker;

    //FOV
    [Range(5, 15)]
    public float fovRadius;
    [Range(0, 360)]
    public float fovAngle;
    public GameObject playerRef;
    public PlayerLocomotion playerLocomotion;
    public bool canSeePlayer;
    public LayerMask targetMask, obstructionMask, firecrackerMask;

    private void Awake()
    {
        playerRef = GameObject.Find("Main Character");
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        player = playerRef.transform;
        playerLocomotion = playerRef.GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<EnemyAnimatorManager>();
        agent = GetComponent<NavMeshAgent>();
        delay = 3.0f;
        timeBetweenAttacks = 5.0f;
        isHearingFirecracker = false;
    }

    private void PlayerInFov()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovRadius, targetMask);
        if(rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2 )
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }
    private void Update()
    {
        IsHearingFirecracker();
        if (isHearingFirecracker)
        {
            goToFirecracker();
        }
        else
        {
            PlayerInFov();
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

            if (!canSeePlayer)
            {
                Patroling();
            }
            if (canSeePlayer && !playerInAttackRange)
            {
                ChasePlayer();
            }
            if (canSeePlayer && playerInAttackRange)
            {
                AttackPlayer();
            }
        }
        animatorManager.UpdateAnimatorValues(isPatroling, isChasing, isAttacking);
    }

    private void IsHearingFirecracker()
    {
        Collider[] firecracker = Physics.OverlapSphere(transform.position, 360.0f, firecrackerMask);

        if (firecracker.Length > 0)
        {

            isHearingFirecracker = true;
            firecrackerPoint = firecracker[0].transform.position;
            Invoke(nameof(ResetFirecracker), 9.0f);
        }
    }

    private void ResetFirecracker()
    {
        isHearingFirecracker = false;
    }

    private void goToFirecracker()
    {
        isPatroling = false;
        isChasing = true;
        isAttacking = false;
        agent.speed = 3.0f;
        agent.SetDestination(firecrackerPoint);

    }

    private void Patroling()
    {
        isPatroling = true;
        isChasing = false;
        isAttacking = false;
        agent.speed = 1.5f;

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround) && !Physics.Raycast(walkPoint, transform.up, 2f, obstructionMask))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player.transform);
        agent.speed = 3.0f;
        isPatroling = false;
        isChasing = true;
        isAttacking = false;
    }

    private void AttackPlayer()
    {
        isPatroling = false;
        isChasing = false;
        isAttacking = true;

        agent.SetDestination(transform.position); // inamicul nu se misca in timp ce ataca
        transform.LookAt(player); // inamicul se uita la player\

        if (!alreadyAttacked)
        {
            animatorManager.PlayTargetAnimation("Attack", true);
            playerLocomotion.HandleHit();            
            healthBar.TakeDamage(20);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
