using UnityEngine;
using DG.Tweening;

public class StageTransition : MonoBehaviour
{
    [SerializeField] private float animationDuration = 1f; // アニメーションの時間
    [SerializeField] private RectTransform panel; // 移動するパネル
    [SerializeField] private float xDistance = 800f; // X軸方向の移動距離
    [SerializeField] private Vector2 startPosition; // 移動開始位置
    [SerializeField] private Vector2 endPosition; // 移動終了位置

    private void Start()
    {
        panel.anchoredPosition = startPosition; // 初期位置に移動
    }

    // 右のステージに移動するときのアニメーション
    public void MoveRight()
    {
        panel.anchoredPosition = new Vector2(startPosition.x + xDistance,startPosition.y); // 移動開始位置に移動
        // 左に移動するアニメーション
        panel.DOAnchorPos(endPosition, animationDuration);
    }

    // 左のステージに移動するときのアニメーション
    public void MoveLeft()
    {
        panel.anchoredPosition = new Vector2(endPosition.x - xDistance,endPosition.y); // 移動終了位置に移動
        // 右に移動するアニメーション
        panel.DOAnchorPos(startPosition, animationDuration);
    }
}
