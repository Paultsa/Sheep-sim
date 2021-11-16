using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    
    void Start()
    {
        
    }

    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        transform.Translate(xMovement * moveSpeed * Time.deltaTime, 0, zMovement * moveSpeed * Time.deltaTime);

        float xMouse = Input.GetAxis("Mouse X");
        Vector3 lookAt = new Vector3(0, xMouse * rotateSpeed, 0);
        transform.Rotate(lookAt);
    }
}
