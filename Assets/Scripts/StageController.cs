using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("StageUp"))
        {
            StageManager.instance.ChangeStage(1); // 右のオブジェクトに接触した場合
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("StageDown"))
        {
            StageManager.instance.ChangeStage(-1); // 左のオブジェクトに接触した場合
        }

        if (StageManager.instance.CurrentStage < 1)
        {
            StageManager.instance.CurrentStage = 1; // ステージ数が1未満にならないようにする
        }
    }
}