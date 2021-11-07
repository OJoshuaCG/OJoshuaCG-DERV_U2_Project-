using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBullet : MonoBehaviour
{
    Rigidbody rb;
    public float speedBullet = 20;

    void Start(){
        rb = GetComponent<Rigidbody>();   
    }

    void Update(){
        rb.AddForce(transform.forward * speedBullet, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;
        string tag = other.gameObject.tag;

        if(tag.Equals("Enemy")){
            GameObject enemy = GameObject.Find(name);
            Destroy(enemy);
            GameObject bullet = GameObject.Find(this.gameObject.name);
            Destroy(bullet);
        }
    }
}
