using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CandyScript : MonoBehaviour
{

    public bool isInvalidCandy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
