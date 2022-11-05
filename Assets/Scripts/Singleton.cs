using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get;private set; }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Duplicate " + nameof(T) + " on obj " + gameObject.name);
            Destroy(this);
        }
        instance = this as T;
    }
}