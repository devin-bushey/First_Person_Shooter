using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyStates { alive, dead };

public class Enemy : MonoBehaviour
{
    private EnemyStates state;
    public float IdleTime { get; private set; } = 5.0f;
    public float PatrolTime { get; private set; } = 8.0f;
    public float DieTime { get; private set; } = 4.5f;
    public float ChaseRange { get; private set; } = 8.0f;
    public float AttackRange { get; private set; } = 7.0f;
    public float AttackRangeStop { get; private set; } = 10.0f;

    public GameObject Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public List<Transform> Waypoints { get; private set; }
    private int waypointIndex = 0;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPt;
    private float projectileForwardForce = 30;
    private float projectileUpForce = 7;

    private int health;
    private int maxHealth = 5;
    [SerializeField] private Image healthBar;

    [SerializeField] GameObject[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Waypoints = new List<Transform>();
        this.state = EnemyStates.alive;
        health = maxHealth;

        foreach (GameObject wp in waypoints)
        {
            Waypoints.Add(wp.transform);
        }

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, AttackRange);
    //    Gizmos.DrawWireSphere(transform.position, ChaseRange);
    //    Gizmos.DrawWireSphere(transform.position, AttackRangeStop);
    //}

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPt.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileForwardForce, ForceMode.Impulse);
        rb.AddForce(transform.up * projectileUpForce, ForceMode.Impulse);
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    public void DetermineNextWaypoint()
    {
        waypointIndex = Random.Range(0, Waypoints.Count);
    }

    public Vector3 GetCurrentWaypoint()
    {
        return Waypoints[waypointIndex].position;
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    public void ReactToHit()
    {
        if (this.state == EnemyStates.alive)
        {
            Debug.Log("Enemy Hit");
            health -= 1;
            Debug.Log("Enemy Health: " + health);
            if (health == 0)
            {
                if (health <= 0)
                {
                    Enemy enemyAI = GetComponent<Enemy>();
                    if (enemyAI != null)
                    {
                        enemyAI.ChangeState(EnemyStates.dead);
                    }

                    Animator enemyAnimator = GetComponentInChildren<Animator>();
                    if (enemyAnimator != null)
                    {
                        enemyAnimator.SetTrigger("Die");
                    }
                    this.state = EnemyStates.dead;
                    Messenger.Broadcast(GameEvent.ENEMY_DEAD);
                }
            }
            float percent = (float)health / maxHealth;
            healthBar.fillAmount = percent;
        }
    }

}
