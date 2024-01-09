using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public event Action OnHit;
    public event Action<Entity> OnDie;

    [field: SerializeField] public float Hp { get; protected set; }

    [SerializeField] protected Animator AnimatorController;
    protected bool isDead = false;

    private void Update()
    {
        if (!isDead)
            UpdateOnAlive();
    }

    public virtual void Hit(float damage)
    {
        Hp -= damage;

        OnHit?.Invoke();

        if (Hp <= 0)
        {
            Die();
            return;
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");
        OnDie?.Invoke(this);
    }

    protected virtual void UpdateOnAlive() { }
}
