using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]
    private PlayerAnimator playerAnimator;

    [SerializeField]
    private Transform runnersParent;

    [SerializeField]
    private GameObject runnerPrefab;


    [Header("Settings")]
    [SerializeField]
    private float radius;

    [SerializeField]
    private float angle;

    private void Update()
    {
        if(!GameManager.Instance.IsGameState())
        {
            return;
        }

        PlaceRunners();   

        if(runnersParent.childCount <= 0) 
        {
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {         
            runnersParent.GetChild(i).localPosition = GetRunnerLocalPosition(i);
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0f, z);
    }

    private void AddRunners(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }

        playerAnimator.Run();
    }

    private void RemoveRunners(int amount)
    {
        if(amount > runnersParent.childCount)
        {
            amount = runnersParent.childCount;
        }

        int runnersAmount = runnersParent.childCount;

        for(int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);

            Destroy(runnerToDestroy.gameObject);
        }

    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount); 
                break;
            case BonusType.Multiplication:
                int runnerAmountToAdd = runnersParent.childCount * (bonusAmount - 1);
                AddRunners(runnerAmountToAdd);
                break;
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Division:
                int runnerAmountToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnerAmountToRemove);
                break;
        }
    }
}
