using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bomb : MonoBehaviour
{
    public enum State
    {
        Idle,
        Drop
    }

    public float explosionRadius;
    public LayerMask explosionHittableMask;
    public float recyleDelay = 1f;
    public UnityEvent OnExplosion;
    public UnityEvent OnRecycle;
    State state;

    public void Drop()
    {
        state = State.Drop;
    }

    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection(
            (IXRSelectInteractable)interactable);
        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 150, 300));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Idle)
            return;
        Explosion();
    }

    void Explosion()
    {
        var overlaps = Physics.OverlapSphere(
            transform.position, explosionRadius,
            explosionHittableMask, QueryTriggerInteraction.Collide);

        foreach(var overlap in overlaps)
        {
            var hitObject = overlap.GetComponent<Hittable>();
            hitObject?.Hit();
        }

        OnExplosion?.Invoke();
        Invoke(nameof(Recycle), recyleDelay);
    }

    void Recycle()
    {
        state = State.Idle;
        OnRecycle?.Invoke();
    }
}
