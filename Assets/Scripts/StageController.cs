using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] public bool isRight = false;

    private void Awake()
    {
        StageManager.Instance.SetStageController(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager.Instance.ChangeStage(isRight ? 1 : -1, isRight);
        }
    }
}