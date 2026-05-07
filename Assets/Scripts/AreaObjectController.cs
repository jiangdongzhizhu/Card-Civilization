using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardCivilization
{
    public class AreaObjectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image hexImage;
        [SerializeField] private Text mainText;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject validIndicatorGO;
        [SerializeField] private GameObject invalidIndicatorGO;
        private HexGridElement<Area> hexGridElement;
        private bool isValidTarget = false;
        private bool isSelectedTarget = false;

        public Area Area => hexGridElement;

        private void Update()
        {
            if (hexGridElement.value == null) return;
            UpdateTexts();
        }

        public void Initialize(HexGridElement<Area> hexGridElement)
        {
            this.hexGridElement = hexGridElement;
            mainText.text = "";
            scoreText.text = "";
        }

        public void UpdateTexts()
        {
            Area area = hexGridElement;
            mainText.text = area.BuildingID;
            string scoreTextContent = area.TotalValuePoint.ToString();
            if (area.TempValuePoint > 0)
            {
                scoreTextContent += $"(+{area.TempValuePoint})";
            }
            scoreText.text = scoreTextContent;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isSelectedTarget) return;
            isValidTarget = SkillInteractionManager.Inst.IsTargetValid(hexGridElement);
            var indicatorGO = isValidTarget ? validIndicatorGO : invalidIndicatorGO;
            indicatorGO.SetActive(true);
        }

        private void HideHoverIndicator()
        {
            if (isSelectedTarget) return;
            if (isValidTarget)
            {
                validIndicatorGO.SetActive(false);
                isValidTarget = false;
            }
            else
            {
                invalidIndicatorGO.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideHoverIndicator();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                HideHoverIndicator();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!isValidTarget) return;
                isSelectedTarget = true;
                isValidTarget = false;
                SkillInteractionManager.Inst.SelectTarget(this);
            }
        }

        public void CancelSelection()
        {
            isSelectedTarget = false;
            isValidTarget = false;
            validIndicatorGO.SetActive(false);
            invalidIndicatorGO.SetActive(false);
        }
    }
}