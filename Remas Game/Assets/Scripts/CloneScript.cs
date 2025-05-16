using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    public static CloneScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject[] candies;
    float maxX=8;
    [SerializeField] float spawnInterval;
    void Start()
    {
        startSpawn();
    }

 

    void spawnCandy()
    {
        int rand = Random.Range(0, candies.Length);
        float randx = Random.Range(-maxX, maxX);
        Vector3 randPos = new Vector3(randx, transform.position.y, transform.position.z);

        GameObject spawnedCandy = Instantiate(candies[rand], randPos, transform.rotation);

        // Apply the single candy speed value
        float candySpeed = GameManager.instance.GetCandySpeed();
        spawnedCandy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -candySpeed);
    }

    IEnumerator spawnCandies()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            spawnCandy();
            yield return new WaitForSeconds(spawnInterval);
        }
    } 

    public void startSpawn()
    {
        StartCoroutine("spawnCandies");
    }

    public void stopSpawn()
    {
        StopCoroutine("spawnCandies");
    }



}
