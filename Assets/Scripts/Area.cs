using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    [SerializeField] private Image hexImage;
    [SerializeField] private Text buildingIDText;
    [SerializeField] private Text scoreText;
    private HexGridElement<AreaInfo> hexGridElement;

    public AreaInfo AreaInfo => hexGridElement.value;

    public void Initialize(HexGridElement<AreaInfo> hexGridElement)
    {
        this.hexGridElement = hexGridElement;
        UpdateTerrainType();
        buildingIDText.text = "";
        scoreText.text = "";
        AreaInfo areaInfo = hexGridElement.value;
        areaInfo.AfterTerrainTypeChanged += UpdateTerrainType;
        areaInfo.AfterBuildingChanged += UpdateBuildingInfo;
    }

    public void UpdateTerrainType()
    {
        Color color = Color.white;
        AreaInfo areaInfo = hexGridElement.value;
        switch (areaInfo.TerrainType)
        {
            case TerrainType.Plain:
                color = new Color(0.8f, 0.63f, 0.28f);
                break;
            case TerrainType.Forest:
                color = Color.green;
                break;
            case TerrainType.Mountain:
                color = new Color(0.85f, 0.22f, 0.18f);
                break;
            case TerrainType.Desert:
                color = Color.yellow;
                break;
            case TerrainType.Waters:
                color = new Color(0.16f, 0.3f, 0.8f);
                break;
        }
        hexImage.color = color;
    }

    public void UpdateBuildingInfo()
    {
        AreaInfo areaInfo = hexGridElement.value;
        string buildingID = areaInfo.BuildingID != null ? areaInfo.BuildingID : "";
        buildingIDText.text = buildingID;
        scoreText.text = "Score " + areaInfo.Score.ToString();
    }

    public void PlayCard()
    {
        GlobalManager.Inst.TargetPosition = hexGridElement.coordinates;
    }
}