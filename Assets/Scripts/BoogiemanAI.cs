using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoogiemanAI : MonoBehaviour
{
    
    public enum FSMStates
    {
        Idle,
        Chase, 
        Attack, 
    }

    public FSMStates currentState;
    public float attackDistance = 2.0f;
    public float enemySpeed = 2.0f;
    public float chaseDistance = 5.0f;
    public GameObject player;
    //public AudioClip deadVFX;
    public AudioClip growlingSFX;
    public Transform enemyEyes;
    public float fieldOfView = 150f;

    
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;

    //EnemyHealth enemyHealth;
    //int health;

    int currentDestinationIndex = 0;
    NavMeshAgent agent;
    

    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        //enemyHealth = GetComponent<EnemyHealth>();
        
        //health = enemyHealth.currentHealth;
        
        agent = GetComponent<NavMeshAgent>();

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, 
            player.transform.position);

        //health = enemyHealth.currentHealth;

        switch(currentState)
        {
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;       
        }

    }

    void Initialize()
    {
        anim.SetInteger("animState", 1);
        currentState = FSMStates.Chase;
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 1);

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;

        agent.speed = 5f;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {

        nextDestination = player.transform.position;
        
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
            
        }
        else if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("animState", 2);

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 
            10 * Time.deltaTime);
    }

    private void OnDestroy()
    {
        //Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    private void OnDrawGizmos()
    {
        //attack 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //chase
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        //eyes
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        //draw lines
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit; 

        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    return true;
                }

                return false;
            }

            return false;
        }

        return false;
    }
}
