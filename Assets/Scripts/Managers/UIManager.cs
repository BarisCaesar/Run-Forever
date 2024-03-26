using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject levelCompletePanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private TextMeshProUGUI levelText;


    private void Start()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(false);

        progressBar.value = 0;
        
        levelText.SetText("Level " + (RoadChunkManager.Instance.GetCurrentLevel() + 1));

        GameManager.OnGameStateChanged += GameStateShangedCallback;
    }
    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private void GameStateShangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.GameOver)
        {
            ShowGameOver();
        }
        else if (state == GameManager.GameState.LevelComplete)
        {
            levelCompletePanel.SetActive(true);
        }
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void UpdateProgressBar()
    {
        if(GameManager.Instance.IsGameState())
        {
            float progress = (PlayerController.Instance.transform.position.z / RoadChunkManager.Instance.GetFinishPositionZ());
            progressBar.value = progress;
        }
        
    }

 

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateShangedCallback;
    }
}
