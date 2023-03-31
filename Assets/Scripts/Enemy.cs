using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<AttackPattern> attackPatterns = new List<AttackPattern>();
    public int maxHP; // 最大HP
    public int currentHP; // 現在のHP
    public int attack; // 攻撃力
    public int defense; // 防御力

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; // 現在のHPを最大HPに設定
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
