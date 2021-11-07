using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackPlayer : MonoBehaviour
{   
    GameObject player;
    float speed = 7;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        Vector3 origen = transform.position;
        Vector3 destino = player.transform.position;

        //destino.z -= 3.0f;       

        transform.LookAt(destino);
        transform.position = Vector3. MoveTowards(origen, destino, speed * Time.deltaTime);
        
        //transform.position = Vector3.Lerp(origen, destino, speed * Time.deltaTime);

        /*
        Vector3 currentVelocity = new Vector3(0f, 0f, 0f);
        transform.position = Vector3.SmoothDamp(origen, destino, ref currentVelocity, speed * Time.deltaTime);
        */

        //float distancia = Vector3.Distance(origen, destino);
    }

    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;
        string tag = other.gameObject.tag;

        if(tag.Equals("Player")){
            //GameObject obj = this.gameObject;
            GameObject obj = GameObject.Find(this.gameObject.name);
            Destroy(obj);
        }
    }

}
