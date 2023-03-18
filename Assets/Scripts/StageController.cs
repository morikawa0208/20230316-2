using UnityEngine;

//ステージ外のGoalRightオブジェクト、GoalLeftオブジェクトに接触した時にステージ数を増減するスクリプト
public class StageController : MonoBehaviour
{
    [SerializeField] private bool isRight = false; // StageUpが右側かどうか

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.Instance.ChangeStage(isRight ? 1 : -1);
        }
    }
}
