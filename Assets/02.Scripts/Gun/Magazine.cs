using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20;
    public float chargingTime = 2f;     // ������ ���� �ð� 2��

    public UnityEvent OnReloadStart;    // ������ ���� �̺�Ʈ
    public UnityEvent OnReloadEnd;      // ������ ���� �̺�Ʈ
    public UnityEvent<int> OnBulletsChanged;    // �Ѿ� ���� ���� �̺�Ʈ
    public UnityEvent<float> OnChargeChanged;   // �� ���� �̺�Ʈ

    private int currentBullets;

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    /// ���� �Ѿ� ���� ����
    private int CurrentBullets
    {
        get => currentBullets;  //���� �Ѿ� ���� ��ȯ
        set                     //���� �Ѿ� ���� ����
        {
            if (value < 0)
                currentBullets = 0;
            else if (value > maxBullets)
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnBulletsChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }

    /// �ܿ� �Ѿ� ����
    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
            return false;
    }

    /// ������ ����
    //[ContextMenu("Reload")]
    public void StartReload()
    {
        if (currentBullets == maxBullets)
            return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    /// ������ ����
    public void StopReload()
    {
        StopAllCoroutines();
    }

    /// ������(����)
    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();            // ������ ���� �̺�Ʈ

        var beginTime = Time.time;          // ���� �ð�
        var beginBullets = currentBullets;  // �Ѿ� ���� ����
        var enoughPercent = 1f - ((float)currentBullets / maxBullets);  // ���� ����
        var enoughChargingTime = chargingTime * enoughPercent;          // ���� �ð�

        while (true)
        {
            // ����
            var t = (Time.time - beginTime) / enoughChargingTime;
            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t);

            if (t >= 1f)
                break;

            yield return null;
        }

        CurrentBullets = maxBullets;
        OnReloadEnd?.Invoke();              // ������ ���� �̺�Ʈ
    }
}