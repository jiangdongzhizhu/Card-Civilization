using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardCivilization
{
    public class SkillInteractorBase : MonoBehaviour, IPointerDownHandler
    {
        public virtual Skill Skill => null;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!CanSelectSkill()) return;
            if (Skill.TargetNumber <= 0)
            {
                CastSkill(null);
                return;
            }
            SkillInteractionManager.Inst.SelectSkill(this);
        }

        public void CastSkill(List<Area> targets)
        {
            Skill.Cast(targets);
            OnSkillCast();
        }

        protected virtual bool CanSelectSkill()
        {
            return true;
        }

        protected virtual void OnSkillCast()
        {
        
        }
    }
}