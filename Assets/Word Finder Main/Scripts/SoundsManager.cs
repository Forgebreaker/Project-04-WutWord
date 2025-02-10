using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    [Header(" Sounds ")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource letterAddedSound;
    [SerializeField] private AudioSource letterRemovedSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameoverSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.onLetterAdded += PlayLetterAddedSound;
        InputManager.onLetterRemoved += PlayLetterRemovedSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        InputManager.onLetterAdded -= PlayLetterAddedSound;
        InputManager.onLetterRemoved -= PlayLetterRemovedSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.LevelComplete:
                levelCompleteSound.Play();
                break;

            case GameState.Gameover:
                gameoverSound.Play();
                break;
        }
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    private void PlayLetterAddedSound()
    {
        letterAddedSound.Play();
    }

    private void PlayLetterRemovedSound()
    {
        letterRemovedSound.Play();
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        letterAddedSound.volume = 1;
        letterRemovedSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
    }

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        letterAddedSound.volume = 0;
        letterRemovedSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;
    }
}
