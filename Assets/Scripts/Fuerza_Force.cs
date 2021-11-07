using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuerza_Force : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float speedFuerza = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rb.AddForce(new Vector3(0f, 0f, 0f), ForceMode.Force); 
        //rb.AddForce(Vector3.right * 20f, ForceMode.Force);  //grobales
        rb.AddForce(transform.right * speedFuerza, ForceMode.Force);  //local
    }
}
