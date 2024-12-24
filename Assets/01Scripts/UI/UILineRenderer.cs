using UnityEngine;
using UnityEngine.UI;

namespace GGM.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRenderer : MaskableGraphic
    {
        public Vector2[] points;
        public float thickness = 1f; //선의 두께
        public bool center = true; //중앙 정렬
        public Color lineColor; //선의 색상
        
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear(); //기존에 있던 데이터를 클리어한다.
            
            //그려줄 점이 모자랄 때는 그리지 않는다.
            if (points == null || points.Length < 2) return;

            for (int i = 0; i < points.Length - 1; i++)
            {
                CreateLineSegments(points[i], points[i + 1], vh);
                int index = i * 5; //한 점당 정점이 5개니까
                
                vh.AddTriangle(index, index + 1, index + 3); // 0, 1, 3
                vh.AddTriangle(index + 3, index + 2, index); // 3, 2, 0

                if (i != 0)
                {
                    vh.AddTriangle(index, index-1, index-3);
                    vh.AddTriangle(index + 1, index -1, index-2);
                }
            }
        }

        private void CreateLineSegments(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta * 0.5f) : Vector2.zero;
            
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = lineColor;
            
            //세그먼트 시작점 설정
            Quaternion point1Rot = Quaternion.Euler(0, 0, RotatePointToward(point1, point2) + 90f);
            vertex.position = point1Rot * new Vector3(-thickness * 0.5f, 0f);
            vertex.position += point1 - offset;
            vh.AddVert(vertex); //구조체라서 값복사로 들어가.
            vertex.position = point1Rot * new Vector3(thickness * 0.5f, 0f);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);
            
            Quaternion point2Rot = Quaternion.Euler(0, 0, RotatePointToward(point2, point1) - 90f);
            vertex.position = point2Rot * new Vector3(-thickness * 0.5f, 0f);
            vertex.position += point2 - offset;
            vh.AddVert(vertex); //구조체라서 값복사로 들어가.
            vertex.position = point2Rot * new Vector3(thickness * 0.5f, 0f);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);
            
            //마지막 점을 하나 추가할께
            vertex.position = point2 - offset;
            vh.AddVert(vertex);
        }

        private float RotatePointToward(Vector3 vertex, Vector3 target)
        => Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * Mathf.Rad2Deg;
    }
}
