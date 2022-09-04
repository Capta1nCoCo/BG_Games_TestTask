using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{    
    [SerializeField] private Material[] stateMaterials;

    private const int NORMAL_STATE = 0;
    private const int SHIELDED_STATE = 1;

    private bool isShielded;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = stateMaterials[NORMAL_STATE];

        GameEvents.ActivateShield += ActivateShield;
    }

    private void OnDestroy()
    {
        GameEvents.ActivateShield -= ActivateShield;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Layers.DeadlyArea)
        {
            if (!isShielded)
            {                
                ProcessDeath();
            }
        }
        
    }

    private void ProcessDeath()
    {
        var deathVFX = EffectManager.Instance.GetDeathVFX();
        deathVFX.transform.position = transform.position;
        deathVFX.SetActive(true);
        GameEvents.Death();
    }

    private void ActivateShield(bool isActive)
    {
        if (isActive)
        {
            meshRenderer.material = stateMaterials[SHIELDED_STATE];
            isShielded = true;
        }
        else
        {
            meshRenderer.material = stateMaterials[NORMAL_STATE];
            isShielded = false;
        }
    }
}
