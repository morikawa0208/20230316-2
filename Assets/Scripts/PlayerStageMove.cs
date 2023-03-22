using UnityEngine;

public class PlayerStageMove : MonoBehaviour
{
    private bool isOnUpStage = false;
    private bool isOnDownStage = false;

    private StageTransition stageTransition;

    private void Start()
    {
        stageTransition = FindObjectOfType<StageTransition>();//ステージ移動の暗転のためのスクリプト
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StageUp"))
        {
            stageTransition.MoveRight();//ステージ移動の暗転
            transform.position = new Vector2(-2.7f, transform.position.y);
            isOnUpStage = true;
        }
        else if (other.CompareTag("StageDown"))
        {
            stageTransition.MoveLeft();//ステージ移動の暗転
            transform.position = new Vector2(2.7f, transform.position.y);
            isOnDownStage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StageUp"))
        {
            isOnUpStage = false;
        }
        else if (other.CompareTag("StageDown"))
        {
            isOnDownStage = false;
        }
    }

    private void Update()
    {
        if (isOnUpStage)
        {
            transform.position = new Vector2(-2.7f, transform.position.y);
        }
        else if (isOnDownStage)
        {
            transform.position = new Vector2(2.7f, transform.position.y);
        }
    }
}
