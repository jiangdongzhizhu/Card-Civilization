using System.Collections.Generic;

namespace CardCivilization
{
    public enum SkillType
    {
        None,
        BuildingCard,
        CommandCard,
        TacticalCard,
        Weapon
    }

    public class Skill
    {
        public string ID { get; protected set; }
        public SkillType SkillType { get; protected set; }
        public int Cost { get; protected set; }
        public int TargetNumber { get; protected set; }
        public bool IsExhausted { get; protected set; }

        public virtual bool IsTargetValid(Area target, int targetIndex)
        {
            return true;
        }

        public virtual void Cast(List<Area> targets)
        {

        }
    }

    public class ReinforceCard : Skill
    {
        public ReinforceCard()
        {
            ID = "Reinforce";
            SkillType = SkillType.CommandCard;
            Cost = 0;
            TargetNumber = 1;
            IsExhausted = true;
        }

        public override void Cast(List<Area> targets)
        {
            targets[0].AddValuePoint(1, true);
        }
    }

    public class RequestSupportCard : Skill
    {
        public RequestSupportCard()
        {
            ID = "RequestSupport";
            SkillType = SkillType.CommandCard;
            Cost = 1;
            TargetNumber = 0;
        }

        public override void Cast(List<Area> targets)
        {
            CardManager.Inst.AddCardToHand<ReinforceCard>(3);
        }
    }

    public class RetrieveCard : Skill
    {
        public RetrieveCard()
        {
            ID = "Retrieve";
            SkillType = SkillType.CommandCard;
            Cost = 1;
            TargetNumber = 1;
        }

        public override void Cast(List<Area> targets)
        {
            int drawTimes = 1;
            targets[0].AddValuePoint(2, true);
            foreach (Area area in AreaManager.Inst.HexGrid.GetAllElements())
            {
                if (area.ValidValuePoint >= 8)
                {
                    drawTimes = 3;
                    break;
                }
            }
            CardManager.Inst.DrawCards(drawTimes);
        }
    }
}
