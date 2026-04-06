using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Scripts : MonoBehaviour
{
    public GameObject magePlayer;
    BoxCollider2D snakeCd;
    private Animator animatorGO;
    private Animator animatorMagePlayer;

    public bool checkPos,
        isDead = false;
    float waitTime = 0f;
    float speedX = 1f;
    float delayAttack,
        waitAttack;

    void Start()
    {
        snakeCd = gameObject.GetComponent<BoxCollider2D>();
        animatorGO = GetComponent<Animator>();
        animatorMagePlayer = magePlayer.GetComponent<Animator>();

        animatorGO.SetBool("isWalk", true);
        animatorGO.SetBool("isAttack", false);
        animatorGO.SetBool("isDeath", false);
    }

    void Update()
    {
        if (isDead == false)
        {
            SnakeMovement();
            waitTime -= Time.deltaTime;
            if (waitTime < 0.0F)
            {
                snakeCd.isTrigger = false;
            }
        }
        else
        {
            animatorGO.SetBool("isWalk", false);
            animatorGO.SetBool("isAttack", false);
            animatorGO.SetBool("isDeath", true);
            snakeCd.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GOControlLeft")
        {
            checkPos = true;
        }

        if (collision.gameObject.tag == "GOControlRight")
        {
            checkPos = false;
        }

        if (collision.gameObject.tag == "Snake Tags")
        {
            snakeCd.isTrigger = true;
            waitTime = 0.6f;
        }

        if (collision.gameObject.tag == "Key Tags")
        {
            snakeCd.isTrigger = true;
            waitTime = 0.5f;
        }
    }

    private void SnakeMovement()
    {
        float k = 0;
        if (magePlayer.transform.position.x <= transform.position.x)
        {
            k = transform.position.x - magePlayer.transform.position.x;
        }
        else
        {
            k = magePlayer.transform.position.x - transform.position.x;
        }

        if (k <= 1.1f)
        {
            animatorGO.SetBool("isWalk", false);
            animatorGO.SetBool("isAttack", true);
            animatorGO.SetBool("isDeath", false);
            Walking();
        }
        else
        {
            animatorGO.SetBool("isWalk", true);
            animatorGO.SetBool("isAttack", false);
            animatorGO.SetBool("isDeath", false);
            Walking();
        }
    }

    private void Walking()
    {
        if (checkPos)
        {
            gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);
            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
        else
        {
            gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );
            }
        }
    }
}
