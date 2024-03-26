using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]

    [SerializeField]
    private AudioSource gateHitSound;

    [SerializeField]
    private AudioSource runnerDieSound;

    [SerializeField]
    private AudioSource levelCompleteSound;

    [SerializeField]
    private AudioSource gameOverSound;

    private void Start()
    {
        PlayerInteract.OnGateHit += GateHitCallback;
        GameManager.OnGameStateChanged += GameStateChangedCallback;
        Enemy.OnRunnerDied += RunnerDiedCallback;
    }

    private void GateHitCallback()
    {
        PlaySound(gateHitSound);
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.GameOver)
        {
            PlaySound(gameOverSound);
        }
        else if(state == GameManager.GameState.LevelComplete)
        {
            PlaySound(levelCompleteSound);
        }
    }

    private void RunnerDiedCallback()
    {
        PlaySound(runnerDieSound);
    }

    private void PlaySound(AudioSource source)
    {
        source.Play();
    }

    public void DisableSound()
    {
        gateHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;

    }

    public void EnableSound()
    {
        gateHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
    }

    private void OnDestroy()
    {
        PlayerInteract.OnGateHit -= GateHitCallback;
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
        Enemy.OnRunnerDied -= RunnerDiedCallback;
    }
}
