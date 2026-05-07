using System;

namespace CardCivilization
{
    public class Area
    {
        private Building building;

        public HexGridElement<Area> HexGridElement { get; private set; }
        public int ValuePoint { get; set; }
        public int TempValuePoint { get; private set; }
        public int TotalValuePoint => ValuePoint + TempValuePoint;
        public int ValidValuePoint => HasBuilding ? TotalValuePoint : 0;
        public bool HasBuilding => building != null;
        public string BuildingID => building != null ? building.ID : "";

        public event Action OnBuildingBuiltEvent;
        public event Action OnValuePointIncreaseEvent;

        public void Initialize(HexGridElement<Area> element)
        {
            HexGridElement = element;
            CardManager.Inst.OnTurnEnd += ClearTempValuePoint;

            int roll = UnityEngine.Random.Range(0, 5);
            ValuePoint = roll;
        }

        private void ClearTempValuePoint()
        {
            TempValuePoint = 0;
        }

        public void AddValuePointDirectly(int point, bool isTemp = false)
        {
            if (isTemp)
            {
                TempValuePoint += point;
            }
            else
            {
                ValuePoint += point;
            }
        }

        public void AddValuePoint(int point, bool isTemp = false)
        {
            AddValuePointDirectly(point, isTemp);
            AreaManager.Inst.History.lastBuffedArea = this;
            OnValuePointIncreaseEvent?.Invoke();
        }

        public void BuildBuilding(Building building)
        {
            this.building = building;
            building.OnBuilt(HexGridElement);

            OnBuildingBuiltEvent?.Invoke();
        }

        public void RemoveBuilding()
        {
            building.OnRemove();
            building = null;

            ValuePoint = 0;
        }
    }
}