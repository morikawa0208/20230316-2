using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float minX = -2.7f;
    public float maxX = 2.7f;
    public float minY = 1.5f;
    public float maxY = 2.7f;
    public TurnManager turnManager;
    public PlayerStageMove playerStageMove;
    public Vector2 gridSize = new Vector2(0.3f, 0.3f);

    private bool isMoving = false;
    private GridSnap gridSnap;

    private void Start()
    {
        playerStageMove = GetComponent<PlayerStageMove>();
        gridSnap = GetComponent<GridSnap>();
    }

    public void Move(Vector2 direction)
    {
        if (isMoving) return;

        Vector3 targetPosition = new Vector3(
            transform.position.x + direction.x *gridSize.x,
            Mathf.Round((transform.position.y + direction.y * gridSize.y) / gridSize.y) * gridSize.y,0
        );

        // 上下移動の制限
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // 左移動の制限（ステージが1階の時だけ）
        if (direction.x < 0 && StageManager.Instance.currentStage == 1)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        }

        if (targetPosition == transform.position)
        {
            return;
        }

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(-direction.x, transform.localScale.y, transform.localScale.z);
        }

        isMoving = true;
        gridSnap.enabled = false;
        transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            gridSnap.enabled = true;
        });
        turnManager.ChangeTurn(); //プレイヤーのターン終了
    }

public void MoveUp() => Move(Vector2.up);

public void MoveDown() => Move(Vector2.down);

public void MoveRight() => Move(Vector2.right);

public void MoveLeft() => Move(Vector2.left);
}