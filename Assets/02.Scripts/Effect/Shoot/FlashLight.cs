using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    // »ç°Ý½Ã ÃÑ ³»ºÎ ¹ÝÂ¦ÀÓ
    public float duration = 0.05f;
    Light target;

    private void Awake()
    {
        target = GetComponent<Light>();
    }

    public void Call()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        target.enabled = true;
        yield return new WaitForSeconds(duration);
        target.enabled = false;
    }
}
