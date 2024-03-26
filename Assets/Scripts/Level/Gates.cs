using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType
{
    Addition,
    Difference,
    Multiplication,
    Division
}
public class Gates : MonoBehaviour
{
    

    [Header("Elements")]

    [SerializeField] 
    private SpriteRenderer rightGateRenderer;

    [SerializeField] 
    private SpriteRenderer leftGateRenderer;

    [SerializeField]
    private TextMeshPro rightGateText;

    [SerializeField]
    private TextMeshPro leftGateText;


    [Header("Settings")]

    [SerializeField]
    private BonusType rightGateBonusType;

    [SerializeField]
    private int rightGateBonusAmount;

    [SerializeField]
    private BonusType leftGateBonusType;

    [SerializeField]
    private int leftGateBonusAmount;

    [SerializeField]
    private Color bonusColor;

    [SerializeField]
    private Color penaltyColor;


    private void Start()
    {
        SetupGates();
    }
    private void SetupGates()
    {
        switch(rightGateBonusType)
        {
            case BonusType.Addition:
                rightGateRenderer.color = bonusColor;
                rightGateText.SetText("+" + rightGateBonusAmount.ToString());
                break;
            case BonusType.Difference:
                rightGateRenderer.color = penaltyColor;
                rightGateText.SetText("-" + rightGateBonusAmount.ToString());
                break;
            case BonusType.Multiplication:
                rightGateRenderer.color = bonusColor;
                rightGateText.SetText("x" + rightGateBonusAmount.ToString());
                break;
            case BonusType.Division:
                rightGateRenderer.color = penaltyColor;
                rightGateText.SetText("/" + rightGateBonusAmount.ToString());
                break;
            default: 
                break;
        }

        switch (leftGateBonusType)
        {
            case BonusType.Addition:
                leftGateRenderer.color = bonusColor;
                leftGateText.SetText("+" + leftGateBonusAmount.ToString());
                break;
            case BonusType.Difference:
                leftGateRenderer.color = penaltyColor;
                leftGateText.SetText("-" + leftGateBonusAmount.ToString());
                break;
            case BonusType.Multiplication:
                leftGateRenderer.color = bonusColor;
                leftGateText.SetText("x" + leftGateBonusAmount.ToString());
                break;
            case BonusType.Division:
                leftGateRenderer.color = penaltyColor;
                leftGateText.SetText("/" + leftGateBonusAmount.ToString());
                break;
            default:
                break;
        }

    }

    public int GetBonusAmount(float playerPositionX)
    {
        int bonusAmount;
        if (playerPositionX > 0)
        {
            bonusAmount = rightGateBonusAmount;
        }
        else
        {
            bonusAmount = leftGateBonusAmount;
        }
        return bonusAmount;
    }

    public BonusType GetBonusType(float playerPositionX)
    {
        BonusType bonusType;
        if (playerPositionX > 0)
        {
            bonusType = rightGateBonusType;
        }
        else
        {
            bonusType = leftGateBonusType;
        }
        return bonusType;
    }

    public void Disable()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
    }

}
