using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScourgerBossScript : MonoBehaviour
{
    public const float throwDistance = 50f;

    [HideInInspector]
    public Transform playerTarget;
    [HideInInspector]
    public Transform currentTarget;


    [HideInInspector]
    public float attackDistance;
    private float pathUpdateDeadLine;

    public ScourgerAxeScript axeScript;

    [Header("AI and Animator")]
    public Animator animator;
    public NavMeshAgent navMeshagent;

    [Header("Stats")]
    public float pathUpdateDelay = 0.2f;
    public float targetDistance;
    public bool inRange = true;
    public bool isAtacking = false;
    public bool wasLongAttack = false;

    [Header("Slash Stats")]
    public GameObject slashPrefab;
    [SerializeField]
    private float slashSpeed = 20f;


    private void Awake()
    {
        navMeshagent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        attackDistance = navMeshagent.stoppingDistance;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = playerTarget;
        targetDistance = Vector3.Distance(transform.position, playerTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
       if(currentTarget != null)
       {
            targetDistance = Vector3.Distance(transform.position, currentTarget.position);
            inRange =  targetDistance <= attackDistance;
            
       }
        axeScript.isAttacking = isAtacking;
        animator.SetBool("IsAttacking", isAtacking);
        animator.SetFloat("Speed", navMeshagent.desiredVelocity.sqrMagnitude);
        animator.SetFloat("Distance", targetDistance);
    }

    public void LookAtTarget()
    {
        Vector3 lookPos = playerTarget.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    public void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            pathUpdateDeadLine = Time.time + pathUpdateDelay;
            navMeshagent.SetDestination(currentTarget.position); 
        }
    }

    public void ThrowSlash()
    {
        GameObject slashObj = Instantiate(slashPrefab, transform.position + Vector3.up * 5f, Quaternion.identity);
        Vector3 lookPos = playerTarget.position - slashObj.transform.position;
        lookPos.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(-lookPos);
        slashObj.transform.rotation = lookRot;
        slashObj.GetComponent<Rigidbody>().velocity = Vector3.Normalize(lookPos) * slashSpeed;
        Destroy(slashObj, 5f);
    }

}
