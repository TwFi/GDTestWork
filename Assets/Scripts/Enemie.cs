using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : Entity
{
    public enum Types
    {
        Goblin,
        Matreshka,
        SmallGoblin
    }

    public static event Action<Enemie> OnSpawn;

    [SerializeField] float Damage;
    [SerializeField] float AtackSpeed;
    [SerializeField] float AttackRange = 2;
    const float pathBias = 1f;

    [SerializeField] NavMeshAgent Agent;

    float lastAttackTime = 0;
    bool chassing = false;
    bool wasAttack = false;

    private void Start()
    {
        OnSpawn?.Invoke(this);
    }

    protected override void UpdateOnAlive()
    {
        CombatAI();
    }

    private void CombatAI()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);

        if (distanceToPlayer <= AttackRange)
            AutoAttack();
        else if (!chassing || Agent.path.status == NavMeshPathStatus.PathComplete)
            Chassing();
    }

    private void Chassing()
    {
        chassing = true;

        if (wasAttack)
        {
            wasAttack = false;
            StartCoroutine(DelayedChassingPlayer(AtackSpeed));
        }
        else
            ChassingPlayer();
    }

    private IEnumerator DelayedChassingPlayer(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        ChassingPlayer();
    }

    private void AutoAttack()
    {
        Stop();

        if (Time.time - lastAttackTime > AtackSpeed)
        {
            wasAttack = true;
            lastAttackTime = Time.time;
            SceneManager.Instance.Player.Hit(Damage);
            AnimatorController.SetTrigger("Attack");
        }
    }

    private void ChassingPlayer()
    {
        Agent.isStopped = false;
        Vector3 bias = new Vector3(UnityEngine.Random.Range(-pathBias, pathBias), 0, UnityEngine.Random.Range(-pathBias, pathBias));
        Agent.SetDestination(SceneManager.Instance.Player.transform.position + bias);
        AnimatorController.SetFloat("Speed", Agent.speed);
    }

    private void Stop()
    {
        chassing = false;
        Agent.isStopped = true;
        AnimatorController.SetFloat("Speed", 0);
    }

    protected override void Die()
    {
        base.Die();
        
        Stop();
    }
}
