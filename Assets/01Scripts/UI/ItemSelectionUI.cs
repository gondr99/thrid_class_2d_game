using System;
using DG.Tweening;
using UnityEngine;

namespace GGM.UI
{
    public class ItemSelectionUI : MonoBehaviour
    {
        [SerializeField] private float _tweenDuration = 0.2f;
        
        public RectTransform RectTrm => transform as RectTransform;

        public void MoveAnchorPosition(Vector2 anchorPosition, bool isTween = false)
        {
            if (isTween)
            {
                RectTrm.DOKill();
                RectTrm.DOAnchorPos(anchorPosition, _tweenDuration).SetUpdate(true);
            }
            else
            {
                RectTrm.anchoredPosition = anchorPosition;
            }
        }
    }
}
