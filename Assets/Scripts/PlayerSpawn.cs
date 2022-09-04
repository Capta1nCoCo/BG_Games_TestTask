using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Transform playerModel;

    private float delayInSeconds = 1f;

    private void Awake()
    {
        SpawnPlayer();        
        GameEvents.Death += SpawnPlayer;
    }

    private void OnDestroy()
    {
        GameEvents.Death -= SpawnPlayer;
    }

    private void SpawnPlayer()
    {
        playerModel.transform.position = transform.position;
        playerModel.rotation = transform.rotation;
        StartCoroutine(ActivatePlayerWithDelay());
    }

    private IEnumerator ActivatePlayerWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        playerModel.gameObject.SetActive(true);
    }
}
