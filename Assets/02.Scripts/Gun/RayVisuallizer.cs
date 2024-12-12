using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisuallizer : MonoBehaviour
{
    [Header("[Ray]")]
    public LineRenderer ray;
    public LayerMask hitRayMask;
    public float distance = 100f;

    [Header("[Reticle Point]")]
    public GameObject reticlePoint;
    public bool showReticle = true;

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Off()
    {
        StopAllCoroutines();

        ray.enabled = false;
        reticlePoint.SetActive(false);
    }

    private IEnumerator Process()
    {
        while (true)
        {
            // ���� ray�� �浹�ϴ� ������ �ִٸ�
            if(Physics.Raycast(transform.position, transform.forward, 
                out RaycastHit hitinfo, distance, hitRayMask))
            {
                // �浹 ������ ���� ��ǥ�� ���� ���κ� ��ġ�� ����
                ray.SetPosition(1, transform.InverseTransformPoint(hitinfo.point));
                ray.enabled = true;

                // ray �� �κп� reticle
                reticlePoint.transform.position = hitinfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                ray.enabled = false;
                reticlePoint.SetActive(false);
            }

            yield return null;
        }
    }
}