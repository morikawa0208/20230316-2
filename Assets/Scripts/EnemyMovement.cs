using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // 移動速度
    private Vector2 targetPosition; // 移動先の位置
    private GameObject player; // プレイヤーオブジェクト
    private LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position; // 最初は移動しない
        player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーオブジェクトを検索して取得
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    public void EnemyMove()
    {
        float step = moveSpeed * Time.deltaTime; // 移動速度をフレームレートに依存しないように調整
        targetPosition = GetNewTargetPosition(); // 新しい移動先を取得する
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step); // 新しい移動先に向かって移動する
    }

    // プレイヤーに近づきつつ、他の敵オブジェクトと重ならないように新しい移動先を決める関数
    private Vector2 GetNewTargetPosition()
    {
        Vector2 newPosition = player.transform.position;
        float distance = Vector2.Distance(transform.position, newPosition);

        if (distance > 0.3f)
        {
            // プレイヤーの方向に進む
            Vector2 direction = (newPosition - (Vector2)transform.position).normalized;
            float x = direction.x;
            float y = direction.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                newPosition.x = transform.position.x + Mathf.Sign(x) * 0.3f;
                newPosition.y = transform.position.y;
            }
            else
            {
                newPosition.y = transform.position.y + Mathf.Sign(y) * 0.3f;
                newPosition.x = transform.position.x;
            }
        }

        // 画面外に出ないように位置を制限する
        newPosition.x = Mathf.Clamp(newPosition.x, -2.7f, 2.7f);
        newPosition.y = Mathf.Clamp(newPosition.y, 1.9f, 2.5f);

        // 他の敵オブジェクトと重ならないようにする
        RaycastHit2D hit = Physics2D.Linecast(transform.position, newPosition, enemyLayer);
        if (hit.collider != null)
        {
            return transform.position; // 重なる場合は移動しない
        }

        return newPosition;
    }
}
