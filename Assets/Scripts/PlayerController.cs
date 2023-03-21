using UnityEngine;
using UnityEngine.UI;

//移動ボタンを押した時に対応する方向へプレイヤーが移動するスクリプト
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float minY = 1.9f;
    public float maxY = 2.5f;

    private int direction = 1; // プレイヤーの向きを保持するための変数

    public void MoveUp()
    {
        Vector3 position = transform.position;
        position.y += moveSpeed;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }

    public void MoveDown()
    {
        Vector3 position = transform.position;
        position.y -= moveSpeed;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }

    public void MoveRight()
    {
        direction = -1; // 向きを反転する
        Vector3 position = transform.position;
        position.x += moveSpeed;
        transform.position = position;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z); // スケールを反転する
    }

    public void MoveLeft()
    {
        direction = 1; // 向きを反転する
        Vector3 position = transform.position;
        if (StageManager.Instance.currentStage == 1)
        {
            position.x = Mathf.Clamp(position.x - moveSpeed, -2.7f, position.x);
        }
        else
        {
            position.x -= moveSpeed;
        }
        transform.position = position;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z); // スケールを反転する
    }
}