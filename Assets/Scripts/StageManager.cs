using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public int currentStage = 1;

    [SerializeField] private int minEnemyCount = 1;
    [SerializeField] private int maxEnemyCount = 3;

    [SerializeField] private float gridSpacing = 0.3f;
    private StageController stageController;
    [SerializeField] private List<EnemyList> enemyLists = new List<EnemyList>();

    [System.Serializable]
    public class EnemyList
    {
        public int startStage;
        public int endStage;
        public string name;
        public List<GameObject> enemyPrefabs = new List<GameObject>();
        public List<float> spawnProbabilities = new List<float>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        stageController = FindObjectOfType<StageController>();
    }

    public void ChangeStage(int stage)
    {
        currentStage += stage;
        Debug.Log($"Current Stage: {currentStage}");

        if (enemyLists.Count == 0) return;

        var enemyList = GetEnemyListByCurrentStage();
        if (enemyList == null) return;

        Debug.Log(enemyList.name);

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        var positions = new HashSet<Vector2>();

        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 position;
            do
            {
                float x = Mathf.Round(Random.Range(-2.4f, 2.4f) / gridSpacing) * gridSpacing;
                float y = Mathf.Round(Random.Range(1.9f, 2.5f) / gridSpacing) * gridSpacing;
                position = new Vector2(x, y);
            } while (positions.Contains(position));
            positions.Add(position);

            SpawnEnemyAtPosition(enemyList, position);
        }
    }

    private void SpawnEnemyAtPosition(EnemyList enemyList, Vector2 position)
    {
        float randomValue = Random.Range(0f, enemyList.spawnProbabilities.Sum());
        float spawnProbabilitySum = 0f;
        for (int i = 0; i < enemyList.enemyPrefabs.Count; i++)
        {
            spawnProbabilitySum += enemyList.spawnProbabilities[i];
            if (randomValue <= spawnProbabilitySum)
            {
                GameObject enemyPrefab = enemyList.enemyPrefabs[i];
                Quaternion rotation = stageController.isRight ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
                Instantiate(enemyPrefab, (Vector3)position, rotation);
                break;
            }
        }
    }

    private EnemyList GetEnemyListByCurrentStage()
    {
        return enemyLists.FirstOrDefault(enemyList => currentStage >= enemyList.startStage && currentStage <= enemyList.endStage);
    }

    private void Start()
    {
        currentStage = Mathf.Max(1, currentStage);
    }
}
