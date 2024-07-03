using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private GameObject activeEnemyParent;
    private GameObject deactiveEnemyParent;

    private static EnemySpawnManager instance;

    //Create list color;

    //Random material color when spawned;
    public static EnemySpawnManager Instance
    {
        get { return instance; }

    }

    private void Reset()
    {
        activeEnemyParent = this.transform.Find("ActiveEnemy").gameObject;
        deactiveEnemyParent = this.transform.Find("DeactiveEnemy").gameObject;  
    }
}
