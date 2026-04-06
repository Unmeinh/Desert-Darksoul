using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage_Scripts : MonoBehaviour
{
    public GameObject heartGO,
        levelComplete,
        canvasHint;
    private Animator animatorGO;
    private RectTransform rectTransformHp;
    private Text textCoinPoint,
        textKeyPoint;
    private Image healthBar;

    public bool isGameOver = false;
    public float speedX,
        speedY;
    bool haveKey = false;
    float waitTime = 0F;
    float healthPoint;
    public int coinPoint = 0;
    public int starPoint = 0;
    public int enemyPoint = 0;
    int keyPoint = 0;

    //healthBar: 364.5 - 464.5
    //healthPoint: 100

    void Start()
    {
        textCoinPoint = GameObject.Find("Text_Coin_Num").GetComponent<Text>();
        textKeyPoint = GameObject.Find("Text_Key_Num").GetComponent<Text>();
        healthBar = GameObject.Find("Fill_Bar").GetComponent<Image>();
        rectTransformHp = healthBar.GetComponent<RectTransform>();
        rectTransformHp.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Right,
            464.5f - healthPoint,
            healthPoint
        );
        animatorGO = GetComponent<Animator>();

        healthPoint = 100;

        animatorGO.SetBool("isBeingIdle", true);
        animatorGO.SetBool("isWalking", false);
        animatorGO.SetBool("isRunning", false);
        animatorGO.SetBool("isJumping", false);
        animatorGO.SetBool("isAttacking", false);
        animatorGO.SetBool("isDying", false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            rectTransformHp.SetInsetAndSizeFromParentEdge(
                RectTransform.Edge.Right,
                464.5f - healthPoint,
                healthPoint
            );
            Movement();
            SetviewStar();

            if (heartGO != null)
            {
                if (healthPoint == 100)
                {
                    heartGO.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else
                {
                    heartGO.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }

            if (canvasHint != null)
            {
                if (transform.position.x >= -7.5f)
                {
                    canvasHint.SetActive(false);
                }
                else
                {
                    canvasHint.SetActive(true);
                }
            }
        }
    }

    private void SetviewStar()
    {
        Sprite threeStar = LoadMultipleSprite("viewStar_3", "GUI/viewStar");
        Sprite twoStar = LoadMultipleSprite("viewStar_2", "GUI/viewStar");
        Sprite oneStar = LoadMultipleSprite("viewStar_1", "GUI/viewStar");
        Sprite noStar = LoadMultipleSprite("viewStar_0", "GUI/viewStar");

        if (starPoint == 3)
        {
            GameObject.Find("View_Star").GetComponent<Image>().sprite = threeStar;
            isGameOver = true;
            animatorGO.enabled = false;
            StartCoroutine(AfterDeath());
        }
        if (starPoint == 2)
        {
            GameObject.Find("View_Star").GetComponent<Image>().sprite = twoStar;
        }
        if (starPoint == 1)
        {
            GameObject.Find("View_Star").GetComponent<Image>().sprite = oneStar;
        }
        if (starPoint == 0)
        {
            GameObject.Find("View_Star").GetComponent<Image>().sprite = noStar;
        }
    }

    Sprite LoadMultipleSprite(string imageName, string spriteName)
    {
        Sprite[] all = Resources.LoadAll<Sprite>(spriteName);

        foreach (var s in all)
        {
            if (s.name == imageName)
            {
                return s;
            }
        }
        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin Tags")
        {
            coinPoint++;
            Destroy(collision.gameObject);
            textCoinPoint.text = "x " + coinPoint.ToString();
        }

        if (collision.gameObject.tag == "Star Tags")
        {
            starPoint++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Heart Tags")
        {
            if (healthPoint <= 70)
            {
                healthPoint += 30;
                Destroy(collision.gameObject);
            }
            else
            {
                if (healthPoint != 100)
                {
                    healthPoint += (100 - healthPoint);
                    Destroy(collision.gameObject);
                }
            }
        }

        if (collision.gameObject.tag == "Key Tags")
        {
            haveKey = true;
            keyPoint++;
            Destroy(collision.gameObject);
            textKeyPoint.text = "x " + keyPoint.ToString();
        }

        if (collision.gameObject.tag == "Chest Tags")
        {
            if (haveKey == true)
            {
                starPoint++;
                Destroy(collision.gameObject);
                GameObject.Find("/Object/Chest_Open").GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        InteractSnake(collision);

        if (collision.gameObject.tag == "MaxBottom")
        {
            Dying();
        }
    }

    void InteractSnake(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snake Tags")
        {
            GameObject snakeGO = collision.gameObject;
            if (animatorGO.GetBool("isAttacking") == true)
            {
                Snake_Scripts snakeScript = snakeGO.GetComponent<Snake_Scripts>();
                snakeScript.isDead = true;
                StartCoroutine(DestroyGob(snakeGO));
            }
            else
            {
                healthPoint -= 30;
                if (coinPoint > 0)
                {
                    coinPoint--;
                    textCoinPoint.text = "x " + coinPoint.ToString();
                }

                if (healthPoint <= 0)
                {
                    Dying();
                }
                else
                {
                    transform.position = new Vector3(-9, -2.545f, -5);
                }
            }
        }
    }

    IEnumerator DestroyGob(GameObject gOb)
    {
        yield return new WaitForSeconds(3);
        Destroy(gOb);
        enemyPoint++;
    }

    IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(1);
        levelComplete.SetActive(true);
    }

    void Dying()
    {
        animatorGO.SetBool("isBeingIdle", false);
        animatorGO.SetBool("isWalking", false);
        animatorGO.SetBool("isRunning", false);
        animatorGO.SetBool("isJumping", false);
        animatorGO.SetBool("isAttacking", false);
        animatorGO.SetBool("isDying", true);
        rectTransformHp.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Right,
            464.5f - healthPoint,
            healthPoint
        );
        isGameOver = true;
        StartCoroutine(AfterDeath());
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Walking(0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Walking(1);
        }

        if (
            Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.RightShift)
            || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)
        )
        {
            Running(0);
        }

        if (
            Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightShift)
            || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)
        )
        {
            Running(1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jumping();
        }

        if (Input.GetKey(KeyCode.J))
        {
            Attacking();
        }

        BeingIdle();
    }

    void BeingIdle()
    {
        waitTime -= Time.deltaTime;
        if (waitTime < 0.0F)
        {
            animatorGO.SetBool("isBeingIdle", true);
            animatorGO.SetBool("isWalking", false);
            animatorGO.SetBool("isRunning", false);
            animatorGO.SetBool("isJumping", false);
            animatorGO.SetBool("isAttacking", false);
        }
    }

    void Walking(int turn)
    {
        animatorGO.SetBool("isBeingIdle", false);
        animatorGO.SetBool("isWalking", true);
        animatorGO.SetBool("isRunning", false);
        animatorGO.SetBool("isJumping", false);
        animatorGO.SetBool("isAttacking", false);
        waitTime = 0.5F;

        if (turn == 0)
        {
            gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
        if (turn == 1)
        {
            gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);
            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
    }

    void Running(int turn)
    {
        animatorGO.SetBool("isBeingIdle", false);
        animatorGO.SetBool("isWalking", false);
        animatorGO.SetBool("isRunning", true);
        animatorGO.SetBool("isJumping", false);
        animatorGO.SetBool("isAttacking", false);
        waitTime = 0.5F;

        if (turn == 0)
        {
            gameObject.transform.Translate(Vector2.right * (speedX + 1) * Time.deltaTime);
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
        if (turn == 1)
        {
            gameObject.transform.Translate(Vector2.left * (speedX + 1) * Time.deltaTime);
            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
    }

    void Jumping()
    {
        animatorGO.SetBool("isBeingIdle", false);
        animatorGO.SetBool("isWalking", false);
        animatorGO.SetBool("isRunning", false);
        animatorGO.SetBool("isJumping", true);
        animatorGO.SetBool("isAttacking", false);
        waitTime = 0.5F;

        gameObject.transform.Translate(Vector2.up * 7 * Time.deltaTime);
    }

    void Attacking()
    {
        animatorGO.SetBool("isBeingIdle", false);
        animatorGO.SetBool("isWalking", false);
        animatorGO.SetBool("isRunning", false);
        animatorGO.SetBool("isJumping", false);
        animatorGO.SetBool("isAttacking", true);
        waitTime = 0.5F;
    }
}
