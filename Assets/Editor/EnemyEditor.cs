using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 通常のインスペクタ表示
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        Enemy enemy = (Enemy)target;
        Transform enemyTransform = enemy.transform;

        // 攻撃範囲のプレビューをシーンビューに表示
        Handles.color = new Color(1, 0, 0, 0.5f);

        // 全てのAttackRangeに対してループ
        foreach (var attackRange in enemy.attackRanges)
        {
            Vector3 worldOffset = enemyTransform.TransformVector(attackRange.offset);
            Vector3 worldSize = enemyTransform.TransformVector(attackRange.size);
            Handles.DrawSolidRectangleWithOutline(
                new Rect(enemyTransform.position + worldOffset, worldSize),
                new Color(1, 0, 0, 0.2f),
                Color.red
            );
        }
    }
}
