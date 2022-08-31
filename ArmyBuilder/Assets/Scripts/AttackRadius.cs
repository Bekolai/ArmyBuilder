using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider Collider;
    List<IDamageable> Damageables = new List<IDamageable>();
    public int Damage = 10;
    public float AttackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable Target);
    public AttackEvent OnAttack;
    private Coroutine AttackCoroutine;
    [SerializeField] EnemyMovement enemyMovement;
    public bool isPlayer,isSoldier;
    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            
            Damageables.Add(damageable);

            if (AttackCoroutine == null)
            {
                if(!isPlayer)
            {
                enemyMovement.setTarget(other.transform);
                //Debug.Log(other.gameObject.name);
            }
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if (Damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
                if (!isPlayer && isSoldier)
                {
                    enemyMovement.setTarget(WarLevel.Instance.SetTargetEnemy());

                }
                else if (!isPlayer && !isSoldier)
                {
                    enemyMovement.setTarget(WarLevel.Instance.SetTargetSoldier());

                }
            }
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(AttackDelay);
        WaitForSeconds Wait2 = new WaitForSeconds(AttackDelay/2);

     //   yield return Wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                }
            }
            yield return Wait2;
            AudioManager.Instance.PlaySwordClip();
            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(Damage);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return Wait2;

            Damageables.RemoveAll(DisabledDamageables);
        }
        if (!isPlayer && isSoldier)
        {
            enemyMovement.setTarget(WarLevel.Instance.SetTargetEnemy());
            
        }
        else if(!isPlayer && !isSoldier)
        {
            enemyMovement.setTarget(WarLevel.Instance.SetTargetSoldier());
      
        }
        AttackCoroutine = null;
    }

    private bool DisabledDamageables(IDamageable Damageable)
    {  
        //  return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
        return Damageable != null && !Damageable.IsAlive();
        

    }
   public void UpdateDamage(int damage)
    {
        Damage = damage;
    }
}