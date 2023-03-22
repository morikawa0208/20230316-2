using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float minY = 1.9f;
    public float maxY = 2.5f;
    //public GameManager gameManager;
    public PlayerStageMove playerStageMove;

    private int direction = 1; // プレイヤーの向きを保持するための変数
    private bool isMoving = false;

    private void Start()
    {
        playerStageMove = GetComponent<PlayerStageMove>();
    }

    public void MoveUp()
    {
        if (isMoving) return;

        isMoving = true;
        Vector3 targetPosition = transform.position + new Vector3(0, moveSpeed, 0);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            //gameManager.EndPlayerTurn();
        });
    }

    public void MoveDown()
    {
        if (isMoving) return;

        isMoving = true;
        Vector3 targetPosition = transform.position - new Vector3(0, moveSpeed, 0);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            //gameManager.EndPlayerTurn();
        });
    }

    public void MoveRight()
    {
        if (isMoving) return;

        isMoving = true;
        direction = -1;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        Vector3 targetPosition = transform.position + new Vector3(moveSpeed, 0, 0);
        transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            //gameManager.EndPlayerTurn();
        });
    }

    public void MoveLeft()
    {
        if (isMoving) return;

        isMoving = true;
        direction = 1;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        Vector3 targetPosition = transform.position - new Vector3(moveSpeed, 0, 0);

        if (StageManager.Instance.currentStage == 1)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, -2.7f, targetPosition.x);
        }

        transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            //gameManager.EndPlayerTurn();
        });
    }
}
