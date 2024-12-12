using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    // 사격시 컨트롤러 진동
    public float amplitude = 0.5f;  // 세기
    public float duration = 0.05f;  // 주기

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

        // 인터랙터 get
        var interactor = target.firstInteractorSelecting
            as XRBaseControllerInteractor;
        if (interactor.xrController == null) return;

        // 컨트롤러 햅틱
        interactor.xrController.SendHapticImpulse(amplitude, duration);
    }
}
