using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack config", menuName = "Data/Attack config")]
public class AttackConfig : ScriptableObject
{
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public bool TryGetAttack(Attack.Types type , out Attack outAttack)
    {
        foreach (var attack in Attacks)
        {
            if (attack != null && attack.Type == type)
            {
                outAttack = attack;
                return true;
            }
        }

        Debug.LogError($"Attack not finded by type: {type}");
        outAttack = null;
        return false;
    }
}
