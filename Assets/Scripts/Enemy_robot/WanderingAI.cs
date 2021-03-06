using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public enum EnemyStates { alive, dead };

public class WanderingAI : MonoBehaviour
{

    private EnemyStates state;

    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;

    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    private float enemySpeed = 1.75f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

    private float baseSpeed = 0.25f;
    float difficultySpeedDelta = 0.3f; // the change in speed per level of difficulty

    public NavMeshAgent Agent { get; set; }
    public float ChaseRange { get; private set; } = 10.0f;
    public GameObject Player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        this.state = EnemyStates.alive;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    void Update()
    {
        if (this.state == EnemyStates.alive)
        {
            Animator enemyAnimator = GetComponent<Animator>();
            if (GetDistanceFromPlayer() < ChaseRange)
            {
                Agent.SetDestination(Player.transform.position);
                enemyAnimator.SetBool("isChasing", true);
            }
            else
            {
                enemyAnimator.SetBool("isChasing", false);
            }

            // Move Enemy
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
            // generate Ray
            Ray ray = new Ray(transform.position, transform.forward);
            // Spherecast and determine if Enemy needs to turn
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }

        }
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        Debug.DrawLine(transform.position, rangeTest);
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    public void SetDifficulty(int newDifficulty) 
    {
        OnDifficultyChanged(newDifficulty);
    }

    void OnDifficultyChanged(int difficulty)
    {
        Debug.Log("WanderingAI.setDifficulty(" + difficulty + ")");
        enemySpeed = baseSpeed + (difficulty * difficultySpeedDelta);
    }

}
