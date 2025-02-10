using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image hapticsImage;

    [Header(" Settings ")]
    private bool soundsState;
    private bool hapticsState;

    void Start()
    {
        LoadStates();
    }

    public void SoundsButtonCallback()
    {
        soundsState = !soundsState;
        UpdateSoundsState();
        SaveStates();
    }

    private void UpdateSoundsState()
    {
        if (soundsState)
            EnableSounds();
        else
            DisableSounds();
    }

    private void EnableSounds()
    {
        SoundsManager.instance.EnableSounds();
        soundsImage.color = Color.white;
    }

    private void DisableSounds()
    {
        SoundsManager.instance.DisableSounds();
        soundsImage.color = Color.gray;
    }


    public void HapticsButtonCallback()
    {
        hapticsState = !hapticsState;
        UpdateHapticsState();
        SaveStates();
    }

    private void UpdateHapticsState()
    {
        if (hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }

    private void EnableHaptics()
    {
        HapticsManager.instance.EnableHaptics();
        hapticsImage.color = Color.white;
    }

    private void DisableHaptics()
    {
        HapticsManager.instance.DisableHaptics();
        hapticsImage.color = Color.gray;
    }

    private void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;

        UpdateSoundsState();
        UpdateHapticsState();
    }

    private void SaveStates()
    {
        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
    }
}
