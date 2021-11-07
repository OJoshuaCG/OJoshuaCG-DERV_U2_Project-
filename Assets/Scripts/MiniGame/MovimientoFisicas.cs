using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoFisicas : MonoBehaviour
{
    public float desplazamiento = 10;
    Rigidbody rb;

    public float speedH;
    public float speedV;
    float yaw;
    float pithc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // T = Tipo de dato parametrizado
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per time. All devices exec in same time.
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(rb.position + transform.forward * desplazamiento * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(rb.position + transform.right * -1f * desplazamiento * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(rb.position + transform.forward * -1f * desplazamiento * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(rb.position + transform.right * desplazamiento * Time.deltaTime);
        }

        yaw += speedH * Input.GetAxis("Mouse X");
        pithc -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pithc, yaw, 0.0f);
    }
}

/*
Temas:
-Debug
-Detección de teclas (input)
*/
