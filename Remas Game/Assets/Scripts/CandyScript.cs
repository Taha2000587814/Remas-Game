using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CandyScript : MonoBehaviour
{

    public bool isInvalidCandy = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Apply the single candy speed value
        float candySpeed = GameManager.instance.GetCandySpeed();
        rb.velocity = new Vector2(0, -candySpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "catcher")
        {
            GameManager.instance.scr.PlayOneShot(GameManager.instance.clip);
            Destroy(gameObject);
            GameManager.instance.incrementScores();
        }
        else if (collision.gameObject.tag == "boundry")
        {
            Destroy(gameObject);
            GameManager.instance.decreseLives();
        }
    }



}
