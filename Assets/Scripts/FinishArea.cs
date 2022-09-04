using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public static FinishArea Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Layers.Player)
        {
            ProcessFinale();
        }
    }

    private void ProcessFinale()
    {
        var winVFX = EffectManager.Instance.GetWinVFX().gameObject;
        winVFX.transform.position = transform.position;
        winVFX.SetActive(true);
        GameEvents.Victory();
    }
}
