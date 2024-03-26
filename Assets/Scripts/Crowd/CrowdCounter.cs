using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]
    private TextMeshPro crowdCounterText;

    [SerializeField]
    private Transform runnersParent;

    private void Update()
    {
        SetCounterText();

        if(runnersParent.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetCounterText()
    {
        if (runnersParent != null)
        {
            crowdCounterText.SetText(runnersParent.childCount.ToString());
        }
    }


}
