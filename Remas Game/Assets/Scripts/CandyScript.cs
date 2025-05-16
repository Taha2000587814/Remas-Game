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

        float candySpeed = GameManager.instance.GetCandySpeed();
        Debug.Log($"Candy spawned with speed: {candySpeed}");

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -candySpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "catcher")
        {
            GameManager.instance.scr.PlayOneShot(GameManager.instance.clip);
            Destroy(gameObject);

            // Check if candy is rotten
            if (gameObject.tag == "Rotten")
            {
                GameManager.instance.InvalidCandyCaught(); // Reduce lives only when caught
            }
            else
            {
                GameManager.instance.incrementScores(); // Increase score for normal candy
            }
        }
        else if (collision.gameObject.tag == "boundry")
        {
            // **Check if it's a rotten candy before reducing lives**
            if (gameObject.tag != "Rotten")
            {
                GameManager.instance.decreseLives(); // Reduce lives only for normal candies
            }

            Destroy(gameObject);
        }
    }

}
