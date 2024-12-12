using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeEmissionColor : MonoBehaviour
{
    public float intensity = 5;
    Renderer target;

    private void Awake()
    {
        target = GetComponent<Renderer>();
    }

    public void Call(Color color)
    {
        target.material.SetColor("_EmissionColor", color * intensity);
    }
}
