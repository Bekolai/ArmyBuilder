using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;
    [SerializeField]
    private Animator Animator = null;

    private const string IsWalking = "Walk";

    private Coroutine FollowCoroutine;
    bool isDied;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
       Player = GameObject.Find("Player").transform;
    }

    public void StartChasing()
    {
        if (FollowCoroutine == null)
        {
            FollowCoroutine = StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
        }
    }
    private void Start()
    {
      //  setTarget(Player);
    }
    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);

        while (!isDied)
        {
            Agent.SetDestination(Player.transform.position);
            yield return Wait;
        }
    }

    private void Update()
    {
        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
    }
    public void setTarget(Transform target)
    {
        FollowCoroutine = null;
        Player = target;
        StartChasing();
    }
    public void Died()
    {
        isDied = true;
    }
}