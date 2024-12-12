using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    // ��ݽ� ��Ʈ�ѷ� ����
    public float amplitude = 0.5f;  // ����
    public float duration = 0.05f;  // �ֱ�

    private XRBaseInteractable target;

    private void Awake()
    {
        target = GetComponent<XRBaseInteractable>();
    }

    public void Call()
    {
        if (target == null ||
            target.firstInteractorSelecting == null ||
            !(target.firstInteractorSelecting is XRBaseControllerInteractor))
            return;

        // ���ͷ��� get
        var interactor = target.firstInteractorSelecting
            as XRBaseControllerInteractor;
        if (interactor.xrController == null) return;

        // ��Ʈ�ѷ� ��ƽ
        interactor.xrController.SendHapticImpulse(amplitude, duration);
    }
}