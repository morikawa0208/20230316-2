using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // プレイヤーのTransform
    public TurnManager turnManager; // TurnManagerスクリプトの参照
    public float moveDuration = 0.1f; // 移動にかかる時間

    private void Start()
    {
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

        // 敵の位置を更新 (DoTweenでモーションを追加)
        transform.DOMove(newPosition, moveDuration).SetEase(Ease.Linear);
    }

    private Vector2 GetClosestGridPosition(Vector2 currentPosition)
    {
        // 上下左右4方向への移動候補を計算
        Vector2[] candidates = new Vector2[]
        {
            new Vector2(currentPosition.x, Mathf.Clamp(currentPosition.y + 0.3f, 1.6f, 2.8f)),
            new Vector2(currentPosition.x, Mathf.Clamp(currentPosition.y - 0.3f, 1.6f, 2.8f)),
            new Vector2(Mathf.Clamp(currentPosition.x + 0.3f, -2.7f, 2.7f), currentPosition.y),
            new Vector2(Mathf.Clamp(currentPosition.x - 0.3f, -2.7f, 2.7f), currentPosition.y)
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

            // プレイヤーに近いかどうかを確認
            float distanceToTarget = Vector2.Distance(candidate, target.position);
            if (distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                closestCandidate = candidate;
            }
        }
        return closestCandidate;
    }
}