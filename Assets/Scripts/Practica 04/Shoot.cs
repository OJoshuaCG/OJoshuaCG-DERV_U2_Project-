using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{   
    public GameObject bullet;
    public GameObject spawner;

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            GameObject bull = Instantiate(bullet, spawner.transform.position, spawner.transform.rotation);

            Destroy(bull, 3);
        }       
    }
}
