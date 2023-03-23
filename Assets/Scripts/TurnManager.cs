using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public void ChangeTurn()// ターンの交代を行う関数
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // ここに敵オブジェクトの共通の行動を記述する
            Debug.Log("Turn:Enemy");

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.EnemyMove();//敵の移動
        }   
    }
}
