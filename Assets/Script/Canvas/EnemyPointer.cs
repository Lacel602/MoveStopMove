using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    private RectTransform pointerRectTransform;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Color enemyColor;
    [SerializeField]
    private GameObject score;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Statistic stat;
    private void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        enemyColor = target.transform.Find("GFX/initialShadingGroup1").GetComponent<SkinnedMeshRenderer>().material.color;
        arrow.transform.Find("Image").GetComponent<Image>().color = enemyColor;
        score.GetComponent<Image>().color = enemyColor;
        stat = target.GetComponent<Statistic>();
    }

    private void Update()
    {
        MoveAndRotateIndicator();
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = stat.level.ToString();
    }

    private void LoadComponent()
    {
        //target = GameObject.Find("ActiveEnemy").transform.GetChild(0).gameObject;
        arrow = this.transform.Find("Arrow").gameObject;
        pointerRectTransform = arrow.GetComponent<RectTransform>();
        player = GameObject.Find("Player").transform;
        score = this.transform.Find("Score").gameObject;
        scoreText = score.GetComponentInChildren<TMP_Text>();
    }


    private void MoveAndRotateIndicator()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

        bool isOffScreen = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen && target.activeSelf)
        {
            arrow.gameObject.SetActive(true);
            score.gameObject.SetActive(true);

            //Move arrow and score
            Vector3 cappedScreenPos = screenPos;
            cappedScreenPos.z = 0;
            cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, 60, Screen.width - 60);
            cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, 60, Screen.height - 60);

            arrow.transform.position = cappedScreenPos;
            score.transform.position = cappedScreenPos;

            //Rotate arrow
            //Get center point of screen
            Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);

            //Caculate the angle tan(y/x)
            float angle = Mathf.Atan2(cappedScreenPos.y - centerScreen.y, cappedScreenPos.x - centerScreen.x) * Mathf.Rad2Deg;

            //Rotate the arrow 
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            arrow.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
        }
    }
}
