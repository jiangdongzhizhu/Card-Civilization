using UnityEngine;

public class AreaGenerator : MonoBehaviour
{
    [SerializeField] private Transform areaRoot;
    [SerializeField] private GameObject areaGO;
    [SerializeField] private Vector2 hexSize;

    public void GenerateAreas(HexGrid<AreaInfo> hexGrid)
    {
        foreach (var item in hexGrid.GetAllElements())
        {
            var position = hexGrid.RealPosition(item, hexSize.x, hexSize.y);
            var go = Instantiate(areaGO, areaRoot);
            go.transform.localPosition = position;
            var area = go.GetComponent<Area>();
            area.Initialize(item);
        }
    }
}
