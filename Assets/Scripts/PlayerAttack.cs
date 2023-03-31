using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public TurnManager turnManager; // TurnManagerスクリプトの参照
    public AttackPattern defaultAttackPattern; // 素手でのデフォルトの攻撃パターン
    public AttackPattern currentWeapon; // 現在の武器（nullの場合は素手）

    private void Start()
    {
        // プレイヤーのオブジェクトをタグで検索して参照を設定
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            turnManager = playerObject.GetComponent<TurnManager>(); // TurnManagerコンポーネントを取得
        }
        else
        {
            Debug.LogError("EnemyMovement: Player object with 'Player' tag not found.");
        }
    }

    public void Attack()
    {
        AttackPattern attackPattern = currentWeapon != null ? currentWeapon : defaultAttackPattern;

        Collider2D[] colliders = new Collider2D[10];
        int layerMask = LayerMask.GetMask("Enemy"); // 敵のレイヤーマスクを取得（敵が"Enemy"レイヤーにあると仮定）

        foreach (var attackRange in attackPattern.attackRanges)
        {
            Vector2 worldSize = attackRange.size;
            Vector2 worldOffset = attackRange.offset;

            int numColliders = Physics2D.OverlapBoxNonAlloc(transform.position + (Vector3)worldOffset, worldSize, 0, colliders, layerMask);

            for (int i = 0; i < numColliders; i++)
            {
                Collider2D collider = colliders[i];
                // 敵にダメージを与える処理を実装
                Debug.Log("敵に攻撃がヒット");
            }
        }

        turnManager.ChangeTurn();
    }
}
