using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager instance;

    [Header(" Settings ")]
    private bool haptics;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnableHaptics()
    {
        haptics = true;
    }

    public void DisableHaptics()
    {
        haptics = false;
    }

    public bool HapticsEnabled()
    {
        return haptics;
    }

    public static void Vibrate()
    {
        if (instance.HapticsEnabled())
        {
        }
    }
}
