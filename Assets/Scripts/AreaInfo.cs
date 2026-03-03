using System;

public enum TerrainType
{
    None,
    Plain,
    Forest,
    Mountain,
    Desert,
    Waters
}

public class AreaInfo
{
    private TerrainType terrainType;
    private int additionalScore;

    public TerrainType TerrainType
    {
        get => terrainType;
        set
        {
            terrainType = value;
            AfterTerrainTypeChanged?.Invoke();
        }
    }
    public string BuildingID { get; private set; }
    public int Score { get; private set; }

    public event Action AfterTerrainTypeChanged;
    public event Action AfterBuildingChanged;

    public void AddScore(int score)
    {
        additionalScore += score;
    }

    public void BuildBuilding(string buildingID)
    {
        BuildingID = buildingID;
        Building.dict[buildingID].OnBuilt?.Invoke();

        Score += additionalScore;
        additionalScore = 0;
        AfterBuildingChanged?.Invoke();
    }
}
