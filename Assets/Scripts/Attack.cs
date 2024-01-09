using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Data/Attack")]
public class Attack : ScriptableObject
{
    public enum Types
    {
        Normal,
        Super
    }

    [field: SerializeField] public Types Type { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public string AnimationParameter { get; private set; }
}
