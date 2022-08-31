using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private AttackRadius AttackRadius;
    [SerializeField]
    private Animator Animator;
    private Coroutine LookCoroutine;

    [SerializeField]
    private int Health = 300;
    private const string ATTACK_TRIGGER = "Attack";
    private const string DEATH_TRIGGER = "Death";
    bool isAlive;

    private void Awake()
    {
        AttackRadius.OnAttack += OnAttack;
        isAlive = true;
    }

    private void OnAttack(IDamageable Target)
    {
        Animator.SetTrigger(ATTACK_TRIGGER);

        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }

    private IEnumerator LookAt(Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0)
        {
            isAlive = false;
            Animator.Play(DEATH_TRIGGER);
            WarLevel.Instance.SoldierDied(gameObject);
            AttackRadius.gameObject.SetActive(false);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
   public void UpdateHealth(int health)
    {
        Health = health;
    }
    public bool IsAlive()
    {
        return isAlive;
    }
}