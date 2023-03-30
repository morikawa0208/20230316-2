using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> enemies;

    public void ChangeTurn() // ターンの交代を行う関数
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("TurnManager: Player object with 'Player' tag not found.");
            return;
        }

        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement == null)
            {
                Debug.LogError("TurnManager: EnemyMovement component not found on enemy object.");
                continue;
            }

            AttackPattern attackPattern = enemy.GetComponent<AttackPattern>();
            if (attackPattern == null)
            {
                Debug.LogError("TurnManager: AttackPattern component not found on enemy object.");
                continue;
            }

            bool playerInRange = false;

            // 敵の攻撃範囲をチェック
            foreach (AttackRange attackRange in attackPattern.attackRanges)
            {
                Vector2 worldOffset = enemy.transform.TransformVector(attackRange.offset);
                Vector2 worldSize = enemy.transform.TransformVector(attackRange.size);
                Rect attackRect = new Rect(enemy.transform.position.x + worldOffset.x, enemy.transform.position.y + worldOffset.y, worldSize.x, worldSize.y);

                if (attackRect.Contains(playerObject.transform.position))
                {
                    playerInRange = true;
                    break;
                }
            }

            if (playerInRange)
            {
                // ここに敵オブジェクトの共通の攻撃行動を記述する
                Debug.Log("Turn:EnemyAttack");
                // 敵の攻撃処理を呼び出す（例：enemy.GetComponent<EnemyAttack>().Attack();）
            }
            else
            {
                // 敵の移動
                Debug.Log("Turn:EnemyMove");
                enemyMovement.EnemyMove();
            }
        }
    }
}
