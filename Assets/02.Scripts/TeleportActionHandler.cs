using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public InputActionReference inputActionRef;
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    bool isShowing = false;

    public void OnEnable()
    {
        inputActionRef.action.canceled += OnCanceled;
    }

    private void OnDisable()
    {
        inputActionRef.action.canceled -= OnCanceled;
    }

    public void OnCanceled(InputAction.CallbackContext obj)
    {
        isShowing = !isShowing;

        if (isShowing)
            StartCoroutine(DelayCall(OnShow));
        else
            StartCoroutine(DelayCall(OnHide));
    }

    IEnumerator DelayCall(UnityEvent e)
    {
        yield return null;
        e?.Invoke();
    }
}
