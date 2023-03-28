using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // プレイヤーのTransform
    public TurnManager turnManager; // TurnManagerスクリプトの参照
    public float moveDuration = 0.1f; // 移動にかかる時間
    public LayerMask collisionLayerMask; // 衝突判定の対象となるレイヤーマスク
    public Vector2 gridSize = new Vector2(1f, 1f); // グリッドのサイズを追加

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

        // 敵の位置を更新 (DoTweenでモーションを追加)
        transform.DOMove(newPosition, moveDuration).SetEase(Ease.Linear);
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

        // 最もプレイヤーに近く、他のオブジェクトと重ならない移動候補を選択
        float minDistance = Mathf.Infinity;
        Vector2 closestCandidate = currentPosition;
        float radius = 0.1f; // 重なり判定の半径
        Collider2D[] results = new Collider2D[1]; // 重なり判定の結果を格納する配列

        foreach (Vector2 candidate in candidates)
        {
            // 候補が現在の位置と同じ場合、スキップ
            if (candidate == currentPosition)
                    {
                continue;
            }

            // 候補の位置をGridSnapを使ってスナップ
            Vector2 snappedCandidate = new Vector2(Mathf.Round(candidate.x / gridSize.x) * gridSize.x, Mathf.Round(candidate.y / gridSize.y) * gridSize.y);

            // 重なり判定を行い、他の敵やプレイヤーと重なっている場合、スキップ
            int overlapCount = Physics2D.OverlapCircleNonAlloc(snappedCandidate, radius, results, collisionLayerMask);
            if (overlapCount > 0)
            {
                continue;
            }

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
}
