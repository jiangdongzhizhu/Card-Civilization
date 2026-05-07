using CardCivilization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsUIManager : MonoBehaviour
{
    [SerializeField] private Text totalScoreText;

    private void FixedUpdate()
    {
        if (AreaManager.Inst.HexGrid == null) return;
        int totalScore = 0;
        foreach (Area area in AreaManager.Inst.HexGrid.GetAllElements())
        {
            totalScore += area.ValidValuePoint;
        }
        totalScoreText.text = totalScore.ToString();
    }
}
