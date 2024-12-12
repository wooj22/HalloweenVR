using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    // ���ؼ� ���� ����
    public int index;
    LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        // ���� ��ǥ����
        if (target.useWorldSpace)
        {
            // target ��ȣ, ��ġ ����
            target.SetPosition(index, worldPosition);
        }
        else
        {
            // ���� ��ġ ��� -> target ��ȣ, ��ġ ����
            var localPosition =
                transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index, localPosition);
        }
    }
}