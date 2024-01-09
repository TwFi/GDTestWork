using UnityEngine;

public class DieEffect : MonoBehaviour
{
    [SerializeField] GameObject particleObject;
    [SerializeField] Entity entity;

    private void Start()
    {
        particleObject.SetActive(false);
        entity.OnDie += EntityDied;
    }

    private void EntityDied(Entity ent)
    {
        particleObject.SetActive(true);
    }

    private void OnDestroy()
    {
        entity.OnDie -= EntityDied;
    }
}