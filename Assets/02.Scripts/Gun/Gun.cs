using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGrab;       // √— ¿‚¿ª∂ß
    public UnityEvent OnRelease;    // √— ≥ı¿ª∂ß

    public void Grab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
            OnGrab?.Invoke();
    }

    public void Release(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
            OnRelease?.Invoke();
    }
}
