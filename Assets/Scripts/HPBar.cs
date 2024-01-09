using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] Image iconValue;

    float maxValue = 1;
    
    private void Start()
    {
        Init(entity.Hp);
        entity.OnDie += EntityDie;
        entity.OnHit += EntityHit;
    }

    public void Init(float _maxValue)
    {
        maxValue = _maxValue;
        Set(entity.Hp);
    }

    private void EntityDie(Entity entity)
    {
        Destroy(gameObject);
    }

    private void EntityHit()
    {
        Set(entity.Hp);
    }

    private void Set(float rawValue)
    {
        float value = Mathf.Clamp01(rawValue / maxValue);
        iconValue.fillAmount = value;
    }

    private void OnDestroy()
    {
        entity.OnHit -= EntityHit;
        entity.OnDie -= EntityDie;
    }
}
