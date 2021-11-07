using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{   
    public GameObject [] objSpawn;
    public bool isEnabled = false;
    public int sprite = 0;

    int cantidadSpawns = 35;

    float x, y, z;
    public static float area_Size = 15;
    //float inf = -5.0f, sup = 5.0f;
    static float inf = area_Size/2*-1;
    static float sup = area_Size/2;
    
    private void Start() {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update(){
        if(isEnabled){
            StartCoroutine("corrutinaSpawn");
            isEnabled = false;
        }        
    }

    int i = 0;
    System.Random rnd = new System.Random();
    IEnumerator corrutinaSpawn(){
        while(i < cantidadSpawns){
            x = GetRndFloat();
            z = GetRndFloat();            
            
            GameObject obj = Instantiate(objSpawn[sprite],         // Se establece el objeto
                            new Vector3(transform.position.x+x, y, transform.position.z+z), // La posicion (x & z random)
                            new Quaternion(0f, 0f, 0f, 1))  // La rotacion
                            as GameObject;                  // Se transforma en un GameObject
            obj.name = "object_" + i;
            obj.tag = "Enemy";

            i++;
            yield return new WaitForSeconds(2f);
        }        
    }

    private float GetRndFloat(){
        double aux = rnd.NextDouble()*(inf-(sup))+(sup);
        return((float)aux);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;        
        
        Gizmos.DrawCube(transform.position, new Vector3(area_Size,0.1f,area_Size));    
    }
}
