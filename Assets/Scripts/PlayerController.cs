using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float minY = 1.9f;
    public float maxY = 2.5f;

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
    public void MoveRight()//横移動は画面外のゴール判定にぶつけるため、制限なし
    {
        Vector3 position = transform.position;
        position.x += moveSpeed;
        transform.position = position;
    }
    public void MoveLeft()//横移動は画面外のゴール判定にぶつけるため、制限なし
    {
        Vector3 position = transform.position;
        position.x -= moveSpeed;
        transform.position = position;
    }
}