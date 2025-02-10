using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject keyboard;
    private KeyboardKey[] keys;


    [Header(" Text Elements ")]
    [SerializeField] private TextMeshProUGUI keyboardPriceText;
    [SerializeField] private TextMeshProUGUI letterPriceText;

    [Header(" Settings ")]
    [SerializeField] private int keyboardHintPrice;
    [SerializeField] private int letterHintPrice;

    private bool shouldReset;

    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
    }

    // Start is called before the first frame update
    void Start()
    {
        keyboardPriceText.text = keyboardHintPrice.ToString();
        letterPriceText.text = letterHintPrice.ToString();

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

                if(shouldReset)
                {
                    letterHintGivenIndices.Clear();
                    shouldReset = false;
                }

                break;

            case GameState.LevelComplete:
                shouldReset = true;
                break;

            case GameState.Gameover:
                shouldReset = true;
                break;
        }
    }

    public void KeyboardHint()
    {
        if (DataManager.instance.GetCoins() < keyboardHintPrice)
            return;

        string secretWord = WordManager.instance.GetSecretWord();

        List<KeyboardKey> untouchedKeys = new List<KeyboardKey>();

        for (int i = 0; i < keys.Length; i++)
            if (keys[i].IsUntouched())
                untouchedKeys.Add(keys[i]);


        List<KeyboardKey> t_untouchedKeys = new List<KeyboardKey>(untouchedKeys);

        for (int i = 0; i < untouchedKeys.Count; i++)
            if (secretWord.Contains(untouchedKeys[i].GetLetter()))
                t_untouchedKeys.Remove(untouchedKeys[i]);

        if (t_untouchedKeys.Count <= 0)
            return;

        int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
        t_untouchedKeys[randomKeyIndex].SetInvalid();

        DataManager.instance.RemoveCoins(keyboardHintPrice);
    }

    List<int> letterHintGivenIndices = new List<int>();
    public void LetterHint()
    {
        if (DataManager.instance.GetCoins() < letterHintPrice)
            return;

        if (letterHintGivenIndices.Count >= 5)
        {
            Debug.Log("All hints have been given");
            return;
        }

        List<int> letterHintNotGivenIndices = new List<int>();

        for (int i = 0; i < 5; i++)
            if (!letterHintGivenIndices.Contains(i))
                letterHintNotGivenIndices.Add(i);
        

        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();

        string secretWord = WordManager.instance.GetSecretWord();

        int randomIndex = letterHintNotGivenIndices[Random.Range(0, letterHintNotGivenIndices.Count)];
        letterHintGivenIndices.Add(randomIndex);

        currentWordContainer.AddAsHint(randomIndex, secretWord[randomIndex]);

        DataManager.instance.RemoveCoins(letterHintPrice);
    }
}

