using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // プレイヤーのTransform
    public TurnManager turnManager; // TurnManagerスクリプトの参照
    public Vector2 gridSize = new Vector2(0.3f, 0.3f); // グリッドのサイズを追加

    private GridSnap gridSnap; // GridSnapコンポーネントの参照

    private void Start()
    {
        gridSnap = GetComponent<GridSnap>(); // GridSnapコンポーネントを取得

        // プレイヤーのオブジェクトをタグで検索して参照を設定
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            turnManager = playerObject.GetComponent<TurnManager>(); // TurnManagerコンポーネントを取得
        }
        else
        {
            Debug.LogError("EnemyMovement: Player object with 'Player' tag not found.");
        }
    }

    public void EnemyMove()
    {
        if (target == null || turnManager == null)
        {
            Debug.LogError("EnemyMove: target or turnManager is not set.");
            return;
        }

        Vector2 currentPosition = transform.position; // 敵の現在の位置を取得

        Vector2 newPosition = GetClosestGridPosition(currentPosition);

        // 敵が移動先にプレイヤーがいる場合、その場で停止
        if (newPosition == (Vector2)target.position)
        {
            return;
        }

        // 敵の向きを更新
        UpdateEnemyDirection(currentPosition, newPosition);

        // 敵の位置を更新 (DoTweenでモーションを追加)
        transform.DOMove(newPosition, 0.1f).SetEase(Ease.Linear);
    }

    private Vector2 GetClosestGridPosition(Vector2 currentPosition)
    {
        // 上下左右4方向への移動候補を計算
        Vector2[] candidates = new Vector2[]
        {
            currentPosition + gridSize * Vector2.up,
            currentPosition + gridSize * Vector2.down,
            currentPosition + gridSize * Vector2.right,
            currentPosition + gridSize * Vector2.left
        };

        // 最もプレイヤーに近い移動候補を選択
        float minDistance = Mathf.Infinity;
        Vector2 closestCandidate = currentPosition;

        foreach (Vector2 candidate in candidates)
        {
            // 候補が現在の位置と同じ場合、スキップ
            if (candidate == currentPosition)
            {
                continue;
            }

            // 候補の位置をGridSnapを使ってスナップ
            Vector2 snappedCandidate = new Vector2(Mathf.Round(candidate.x / gridSize.x) * gridSize.x, Mathf.Round(candidate.y / gridSize.y) * gridSize.y);

            // プレイヤーに近いかどうかを確認
            float distanceToTarget = Vector2.Distance(snappedCandidate, target.position);
            if (distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                closestCandidate = snappedCandidate;
            }
        }

        return closestCandidate;
    }

    private void UpdateEnemyDirection(Vector2 currentPosition, Vector2 newPosition)
    {
        Vector2 direction = newPosition - currentPosition;
        float scaleX = Mathf.Abs(transform.localScale.x);
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ); // 左向き
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ); // 右向き
        }
    }
}
