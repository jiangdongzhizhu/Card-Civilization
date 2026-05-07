using System.Collections.Generic;

namespace CardCivilization
{
    public class BuildingCard<T> : Skill where T : Building, new()
    {
        public BuildingCard()
        {
            SkillType = SkillType.BuildingCard;
            Cost = 1;
            TargetNumber = 1;
        }

        public override bool IsTargetValid(Area target, int targetIndex)
        {
            if (target.HasBuilding) return false;

            bool hasNeighbor = false;
            var neighbors = AreaManager.Inst.HexGrid.Area(target.HexGridElement, 1, false);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.value.HasBuilding)
                {
                    hasNeighbor = true;
                    break;
                }
            }
            return hasNeighbor;
        }

        public sealed override void Cast(List<Area> targets)
        {
            targets[0].BuildBuilding(new T());
            OnCastEffect(targets);
        }

        protected virtual void OnCastEffect(List<Area> targets) { }
    }

    public class CommonBuildingCard : BuildingCard<CommonBuilding>
    {
        public CommonBuildingCard()
        {
            ID = "CommonBuilding";
        }
    }

    public class GrowerCard : BuildingCard<Grower>
    {
        public GrowerCard()
        {
            ID = "Grower";
            Cost = 2;
        }
    }

    public class BigNumLoverCard : BuildingCard<BigNumLover>
    {
        public BigNumLoverCard()
        {
            ID = "BigNumLover";
        }
    }

    public class GiftGiverCard : BuildingCard<GiftGiver>
    {
        public GiftGiverCard()
        {
            ID = "GiftGiver";
            TargetNumber = 2;
        }

        public override bool IsTargetValid(Area target, int targetIndex)
        {
            if (targetIndex == 1)
            {
                return true;
            }
            return base.IsTargetValid(target, targetIndex);
        }

        protected override void OnCastEffect(List<Area> targets)
        {
            if (targets[0].TotalValuePoint >= 4)
            {
                targets[1].AddValuePoint(4);
            }
        }
    }

    public class EquivalentLoverCard : BuildingCard<EquivalentLover>
    {
        public EquivalentLoverCard()
        {
            ID = "EquivalentLover";
        }
    }
}
