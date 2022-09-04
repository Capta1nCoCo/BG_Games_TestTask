using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private GameObject winVFXPrefab;

    [SerializeField] private int poolSize = 1;

    private Queue<GameObject> deathVFXPool = new Queue<GameObject>();
    private Queue<GameObject> winVFXPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        SpawnDeathVFX();
        SpawnWinVFX();
    }

    private void SpawnDeathVFX()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var deathVFX = Instantiate(deathVFXPrefab);
            deathVFXPool.Enqueue(deathVFX);
            deathVFX.SetActive(false);
        }
    }

    private void SpawnWinVFX()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var winVFX = Instantiate(winVFXPrefab);
            winVFXPool.Enqueue(winVFX);
            winVFX.SetActive(false);
        }
    }

    public GameObject GetDeathVFX()
    {
        GameObject deathVFX = deathVFXPool.Dequeue();
        deathVFX.SetActive(true);
        deathVFXPool.Enqueue(deathVFX);
        return deathVFX;
    }
    
    public GameObject GetWinVFX()
    {
        GameObject winVFX = winVFXPool.Dequeue();
        winVFX.SetActive(true);
        winVFXPool.Enqueue(winVFX);
        return winVFX;
    }
}
