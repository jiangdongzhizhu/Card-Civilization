using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        RegisterTotalScore();
        UpdateTotalScore();
    }

    private void RegisterTotalScore()
    {
        foreach (AreaInfo areaInfo in GlobalManager.Inst.HexGrid.GetAllElements())
        {
            areaInfo.AfterBuildingChanged += UpdateTotalScore;
        }
    }

    private void UpdateTotalScore()
    {
        totalScoreText.text = GlobalManager.Inst.GetTotalScore().ToString();
    }
}
