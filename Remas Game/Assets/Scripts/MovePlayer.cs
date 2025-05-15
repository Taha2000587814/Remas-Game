using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public bool isMove = true;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        Move(); 
    }
    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        transform.position += Vector3.right *inputX* moveSpeed * Time.deltaTime;
        float xpos=Mathf.Clamp(transform.position.x, -maxPos, maxPos);
        transform.position = new Vector3(xpos, transform.position.y, transform.position.z);
    }
}
