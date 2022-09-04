using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<bool> ActivateShield;
    public static Action Death;
    public static Action Victory;
}
