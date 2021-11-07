using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawn_Area : MonoBehaviour
{   
    
    //public TextMeshProUGUI txt_puntaje;
    public TextMeshProUGUI txt_tiempo;
    public GameObject objLanzar;
    public Material[] materials;

    System.Random rnd = new System.Random();
    
    float x, y, z;
    
    public static float area_Size = 10;
    public Mesh spawnerMesh;
    public bool solidGuizmo = false;

    //float inf = -5.0f, sup = 5.0f;
    static float inf = area_Size/2*-1;
    static float sup = area_Size/2;
    int i = 0, tiempo = 60, puntaje = 0;

    

    void Start()
    {
        y = transform.position.y;
        StartCoroutine("corrutinaSpawn");
        StartCoroutine("corrutinaTiempo");
    }


    IEnumerator corrutinaSpawn(){
        while(true){
            x = GetRndFloat();
            z = GetRndFloat();            
            
            GameObject obj = Instantiate(objLanzar,         // Se establece el objeto
                            new Vector3(transform.position.x+x, y, transform.position.z+z),           // La posicion (x & z random)
                            new Quaternion(0f, 0f, 0f, 1))  // La rotacion
                            as GameObject;                  // Se transforma en un GameObject
            obj.name = "object_" + i;
            obj.tag = "spawn";
            
            // Se establece el Material (color)
            int aux = rnd.Next(0, 4);
            Renderer rend = obj.GetComponent<Renderer>();
            rend.material = materials[aux];

            i++;
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator corrutinaTiempo(){
        while(tiempo > 0){
            tiempo--;
            txt_tiempo.text = tiempo.ToString(); 
            
            yield return new WaitForSeconds(1.0f);        
        }
        StopCoroutine("corrutinaSpawn");
        StopCoroutine("corrutinaTiempo");        
    }

    private float GetRndFloat(){
        double aux = rnd.NextDouble()*(inf-(sup))+(sup);
        return((float)aux);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        
        
        if(solidGuizmo) {
            Gizmos.DrawCube(transform.position, new Vector3(area_Size,0.1f,area_Size));
            Gizmos.DrawSphere(transform.position,1);
        }
        else {
            Gizmos.DrawWireCube(transform.position, new Vector3(area_Size,0.5f,area_Size));

            Gizmos.DrawMesh(spawnerMesh, transform.position, Quaternion.identity,Vector3.one); // posicion, rotacion,escala

            Gizmos.DrawWireSphere(transform.position,1);
        }


        //Gizmos.DrawIcon(transform.position,"spawn.png");
    }
    /*
    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Player")){
            x = GetRndFloat();
            z = GetRndFloat();            
            
            GameObject obj = Instantiate(objLanzar,         // Se establece el objeto
                            new Vector3(x, y, z),           // La posicion (x & z random)
                            new Quaternion(0f, 0f, 0f, 1))  // La rotacion
                            as GameObject;                  // Se transforma en un GameObject
            obj.name = "object_" + i;

            // Elegimos el material y definimos el tag
            int aux = rnd.Next(0, 4);
            
            // Se establece el Tag
            //obj.tag = materials[aux].ToString();
            //Debug.Log(materials[aux].ToString());

            //Debug.Log("El tag es: " + obj.tag.ToString());

            // Se establece el Material (color)
            Renderer rend = obj.GetComponent<Renderer>();
            rend.material = materials[aux];

            i++;
        }
    }
    */


    

    /*
    public void UpdatePoints(){
        puntaje++;
        txt_puntaje.text = puntaje.ToString();
    }
    */
    
}
