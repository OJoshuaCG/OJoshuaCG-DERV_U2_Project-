using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool guizmoShowed;
    public bool rayShowed;
    public GameObject reference;
    GameObject temp;
    float camX,camY,camZ;

    public Text instruccion;
    
    bool taking = false;

    void Start()
    {
        instruccion.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        camX = reference.transform.position.x;
        camY = reference.transform.position.y;
        camZ = reference.transform.position.z;

        float distancia = 2.0f;
        Vector3 origen = reference.transform.position;
        Vector3 direccion = transform.forward;
        float duracion = 0.1f;
 
        RaycastHit hit;
        //Se emite el rayo y se comprueba la colision
        if(Physics.Raycast(origen, direccion ,out hit, distancia)) {
            Debug.Log(hit.collider.tag);
            if(hit.collider.tag.Equals("spawn")) {
                Debug.DrawRay(reference.transform.position,transform.forward * distancia,Color.green,duracion);

                string nombre = hit.collider.gameObject.name;
                temp = GameObject.Find(nombre);
                //Destroy(temp);
                if(!taking)instruccion.text = "Left-Click to take";

                if (Input.GetMouseButtonDown(0)) {
                    Debug.Log("Click Izquierdo");
                    instruccion.text = "Left-click to drop";
                    taking = true;                    
                } 

                
        
            }
            
        } else {
            
            Debug.DrawRay(reference.transform.position,transform.forward * distancia,Color.red,duracion);
            
            if(taking){
                if (Input.GetMouseButtonDown(0)) taking = false;
                instruccion.text = "Left-click to drop";
            } else {
                instruccion.text = "";        
            }
            
        }

        if(temp!=null && taking) {
            Vector3 pos = reference.transform.position;
            temp.transform.LookAt(pos);
       } else taking = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position, new Vector3(area_Size,0.1f,area_Size));
         
        camX = reference.transform.position.x;
        camY = reference.transform.position.y;
        camZ = reference.transform.position.z;

        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.
       
        if(guizmoShowed) {

            Gizmos.DrawCube(
            new Vector3(camX,camY,camZ+1),
            new Vector3(0.1f,0.1f,2));
        }

        if(rayShowed){
            Gizmos.DrawRay(reference.transform.position, transform.forward * 2.0f);
        }
        

        //Gizmos.DrawMesh(spawnerMesh, transform.position, Quaternion.identity,Vector3.one); // posicion, rotacion,escala

        //Gizmos.DrawWireSphere(transform.position,1);

        //Gizmos.DrawIcon(transform.position,"spawn.png");
    }

}
