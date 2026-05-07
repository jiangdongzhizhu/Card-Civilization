using UnityEngine;
using UnityEngine.UI;

namespace CardCivilization
{
    public class CardInteractor : SkillInteractorBase
    {
        [SerializeField] private Text costText;
        [SerializeField] private Text nameText;
        [SerializeField] private Text skillTypeText;
        private Skill skill;

        public override Skill Skill => skill;

        public void Initialize(Skill skill)
        {
            this.skill = skill;
            costText.text = skill.Cost.ToString();
            nameText.text = skill.ID;
            skillTypeText.text = skill.SkillType.ToString();
        }

        protected override bool CanSelectSkill()
        {
            return CardManager.Inst.CanSelectCard(skill);
        }

        protected override void OnSkillCast()
        {
            CardManager.Inst.PlayCard(skill);
        }
    }
}