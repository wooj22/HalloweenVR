using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    // ü��
    public int maxHP = 10;
    int curHP;

    // �̺�Ʈ
    public UnityEvent<string> OnHpChanged;  // ü�º��� �̺�Ʈ
    public UnityEvent OnHit;                // �ǰ� �̺�Ʈ
    public UnityEvent OnDestroy;            // �ı� �̺�Ʈ

    // �̱���
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

    /// �� �ǰ�
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

    /// ü�� ����
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