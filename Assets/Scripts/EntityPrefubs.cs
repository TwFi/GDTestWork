using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entities", menuName = "Data/EntityPrefubs")]
public class EntityPrefubs : ScriptableObject
{
    [Serializable]
    private struct PrefubContainer
    {
        public Enemie.Types type;
        public GameObject prefub;
    }

    [SerializeField] PrefubContainer[] Entities;

    Dictionary<Enemie.Types, GameObject> entities;

    private void FillEntities()
    {
        entities = new ();

        foreach(var container in Entities)
        {
            if (!entities.TryAdd(container.type, container.prefub))
                Debug.LogError($"Attemp add same type [{container.type}] entity in entity prefubs");
        }
    }

    public bool TryGetPrefub(Enemie.Types type, out GameObject prefub)
    {
        if (entities is null)
            FillEntities();

        if (!entities.ContainsKey(type))
            Debug.LogError($"Entity prefubs dont have [{type}]");

        return entities.TryGetValue(type, out prefub);
    }
}
