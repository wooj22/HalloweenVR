using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    static MobManager instance;
    public List<Mob> mobs = new List<Mob>();  // 맞나이거

    public static MobManager Instance
    {
        get
        {
            if(instance == null)
                instance = FindObjectOfType<MobManager>();
            return instance;
        }
    }

    public UnityEvent<Mob> OnSpawn;
    public UnityEvent<Mob> OnDestroy;

    private void Awake()
    {
        instance = this;
    }

    public void OnSpawned(Mob mob)
    {
        mobs.Add(mob);
        OnSpawn?.Invoke(mob);
    }

    public void OnDestroyed(Mob mob)
    {
        if (mobs.Remove(mob))
            OnDestroy?.Invoke(mob);
    }

    public void DestroyAll()
    {
        while (mobs.Count > 0)
            mobs[0]?.Destroy();
    }
}
