using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGM.UI
{
    public class SlotPopupUI : MonoBehaviour
    {

        public Button dropBtn, cancelBtn;
        public RectTransform RectTrm => transform as RectTransform;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowPopupUI(Vector2 position)
        {
            SetActiveUI(true);
            RectTrm.anchoredPosition = position;
        }

        public void SetActiveUI(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1f : 0;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
        }
    }
}
