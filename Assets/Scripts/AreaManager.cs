using UnityEngine;

namespace CardCivilization
{
    public class History
    {
        public Area lastBuffedArea;
    }

    public class AreaManager : MonoBehaviour
    {
        [SerializeField] private AreaGenerator areaGenerator;
        private HexGrid<Area> hexGrid;

        public static AreaManager Inst { get; private set; }
        public HexGrid<Area> HexGrid => hexGrid;
        public History History { get; private set; } = new History();

        private void Awake()
        {
            Inst = this;
            hexGrid = new HexGrid<Area>(4);
        }

        private void Start()
        {
            InitializeAreas();
            areaGenerator.GenerateAreas(hexGrid);
            hexGrid[Vector2Int.zero].value.BuildBuilding(new Center());
        }

        private void InitializeAreas()
        {
            foreach (var item in hexGrid.GetAllElements())
            {
                item.value.Initialize(item);
            }
        }
    }
}