using System.Collections.Generic;

namespace CardCivilization
{
    public class Building
    {
        public string ID { get; protected set; }

        public virtual void OnBuilt(HexGridElement<Area> element) { }

        public virtual void OnRemove() { }
    }

    public class CommonBuilding : Building
    {
        public CommonBuilding()
        {
            ID = "Common";
        }
    }

    public class Center : Building
    {
        protected bool flag = true;

        public Center()
        {
            ID = "Center";
        }

        public override void OnBuilt(HexGridElement<Area> element)
        {
            CardManager.Inst.OnTurnEnd += ResetFlag;
            foreach (Area item in AreaManager.Inst.HexGrid.GetAllElements())
            {
                item.OnValuePointIncreaseEvent += Buff;
            }
        }

        public override void OnRemove()
        {
            CardManager.Inst.OnTurnEnd -= ResetFlag;
            foreach (Area item in AreaManager.Inst.HexGrid.GetAllElements())
            {
                item.OnValuePointIncreaseEvent -= Buff;
            }
        }

        protected void ResetFlag()
        {
            flag = true;
        }

        protected void Buff()
        {
            if (!flag) return;
            flag = false;   //Necessary Priority, otherwise stack overflow.
            AreaManager.Inst.History.lastBuffedArea.AddValuePoint(1);
        }
    }

    public class Grower : Building
    {
        protected Area area;
        protected readonly List<Area> surroundingAreas = new List<Area>();

        public Grower()
        {
            ID = "Grower";
        }

        public override void OnBuilt(HexGridElement<Area> element)
        {
            area = element;
            IEnumerable<HexGridElement<Area>> surroundingAreas = AreaManager.Inst.HexGrid.Area(element, 1, false);
            foreach (Area item in surroundingAreas)
            {
                this.surroundingAreas.Add(item);
                item.OnValuePointIncreaseEvent += BuffSelf;
            }
        }

        public override void OnRemove()
        {
            foreach (var item in surroundingAreas)
            {
                item.OnValuePointIncreaseEvent -= BuffSelf;
            }
        }

        private void BuffSelf()
        {
            area.AddValuePointDirectly(1);
        }
    }

    public class BigNumLover : Building
    {
        public BigNumLover()
        {
            ID = "BigNumLover";
        }

        public override void OnBuilt(HexGridElement<Area> element)
        {
            IEnumerable<HexGridElement<Area>> surroundingAreas = AreaManager.Inst.HexGrid.Area(element, 1, false);
            int buff = 0;
            foreach (Area item in surroundingAreas)
            {
                if (item.TotalValuePoint >= 4)
                {
                    buff++;
                }
            }
            element.value.AddValuePoint(buff);
        }
    }

    public class GiftGiver : Building
    {
        public GiftGiver()
        {
            ID = "GiftGiver";
        }
    }

    public class EquivalentLover : Building
    {
        public EquivalentLover()
        {
            ID = "EquivalentLover";
        }

        public override void OnBuilt(HexGridElement<Area> element)
        {
            IEnumerable<HexGridElement<Area>> surroundingAreas = AreaManager.Inst.HexGrid.Area(element, 1, false);
            int buff = 0;
            foreach (Area item in surroundingAreas)
            {
                if (item.TotalValuePoint == element.value.TotalValuePoint)
                {
                    buff += 2;
                }
            }
            element.value.AddValuePoint(buff);
        }
    }
}