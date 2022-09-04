using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private float delayInSeconds = 2f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameEvents.Death += OnDeath;
    }

    private void OnEnable()
    {
        StartCoroutine(SetDestinationWithDelay());
    }   

    private void OnDestroy()
    {
        GameEvents.Death -= OnDeath;
    }

    private void OnDeath()
    {
        agent.isStopped = true;
        gameObject.SetActive(false);        
    }

    private IEnumerator SetDestinationWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        agent.destination = FinishArea.Instance.transform.position;
        agent.isStopped = false;        
    }
}
