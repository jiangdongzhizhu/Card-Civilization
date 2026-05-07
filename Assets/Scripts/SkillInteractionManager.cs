using System.Collections.Generic;
using UnityEngine;

namespace CardCivilization
{
    public class SkillInteractionManager : MonoBehaviour
    {
        private SkillInteractorBase currentSkillInteractor;
        private readonly List<AreaObjectController> targetObjects = new List<AreaObjectController>();
        public static SkillInteractionManager Inst {  get; private set; }

        private void Awake()
        {
            Inst = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelSelection();
            }
        }

        public void SelectSkill(SkillInteractorBase skillInteractor)
        {
            if (currentSkillInteractor != null)
            {
                CancelSelection();
            }
            currentSkillInteractor = skillInteractor;
            LineIndicatorManager.Inst.EnableLine(skillInteractor.transform);
        }

        public bool IsTargetValid(Area area)
        {
            if (currentSkillInteractor == null) return false;
            return currentSkillInteractor.Skill.IsTargetValid(area, targetObjects.Count);
        }

        public void SelectTarget(AreaObjectController areaObjectController)
        {
            targetObjects.Add(areaObjectController);
            LineIndicatorManager.Inst.AddPoint(areaObjectController.transform);
            if (targetObjects.Count < currentSkillInteractor.Skill.TargetNumber) return;

            List<Area> targets = new List<Area>();
            foreach (var targetObject in targetObjects)
            {
                targets.Add(targetObject.Area);
            }
            currentSkillInteractor.CastSkill(targets);

            CancelSelection();
        }

        private void CancelSelection()
        {
            currentSkillInteractor = null;
            foreach (var targetObject in targetObjects)
            {
                targetObject.CancelSelection();
            }
            targetObjects.Clear();
            LineIndicatorManager.Inst.Clear();
        }
    }
}