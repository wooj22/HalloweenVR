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

    private Magazine magazine;  // ������ ��ũ��Ʈ

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

    /// ����
    private void Shoot()
    {
        // ���� ray�� �浹�ϴ� ������ �ִٸ�
        if (Physics.Raycast(shootPoint.position, shootPoint.forward,
                out RaycastHit hitinfo, maxDistance, hittableMask))
        {
            // �浹 ����Ʈ
            Instantiate(hitEffectPrefab, hitinfo.point, Quaternion.identity);
            OnShootSuccess?.Invoke(hitinfo.point);

            // ���� �ѿ� ������ Hit() ����
            var hitObject = hitinfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();
        }
        else
        {
            // ���� ���κ� ��ġ ����
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitPoint);
        }
    }
}