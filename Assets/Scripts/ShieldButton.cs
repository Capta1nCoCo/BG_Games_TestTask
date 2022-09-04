using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameEvents.ActivateShield(true);
        StartCoroutine(DisableShieldWithDelay());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameEvents.ActivateShield(false);
        StopAllCoroutines();
    }

    private IEnumerator DisableShieldWithDelay()
    {
        yield return new WaitForSeconds(2f);
        GameEvents.ActivateShield(false);
    }
}
