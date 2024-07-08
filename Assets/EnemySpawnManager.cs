using Assets;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    public int enemyTotal = 50;
    [SerializeField]
    public int enemyInMap = 10;
    [SerializeField]
    public int enemyOutMap = 0;
    [SerializeField]
    private GameObject activeEnemyParent;
    [SerializeField]
    private GameObject deactiveEnemyParent;
    [SerializeField]
    private List<GameObject> activeEnemyList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> deactiveEnemyList = new List<GameObject>();
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TMP_Text enemyLeftText;

    private static EnemySpawnManager _instance;
    //Create list color;
    //Random material color when spawned;
    public static EnemySpawnManager Instance
    {
        get
        {
            // If the instance is null, find an existing instance
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<EnemySpawnManager>();
            }

            return _instance;
        }
    }

    private void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        //Spawn enemy if there are no enemy in map
        SpawnEnemyToMap();
        SetEnemyLeftTextUI();
    }

    private void SetEnemyLeftTextUI()
    {
        enemyLeftText.text = enemyTotal.ToString();
    }

    private void SpawnEnemyToMap()
    {
        if (activeEnemyList.Count == 0 && activeEnemyParent.transform.childCount == 0)
        {
            deactiveEnemyList.Clear();
            for (int i = 0; i < deactiveEnemyParent.transform.childCount; i++)
            {
                deactiveEnemyList.Add(deactiveEnemyParent.transform.GetChild(i).gameObject);
            }

            while (deactiveEnemyList.Count > 0)
            {
                Statistic enemyStat = deactiveEnemyList[0].GetComponent<Statistic>();
                //Set enemy to new level
                int newLevel = Random.Range(0, 3);
                enemyStat.level = newLevel;
                deactiveEnemyList[0].GetComponent<EnemyManager>().scoreText.text = enemyStat.level.ToString();
                //Change size based on level
                deactiveEnemyList[0].transform.localScale *= Mathf.Pow(ConstantStat.increaseSize, newLevel);
                enemyStat.score = enemyStat.level;
                //Spawn enemy
                SpawnEnemy(deactiveEnemyList[0]);
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Vector2 positionX = new Vector2(-18f, 126f);
        Vector2 positionZ = new Vector2(-25f, 54f);

        float posX = Random.Range(positionX.x, positionX.y);
        float posZ = Random.Range(positionZ.x, positionZ.y);

        enemy.transform.position = new Vector3(posX, enemy.transform.position.y, posZ);

        BaseStateManager enemyStateManager = enemy.GetComponentInChildren<BaseStateManager>();

        //Enable collider 
        enemyStateManager.currentCollider.enabled = true;
        //Reset enemyStateManager
        enemyStateManager.ResetVariable();

        enemy.SetActive(true);

        enemy.transform.parent = activeEnemyParent.transform;

        if (deactiveEnemyList.Count > 0)
        {
            deactiveEnemyList.Remove(enemy);
            activeEnemyList.Add(enemy);
        }

        //Debug.Log("SpawnEnemy " + enemy.name);
    }

    private void LoadComponent()
    {
        activeEnemyParent = this.transform.Find("ActiveEnemy").gameObject;
        deactiveEnemyParent = this.transform.Find("DeactiveEnemy").gameObject;
        player = GameObject.Find("Player").gameObject;
        enemyLeftText = GameObject.Find("NumberOfEnemyLeft").GetComponent<TMP_Text>();
        GetActiveEnemyList();
    }

    private void GetActiveEnemyList()
    {
        for (int i = 0; i < activeEnemyParent.transform.childCount; i++)
        {
            activeEnemyList.Add(activeEnemyParent.transform.GetChild(i).gameObject);
        }
    }

    public void OnEnemyKilled(GameObject enemy, Statistic enemyStatistic)
    {
        //Decreased number of enemy in map
        enemyInMap--;
        //Increase number of enemy that had been killed
        enemyOutMap++;
        //Set enemy left text in UI
        int enemyleft = enemyTotal - enemyOutMap;
        //Debug.Log("Enemyleft " + enemyleft);

        enemyLeftText.text = enemyleft.ToString();

        //Player win when enemy left = 0;
        if (enemyleft == 0)
        {
            player.GetComponentInChildren<PlayerStateManager>().isWin = true;
            return;
        }

        //Spawn new enemy
        SpawnEnemyLeft(enemy, enemyStatistic);
    }

    private void SpawnEnemyLeft(GameObject enemy, Statistic enemyStat)
    {
        if (enemyInMap < 10 && (enemyOutMap + enemyInMap) < enemyTotal)
        {
            if (enemy != null)
            {
                //Set enemy level to axpproximate player level
                int level = Mathf.Clamp(enemyStat.level + Random.Range(-1, 3), 0, 20);

                enemy.transform.localScale *= Mathf.Pow(ConstantStat.increaseSize, (level - enemyStat.level));
                enemyStat.level = level;

                enemy.GetComponent<EnemyManager>().scoreText.text = enemyStat.level.ToString();

                enemyStat.score = enemyStat.level;


                //Change color & texture


                //Respawn enemy to map
                SpawnEnemy(enemy);
            }

            //Increase number of enemy in map
            enemyInMap++;
        }
        else
        {
            if (enemyOutMap > (enemyTotal - 10))
            {
                //Set enemy parent to deactive
                enemy.transform.parent = deactiveEnemyParent.transform;
                //Remove from active list
                activeEnemyList.Remove(enemy);
                //Add enemy to list
                deactiveEnemyList.Add(enemy);
            }
        }
    }

    public GameObject GetNearestEnemyActive(GameObject current)
    {
        GameObject nearestEnemy = null;
        float minDistance = float.MaxValue;
        //Create new list based on the active enemy list
        List<GameObject> allEnemyList = activeEnemyList;
        //Add player to the list
        allEnemyList.Add(player);
        //Loop in active enemy list to find the nearest
        if (allEnemyList.Count > 0)
        {
            foreach (var enemy in allEnemyList)
            {
                if (enemy != current && enemy.activeSelf)
                {
                    float distance = Vector3.Distance(current.transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }
        }
        return nearestEnemy;
    }
}
