using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCave : MonoBehaviour
{      
    [SerializeField]
    GameObject spawner;
    [SerializeField]
    GameObject objToSpawn;
    [SerializeField]
    GameObject exit;

    float xArea = 12.5f;
    float zArea = 2.5f;
    float x, z, y = 0f;
    bool spawn = false;
    bool follow = false;

    System.Random rnd = new System.Random();

    void Update()
    {
        if(follow){
            followPlayer();
        }        
    }


    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("Player") && !spawn){
            StartCoroutine("corrutinaSpawn");
            
            spawn = true;
        }
    }


    IEnumerator corrutinaSpawn(){        
        for(int i = 0; i < 10; i++){
            x = GetRndFloat(xArea);
            z = GetRndFloat(zArea);

            GameObject obj = Instantiate(objToSpawn,
                new Vector3(spawner.transform.position.x+x, 
                    y,
                    spawner.transform.position.z+z),
                new Quaternion(0f, 0f, 0f, 1))
            as GameObject;
            obj.name = "object_" +i;
            obj.tag = "Enemy";

            yield return new WaitForSeconds(2.5f);
        }
        follow = true;
        exit.SetActive(true);
    }

    private void followPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject squirrel = GameObject.FindGameObjectWithTag("mission2");

        Vector3 origen = squirrel.transform.position;
        Vector3 destino = player.transform.position;
        destino.x -= 1;
        destino.y = origen.y;
        destino.z -= 1;

        squirrel.transform.LookAt(destino);
        squirrel.transform.position = Vector3.MoveTowards(origen, destino, 6 * Time.deltaTime);
    }


    private float GetRndFloat(float value){
        double aux = rnd.NextDouble()*((value*2))-value;
        return((float)aux);
    }

}
