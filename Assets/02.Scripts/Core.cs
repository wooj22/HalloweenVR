using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    // 체력
    public int maxHP = 10;
    int curHP;

    // 이벤트
    public UnityEvent<string> OnHpChanged;  // 체력변경 이벤트
    public UnityEvent OnHit;                // 피격 이벤트
    public UnityEvent OnDestroy;            // 파괴 이벤트

    // 싱글톤
    static Core instance;

    public static Core Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<Core>();
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        curHP = maxHP;
        UpdateUI();
    }

    /// 몹 피격
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mob"))
        {
            var mob = other.GetComponent<Mob>();
            OnHit?.Invoke();
            DecreaseHP(1);
            mob.Destroy();
        }
    }

    /// 체력 감소
    private void DecreaseHP(int amount)
    {
        if (curHP <= 0)
            return;

        curHP -= amount;
        if(curHP <= 0)
        {
            curHP = 0;
            OnDestroy?.Invoke();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        OnHpChanged?.Invoke($"HP : {curHP}");
    }
}
