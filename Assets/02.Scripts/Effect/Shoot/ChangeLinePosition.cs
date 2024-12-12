using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    // 조준선 길이 변경
    public int index;
    LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        // 월드 좌표계라면
        if (target.useWorldSpace)
        {
            // target 번호, 위치 지정
            target.SetPosition(index, worldPosition);
        }
        else
        {
            // 로컬 위치 계산 -> target 번호, 위치 지정
            var localPosition =
                transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index, localPosition);
        }
    }
}
