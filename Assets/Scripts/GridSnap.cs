using UnityEngine;

public class GridSnap : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(0.3f, 0.3f);

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / gridSize.x) * gridSize.x;
        position.y = Mathf.Round(position.y / gridSize.y) * gridSize.y;
        transform.position = position;
    }
}