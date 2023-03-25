using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackPattern))]
public class AttackPatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 通常のインスペクタ表示
        base.OnInspectorGUI();

        AttackPattern pattern = (AttackPattern)target;

        // 攻撃範囲の設定を表示
        EditorGUILayout.LabelField("Attack Ranges", EditorStyles.boldLabel);

        for (int i = 0; i < pattern.attackRanges.Count; i++)
        {
            EditorGUILayout.LabelField($"Attack Range {i + 1}", EditorStyles.boldLabel);
            pattern.attackRanges[i].size = EditorGUILayout.Vector2Field("Size", pattern.attackRanges[i].size);
            pattern.attackRanges[i].offset = EditorGUILayout.Vector2Field("Offset", pattern.attackRanges[i].offset);
        }
    }

    private void OnSceneGUI()
    {
        AttackPattern pattern = (AttackPattern)target;
        Transform patternTransform = pattern.transform;

        // 攻撃範囲のプレビューをシーンビューに表示
        Handles.color = new Color(1, 0, 0, 0.5f);
        for (int i = 0; i < pattern.attackRanges.Count; i++)
        {
            Vector3 worldOffset = patternTransform.TransformVector(pattern.attackRanges[i].offset);
            Vector3 worldSize = patternTransform.TransformVector(pattern.attackRanges[i].size);
            Handles.DrawSolidRectangleWithOutline(
                new Rect(patternTransform.position.x + worldOffset.x, patternTransform.position.y + worldOffset.y, worldSize.x, worldSize.y),
                new Color(1, 0, 0, 0.2f),
                Color.red
            );
        }
    }
}
