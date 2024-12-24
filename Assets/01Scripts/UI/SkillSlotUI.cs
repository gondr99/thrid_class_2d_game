using System;
using GGM.UI;
using UnityEngine;

namespace GGM
{
    public class SkillSlotUI : MonoBehaviour
    {
        [SerializeField] private SkillSlotUI _parent;
        [SerializeField] private UILineRenderer _lineRenderer;
        public RectTransform RectTrm => transform as RectTransform;
        //UI밸리데이팅은 씬 저장, 리프레시 등에서만 발생한다. 일반적인 SO처럼 값이 변할때 발생하지 않는다.
        private void OnValidate()
        {
            if (_parent != null)
            {
                Vector2 size = RectTrm.sizeDelta;

                Vector3 startPosition = new Vector3(size.x * 0.5f, size.y);
                Vector3 relativePos = transform.InverseTransformPoint(_parent.transform.position);
                Vector3 delta = relativePos - startPosition + new Vector3(size.x * 0.5f, 0);
                Vector3 endPosition = startPosition + delta;
                Vector3 middlePos = delta * 0.5f + startPosition;

                
                //알맞게 점을 구한다음에 라인렌더러에 넣어주면 된다.
                _lineRenderer.points = new Vector2[4]
                {
                    startPosition,
                    new Vector2(startPosition.x, middlePos.y),
                    new Vector2(endPosition.x, middlePos.y),
                    endPosition
                };


            }
        }
    }
}
