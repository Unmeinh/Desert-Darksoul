using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete_Scripts : MonoBehaviour
{
    public Mage_Scripts mageScripts;
    public Transform camera;
    private Text textCoin,
        textEnemy;

    int starPoint = 0,
        coinPoint = 0,
        enemyPoint = 0;

    void Start()
    {
        textCoin = GameObject.Find("TextCoin").GetComponent<Text>();
        textEnemy = GameObject.Find("TextEnemy").GetComponent<Text>();
        starPoint = mageScripts.starPoint;
        coinPoint = mageScripts.coinPoint;
        enemyPoint = mageScripts.enemyPoint;

        transform.position = new Vector3(camera.position.x, camera.position.y, -7);
    }

    void Update()
    {
        transform.position = new Vector3(camera.position.x, camera.position.y, -7);
        textCoin.text = "x " + coinPoint.ToString();
        textEnemy.text = "x " + enemyPoint.ToString();
        SetRatingStar();
    }

    private void SetRatingStar()
    {
        Sprite[] allStar = Resources.LoadAll<Sprite>("GUI/ratingstar");

        if (starPoint > 0)
        {
            for (int i = 0; i <= starPoint; i++)
            {
                StartCoroutine(SetStar(allStar[i]));
            }
        }
    }

    IEnumerator SetStar(Sprite starSprite)
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Rating_Star").GetComponent<Image>().sprite = starSprite;
    }
}
