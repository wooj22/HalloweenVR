using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public bool playOnStart = true;
    public float startFactor = 1f;
    public float additveFacotr = 0.1f;
    public float delayPerSpawnGroup = 3f;

    private void Start()
    {
        if (playOnStart) Play();
    }
    public void Play()
    {
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator Process()
    {
        var factor = startFactor;
        var wfs = new WaitForSeconds(delayPerSpawnGroup);

        while (true)
        {
            yield return wfs;
            yield return StartCoroutine(SpawnProcess(factor));

            factor += additveFacotr;
        }
    }

    IEnumerator SpawnProcess(float factor)
    {
        var count = Random.Range(factor, factor * 2);

        for(int i=0; i<count; i++)
        {
            Spawn();
            if (Random.value < 0.2f)
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
        }
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation, transform);
    }
}
