using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerInteract : MonoBehaviour
{
    public static Action OnGateHit;

    [Header("Elements")]

    [SerializeField]
    private CrowdSystem crowdSystem;

    private void Update()
    {
        if (GameManager.Instance.IsGameState())
        {
            InteractGates();
        }
    }
    private void InteractGates()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);
        
        foreach (Collider collider in detectedColliders)
        {
            if(collider.TryGetComponent(out Gates gates))
            {
                Debug.Log("We hit some gates");

                int bonusAmount = gates.GetBonusAmount(transform.position.x);
                BonusType bonusType = gates.GetBonusType(transform.position.x);

                gates.Disable();

                OnGateHit?.Invoke();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
            else if(collider.tag == "Finish")
            {
                Debug.Log("Finish Line");

                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);

                //SceneManager.LoadScene(0);
            }
        }
    }
}
