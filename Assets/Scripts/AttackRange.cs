using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackRange
{
    public Vector2 size = new Vector2(1, 1);
    public Vector2 offset = Vector2.zero;
}

public class AttackPattern : MonoBehaviour
{
    public List<AttackRange> attackRanges = new List<AttackRange>();
}
