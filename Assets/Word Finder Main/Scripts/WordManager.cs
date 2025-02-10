using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [Header(" Elements ")]
    [SerializeField] private string secretWord;
    [SerializeField] private TextAsset wordsText;
    private string words;


    [Header(" Settings ")]
    private bool shouldReset;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        words = wordsText.text;
    }

    void Start()
    {
        SetNewSecretWord();

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                break;

            case GameState.Game:

                if (shouldReset)
                    SetNewSecretWord();

                break;

            case GameState.LevelComplete:
                shouldReset = true;
                break;

            case GameState.Gameover:
                shouldReset = true;
                break;
        }
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetNewSecretWord()
    {
        Debug.Log("String length : " + words.Length);
        int wordCount = (words.Length + 2) / 7;

        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 7;

        secretWord = words.Substring(wordStartIndex, 5).ToUpper();

        shouldReset = false;
    }
}
