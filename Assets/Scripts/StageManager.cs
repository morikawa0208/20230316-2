using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private int currentStage = 1;

    public int CurrentStage // プロパティ
    {
        get { return currentStage; }
        set { currentStage = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeStage(int stage)
    {
        currentStage += stage;
        Debug.Log("Current Stage: " + currentStage);
    }
}
