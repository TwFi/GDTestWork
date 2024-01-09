using System;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] AttackConfig AllAttacks;
    [SerializeField] HPBar hpBar;

    [SerializeField] float AttackRange = 2;
    [SerializeField] int hpReward = 2;

    private void Start()
    {
        OnDie += EntityDied;
    }

    private void EntityDied(Entity entity)
    {
        if (entity is Enemie)
        {
            Hp += hpReward;
            hpBar.Init(Hp);
        }
    }

    public void Attack (int attack)
    {
        if (Enum.IsDefined(typeof(Attack.Types), attack))
        {
            Attack.Types type = (Attack.Types)attack;
            Attack(type);
        }
        else
            Debug.LogError($"Wrong attack type {attack}");
    }

    public void Attack(Attack.Types attackType)
    {
        if (CheckAttackAnimationIsPlay())
            return;

        if (!AllAttacks.TryGetAttack(attackType, out Attack attack))
            return;

        if (TryGetNearestEnemy(out Enemie nearestEnemy))
        {
            nearestEnemy.Hit(attack.Damage);

            transform.transform.rotation = Quaternion.LookRotation(nearestEnemy.transform.position - transform.position);
            AnimatorController.SetTrigger(attack.AnimationParameter);
        }
        else
        {
            AnimatorController.SetTrigger(attack.AnimationParameter);
        }

        bool CheckAttackAnimationIsPlay()
        {
            foreach (var _attack in AllAttacks.Attacks)
            {
                string animName = _attack.AnimationName;

                if (AnimatorController.GetCurrentAnimatorStateInfo(0).IsName(animName))
                    return true;
                if (AnimatorController.GetNextAnimatorStateInfo(0).IsName(animName))
                    return true;
            }

            return false;
        }
    }

    public bool CheckNearestEnemy()
    {
        return TryGetNearestEnemy(out Enemie nearestEnemy);
    }

    private bool TryGetNearestEnemy(out Enemie nearestEnemy)
    {
        var enemies = SceneManager.Instance.Enemies;
        nearestEnemy = null;
        float lastDistanceToEnemy = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < AttackRange && distanceToEnemy < lastDistanceToEnemy)
            {
                nearestEnemy = enemy;
                lastDistanceToEnemy = distanceToEnemy;
            }
        }
        
        return nearestEnemy is null ? false : true;
    }
    protected override void Die()
    {
        base.Die();

        SceneManager.Instance.GameOver();
    }

    private void OnDestroy()
    {
        OnDie -= EntityDied;
    }
}
