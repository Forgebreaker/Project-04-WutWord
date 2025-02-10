using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header(" Elements ")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameoverCG;
    [SerializeField] private CanvasGroup settingsCG;

    [Header(" Menu Elements ")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header(" Level Complete Elements ")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header(" Gameover Elements ")]
    [SerializeField] private TextMeshProUGUI gameoverCoins;
    [SerializeField] private TextMeshProUGUI gameoverSecretWord;
    [SerializeField] private TextMeshProUGUI gameoverBestScore;

    [Header(" Game Elements ")]
    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI gameCoins;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //ShowGame();
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameover();

        GameManager.onGameStateChanged += GameStateChangedCallback;
        DataManager.onCoinsUpdated += UpdateCoinsTexts;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoinsTexts;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Menu:

                ShowMenu();
                HideGame();

                break;

            case GameState.Game:

                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameover();

                break;

            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;

            case GameState.Gameover:
                ShowGameover();
                HideGame();
                break;
        }
    }

    public void UpdateCoinsTexts()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameoverCoins.text = menuCoins.text;
    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }

    private void HideMenu()
    {
        HideCG(menuCG);
    }

    private void ShowGame()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();

        ShowCG(gameCG);
    }

    private void HideGame()
    {
        HideCG(gameCG);
    }

    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(levelCompleteCG);
    }

    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }

    private void ShowGameover()
    {
        gameoverCoins.text = DataManager.instance.GetCoins().ToString();
        gameoverSecretWord.text = WordManager.instance.GetSecretWord();
        gameoverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameoverCG);
    }

    private void HideGameover()
    {
        HideCG(gameoverCG);
    }

    public void ShowSettings()
    {
        ShowCG(settingsCG);
    }

    public void HideSettings()
    {
        HideCG(settingsCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
