using UnityEngine;

//ステージ外のGoalRightオブジェクト、GoalLeftオブジェクトに接触した時にプレイヤーを反対の端に移動させるスクリプト
public class PlayerStageMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "StageUp")
        {
            transform.position = new Vector2(-2.7f, transform.position.y);
        }
        else if (other.tag == "StageDown")
        {
            transform.position = new Vector2(2.7f, transform.position.y);
        }
    }
}
