using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{    
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Singletone [{typeof(T)}] must be one!");
            Destroy(gameObject);
        }
        else
            Instance = (T)this;
    }
}
