using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool _doNotDestroyOnLoad;

    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("MonoSingleton: object of type already exists, instance will be destroy = " + typeof(T).Name);
            Destroy(this.gameObject);
            return;
        }

        Instance = this as T;

        if (_doNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}