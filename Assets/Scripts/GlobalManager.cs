using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private AreaGenerator areaGenerator;
    private HexGrid<AreaInfo> hexGrid;

    public static GlobalManager Inst { get; private set; }
    public HexGrid<AreaInfo> HexGrid => hexGrid;
    public Vector2Int TargetPosition { get; set; }
    public static AreaInfo TargetAreaInfo => Inst.HexGrid[Inst.TargetPosition].value;

    private void Awake()
    {
        Inst = this;
        hexGrid = new HexGrid<AreaInfo>(4);
        InitializeTerrains();
        areaGenerator.GenerateAreas(hexGrid);
    }

    private void InitializeTerrains()
    {
        foreach(var item in hexGrid.GetAllElements())
        {
            var areaInfo = item.value;
            int roll = Random.Range(1, 6);
            areaInfo.TerrainType = (TerrainType)roll;
        }
    }

    public float GetTotalScore()
    {
        int totalScore = 0;
        foreach (AreaInfo areaInfo in HexGrid.GetAllElements())
        {
            totalScore += areaInfo.Score;
        }
        return totalScore;
    }
}
