using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobCounterUI : MonoBehaviour
{
    int killCount;
    int spawnCount;
    TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void UpDateUI()
    {
        if (!enabled)
            return;
        textUI.text =
            $"Kill/Alive/Spawn\n {killCount}/" +
            $"{spawnCount - killCount}/{spawnCount}";
    }

    private void OnEnable()
    {
        killCount = spawnCount = 0;
        UpDateUI();
    }

    public void OnSpawn()
    {
        spawnCount++;
        UpDateUI();
    }

    public void OnKill()
    {
        killCount++;
        UpDateUI();
    }
}
