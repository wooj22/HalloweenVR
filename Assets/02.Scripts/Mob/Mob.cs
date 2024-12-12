using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    public float destroyDelay = 1;
    private bool isDestroyed = false;

    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    private void Start()
    {
        OnCreated?.Invoke();
        //Invoke(nameof(Destroy), 3);

        MobManager.Instance.OnSpawned(this);
    }

    // Destroy
    public void Destroy()
    {
        if (isDestroyed)
            return;
        isDestroyed = true;

        Destroy(gameObject, destroyDelay);
        OnDestroyed?.Invoke();

        MobManager.Instance.OnDestroyed(this);
    }
}
