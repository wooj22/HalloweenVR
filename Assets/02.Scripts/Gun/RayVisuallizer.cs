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
            // 총의 ray와 충돌하는 지점이 있다면
            if(Physics.Raycast(transform.position, transform.forward, 
                out RaycastHit hitinfo, distance, hitRayMask))
            {
                // 충돌 지점의 로컬 좌표를 선의 끝부분 위치로 지정
                ray.SetPosition(1, transform.InverseTransformPoint(hitinfo.point));
                ray.enabled = true;

                // ray 끝 부분에 reticle
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
