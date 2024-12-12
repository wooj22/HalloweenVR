using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20;
    public float chargingTime = 2f;     // 재장전 충전 시간 2초

    public UnityEvent OnReloadStart;    // 재장전 시작 이벤트
    public UnityEvent OnReloadEnd;      // 재장전 종료 이벤트
    public UnityEvent<int> OnBulletsChanged;    // 총알 개수 변경 이벤트
    public UnityEvent<float> OnChargeChanged;   // 총 충전 이벤트

    private int currentBullets;

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    /// 현재 총알 개수 제어
    private int CurrentBullets
    {
        get => currentBullets;  //현재 총알 개수 반환
        set                     //현재 총알 개수 지정
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

    /// 잔여 총알 유무
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

    /// 재장전 보장
    //[ContextMenu("Reload")]
    public void StartReload()
    {
        if (currentBullets == maxBullets)
            return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    /// 재장전 종료
    public void StopReload()
    {
        StopAllCoroutines();
    }

    /// 재장전(충전)
    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();            // 재장전 시작 이벤트

        var beginTime = Time.time;          // 현재 시간
        var beginBullets = currentBullets;  // 총알 개수 지정
        var enoughPercent = 1f - ((float)currentBullets / maxBullets);  // 충전 비율
        var enoughChargingTime = chargingTime * enoughPercent;          // 충전 시간

        while (true)
        {
            // 충전
            var t = (Time.time - beginTime) / enoughChargingTime;
            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t);

            if (t >= 1f)
                break;

            yield return null;
        }

        CurrentBullets = maxBullets;
        OnReloadEnd?.Invoke();              // 재장전 종료 이벤트
    }
}
