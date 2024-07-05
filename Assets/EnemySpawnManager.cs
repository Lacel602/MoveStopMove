using Assets;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public int enemyTotal = 50;
    public int enemyInMap = 10;
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
    private Statistic playerStat;
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
    }

    private void SpawnEnemyToMap()
    {
        if (activeEnemyList.Count == 0 && activeEnemyParent.transform.childCount == 0)
        {
            foreach (var enemy in deactiveEnemyList)
            {
                Statistic enemyStat = enemy.GetComponent<Statistic>();
                enemyStat.level = (int) Random.Range(1, 3);
                enemyStat.score = enemyStat.level;
                SpawnEnemy(enemy);
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
        //Set enemy alive to true
        enemyStateManager.isAlive = true;

        enemy.SetActive(true);

        enemy.transform.parent = activeEnemyParent.transform;

        if (deactiveEnemyList.Count > 0) { 
            deactiveEnemyList.Remove(enemy);
            activeEnemyList.Add(enemy);
        }

        //Debug.Log("SpawnEnemy " + enemy.name);
    }

    private void LoadComponent()
    {
        activeEnemyParent = this.transform.Find("ActiveEnemy").gameObject;
        deactiveEnemyParent = this.transform.Find("DeactiveEnemy").gameObject;
        playerStat = GameObject.Find("Player").GetComponent<Statistic>();
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

        if (enemyLeftText == null)
        {
            Debug.Log(activeEnemyList[0].name);
        }
        enemyLeftText.text = enemyleft.ToString();
        //Spawn new enemy
        SpawnEnemyLeft(enemy, enemyStatistic);
    }

    private void SpawnEnemyLeft(GameObject enemy, Statistic enemyStat)
    {
        if (enemyInMap < 10 && enemyOutMap < enemyTotal)
        {
            if (enemy != null)
            {
                //Set enemy level to axpproximate player level
                int level = Mathf.Clamp(enemyStat.level + Random.Range(-1, 3), 0, 20);
                enemyStat.level = level;
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
            if (enemyOutMap >= enemyTotal)
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
}
