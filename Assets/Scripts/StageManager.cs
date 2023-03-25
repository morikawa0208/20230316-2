using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // シングルトンインスタンス

    public int currentStage = 1; // 現在のステージ番号

    [SerializeField] private int minEnemyCount = 1; // 敵の最小数
    [SerializeField] private int maxEnemyCount = 3; // 敵の最大数

    private const float MinXRange = -2.4f;
    private const float MaxXRange = 2.4f;
    private const float MinYRange = 1.6f;
    private const float MaxYRange = 2.8f;

    private float gridSpacing = 0.3f; // グリッド間のスペース
    private StageController stageController; // StageControllerの参照
    [SerializeField] private List<EnemyList> enemyLists = new List<EnemyList>(); // 敵リストのリスト

    [System.Serializable]
    public class EnemyList
    {
        public int startStage; // 開始ステージ
        public int endStage; // 終了ステージ
        public string name; // 敵リストの名前
        public List<GameObject> enemyPrefabs = new List<GameObject>(); // 敵プレファブのリスト
        public List<float> spawnProbabilities = new List<float>(); // スポーン確率のリスト

        // スポーン確率に基づいてランダムな敵を返す
        public GameObject GetRandomEnemy()
        {
            float randomValue = Random.Range(0f, spawnProbabilities.Sum());
            float spawnProbabilitySum = 0f;
            for (int i = 0; i < enemyPrefabs.Count; i++)
            {
                spawnProbabilitySum += spawnProbabilities[i];
                if (randomValue <= spawnProbabilitySum)
                {
                    return enemyPrefabs[i];
                }
            }

            return null;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // シングルトンインスタンスの設定
    }

    // StageControllerを設定する
    public void SetStageController(StageController controller)
    {
        stageController = controller;
    }

    // ステージを変更する
    public void ChangeStage(int stage, bool isRight)
    {
        int previousStage = currentStage;
        currentStage += stage;
        Debug.Log($"Current Stage: {currentStage}");

        ClearEnemies();

        if (currentStage == 1)
        {
            return;
        }

        if (enemyLists.Count == 0) return;

        var enemyList = GetEnemyListByCurrentStage();
        if (enemyList == null) return;

        Debug.Log(enemyList.name);

        SpawnEnemies(enemyList, isRight);
    }

    // シーン内のすべての敵を削除する
    private void ClearEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    // 与えられた敵リストに基づいて敵をシーンにスポーンさせる
    private void SpawnEnemies(EnemyList enemyList, bool isRight)
    {
        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
        var positions = new HashSet<Vector2>();

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 position = GetUniqueRandomPosition(positions);
            positions.Add(position);

            GameObject enemyPrefab = enemyList.GetRandomEnemy();
            if (enemyPrefab != null)
            {
                Quaternion rotation = !isRight ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
                Instantiate(enemyPrefab, (Vector3)position, rotation);
            }
        }
    }

    // 与えられた位置のセットに含まれていないユニークなランダム位置を返す
    private Vector2 GetUniqueRandomPosition(HashSet<Vector2> positions)
    {
        Vector2 position;
        do
        {
            float x = Mathf.Round(Random.Range(MinXRange, MaxXRange) / gridSpacing) * gridSpacing;
            float y = Mathf.Round(Random.Range(MinYRange, MaxYRange) / gridSpacing) * gridSpacing;
            position = new Vector2(x, y);
        } while (positions.Contains(position));

        return position;
    }

     // 現在のステージに基づいて敵リストを取得する
    private EnemyList GetEnemyListByCurrentStage()
    {
        return enemyLists.FirstOrDefault(enemyList => currentStage >= enemyList.startStage && currentStage <= enemyList.endStage);
    }

    private void Start()
    {
        currentStage = Mathf.Max(1, currentStage);
    }
}
