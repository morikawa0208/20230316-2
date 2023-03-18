using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //ステージ数の管理、増減、ステージ移動後の敵生成を行うスクリプト
    public static StageManager Instance { get; private set; }

    public int currentStage = 1;

    [SerializeField] private int minEnemyCount = 1;
    [SerializeField] private int maxEnemyCount = 3;

    [SerializeField] private float gridSpacing = 0.3f;
    [SerializeField] private List<EnemyList> enemyLists = new List<EnemyList>();

    [SerializeField] private List<int> startStages = new List<int>();
    [SerializeField] private List<int> endStages = new List<int>();

    [System.Serializable]
    public class EnemyList
    {
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
    }

    public void ChangeStage(int stage)
    {
        currentStage += stage;
        Debug.Log($"Current Stage: {currentStage}");

        if (enemyLists.Count == 0) return;

        // 現在のステージに対応する敵リストを取得
        var enemyList = GetEnemyListByCurrentStage();
        Debug.Log(enemyList.name);

        // 既存の敵オブジェクトを破棄
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        // 生成位置を格納するリストを作成
        var positions = new List<Vector2>();

        // 抽選された生成数だけ敵を生成
        var enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
        for (int i = 0; i < enemyCount; i++)
        {
            // 敵を生成する位置を計算
            var x = Random.Range(-2.4f, 2.4f);
            var y = Random.Range(1.9f, 2.5f);
            x = Mathf.Round(x / gridSpacing) * gridSpacing;
            y = Mathf.Round(y / gridSpacing) * gridSpacing;

            
            // すでにその位置に敵がいる場合は、別の位置を計算する
            if (positions.Contains(new Vector2(x, y))) {
                i--;
                continue;
            }
            positions.Add(new Vector2(x, y));


            // 敵をランダムに生成
            var randomValue = Random.Range(0f, enemyList.spawnProbabilities.Sum());
            var spawnProbabilitySum = 0f;
            for (int j = 0; j < enemyList.enemyPrefabs.Count; j++)
            {
                spawnProbabilitySum += enemyList.spawnProbabilities[j];
                if (randomValue <= spawnProbabilitySum)
                {
                    var enemyPrefab = enemyList.enemyPrefabs[j];
                    Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                }
            }
        }
    }

    private EnemyList GetEnemyListByCurrentStage()
    {
        if (currentStage < startStages[0] || currentStage > endStages[endStages.Count - 1]) return null;

        for (int i = 0; i < startStages.Count; i++)
        {
            if (currentStage >= startStages[i] && currentStage <= endStages[i])
            {
                return enemyLists[i];
            }
        }

        return null;
    }

    private void Start()
    {
        currentStage = GetCurrentStage();
    }

    private int GetCurrentStage()
    {
        if (currentStage < 1) return startStages[0];

        for (int i = 0; i < startStages.Count; i++)
        {
            if (currentStage >= startStages[i] && currentStage <= endStages[i])
            {
                return currentStage - startStages[i] + (i * (endStages[i] - startStages[i] + 1));
            }
        }

        return endStages[endStages.Count - 1] + (currentStage - endStages[endStages.Count - 1]) * (endStages[endStages.Count - 1] - startStages[startStages.Count - 1] + 1);
    }


}
