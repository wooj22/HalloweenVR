using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponStand : MonoBehaviour
{
    /// 총이 거치대에 결합되었을 경우
    public void OnSocketed(SelectEnterEventArgs args)
    {
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StartReload();
    }

    /// 총이 거치대에서 분리되었을 경우
    public void OnUnsocketed(SelectExitEventArgs args)
    {
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StopReload();
    }
}
