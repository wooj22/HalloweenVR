using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    private Magazine magazine;  // 재장전 스크립트

    private void Awake()
    {
        magazine = GetComponent<Magazine>();
    }

    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var wfs = new WaitForSeconds(shootDelay);

        while (true)
        {
            if (magazine.Use())
                Shoot();
            else
                OnShootFail?.Invoke();
            yield return wfs;
        }
    }

    /// 공격
    private void Shoot()
    {
        // 총의 ray와 충돌하는 지점이 있다면
        if (Physics.Raycast(shootPoint.position, shootPoint.forward,
                out RaycastHit hitinfo, maxDistance, hittableMask))
        {
            // 충돌 이펙트
            Instantiate(hitEffectPrefab, hitinfo.point, Quaternion.identity);
            OnShootSuccess?.Invoke(hitinfo.point);

            // 몹이 총에 맞으면 Hit() 실행
            var hitObject = hitinfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();
        }
        else
        {
            // 레이 끝부분 위치 지정
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitPoint);
        }
    }
}
