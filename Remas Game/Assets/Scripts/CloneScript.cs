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
    // Start is called before the first frame update
    void Start()
    {
        startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnCandy()
    {
        int rand = Random.Range(0, candies.Length);
        float randx = Random.Range(-maxX, maxX);
        Vector3 randPos = new Vector3(randx,transform.position.y,transform.position.z);
        Instantiate(candies[rand], randPos, transform.rotation);
       
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
