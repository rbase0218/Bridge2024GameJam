using UnityEngine;
using UnityEditor;

public class RectTransformArranger : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField]
    private float xGap = 100f;
    
    [SerializeField]
    private float yGap = 100f;
    
    [SerializeField]
    private int columnsPerRow = 3;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransformArranger requires a RectTransform component!");
        }
    }

    [ContextMenu("Arrange Children in Grid")]
    public void ArrangeChildrenInGrid()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("RectTransformArranger requires a RectTransform component!");
                return;
            }
        }

        int childCount = rectTransform.childCount;
        
        if (childCount == 0)
        {
            Debug.LogWarning("No child objects found to arrange.");
            return;
        }

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            
            if (child == null) continue;
            
            int row = i / columnsPerRow;
            int column = i % columnsPerRow;
            
            Vector2 newPosition = new Vector2(
                column * xGap,
                -row * yGap  // Y축은 위에서 아래로
            );
            
            child.anchoredPosition = newPosition;
        }
        
        // Debug.Log($"Arranged {childCount} child RectTransforms in grid. X Gap: {xGap}, Y Gap: {yGap}");
    }

    [ContextMenu("Reset Children Positions")]
    public void ResetChildrenPositions()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("RectTransformArranger requires a RectTransform component!");
                return;
            }
        }

        int childCount = rectTransform.childCount;
        
        if (childCount == 0)
        {
            Debug.LogWarning("No child objects found to reset.");
            return;
        }

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            
            if (child == null) continue;
            
            child.anchoredPosition = Vector2.zero;
        }
        
        // Debug.Log($"Reset {childCount} child RectTransforms positions to zero.");
    }

    // 런타임에서 호출할 수 있는 public 메서드들
    public void SetGap(float x, float y)
    {
        xGap = x;
        yGap = y;
    }
    
    public void SetColumnsPerRow(int columns)
    {
        columnsPerRow = columns;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(RectTransformArranger))]
public class RectTransformArrangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 Inspector 그리기
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        RectTransformArranger arranger = (RectTransformArranger)target;
        
        // 버튼 1: 그리드 배치
        if (GUILayout.Button("Arrange Children in Grid", GUILayout.Height(30)))
        {
            // Undo 기록
            Undo.RecordObject(arranger.transform, "Arrange Children in Grid");
            
            // 모든 자식들의 RectTransform도 Undo에 기록
            RectTransform rectTransform = arranger.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                for (int i = 0; i < rectTransform.childCount; i++)
                {
                    RectTransform child = rectTransform.GetChild(i) as RectTransform;
                    if (child != null)
                    {
                        Undo.RecordObject(child, "Arrange Children in Grid");
                    }
                }
            }
            
            arranger.ArrangeChildrenInGrid();
            
            // Scene 뷰 업데이트
            EditorUtility.SetDirty(arranger);
            SceneView.RepaintAll();
        }
        
        EditorGUILayout.Space();
        
        // 버튼 2: 포지션 리셋
        if (GUILayout.Button("Reset All Positions to Zero", GUILayout.Height(30)))
        {
            // Undo 기록
            Undo.RecordObject(arranger.transform, "Reset Children Positions");
            
            // 모든 자식들의 RectTransform도 Undo에 기록
            RectTransform rectTransform = arranger.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                for (int i = 0; i < rectTransform.childCount; i++)
                {
                    RectTransform child = rectTransform.GetChild(i) as RectTransform;
                    if (child != null)
                    {
                        Undo.RecordObject(child, "Reset Children Positions");
                    }
                }
            }
            
            arranger.ResetChildrenPositions();
            
            // Scene 뷰 업데이트
            EditorUtility.SetDirty(arranger);
            SceneView.RepaintAll();
        }
        
        EditorGUILayout.Space();
        
        // 도움말
        EditorGUILayout.HelpBox("이 스크립트는 자식 RectTransform들의 위치를 조정합니다. 첫 번째 버튼은 격자 형태로 배치하고, 두 번째 버튼은 모든 위치를 (0,0)으로 리셋합니다.", MessageType.Info);
    }
}
#endif