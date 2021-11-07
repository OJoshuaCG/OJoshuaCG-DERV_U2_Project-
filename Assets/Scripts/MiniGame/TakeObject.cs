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
    float camX, camY, camZ;

    float distancia = 3.0f;
    bool taking = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camX = reference.transform.position.x;
        camY = reference.transform.position.y;
        camZ = reference.transform.position.z;


        Vector3 origen = reference.transform.position;
        Vector3 direccion = transform.forward;
        float duracion = 0.1f;

        RaycastHit hit;
        //Se emite el rayo y se comprueba la colision
        if (Physics.Raycast(origen, direccion, out hit, distancia))
        {

            Debug.Log(hit.collider.tag);
            if (hit.collider.tag.Equals("talkeable"))
            {
                Debug.DrawRay(reference.transform.position, transform.forward * distancia, Color.green, duracion);

                //string nombre = hit.collider.gameObject.name;

                if (Input.GetMouseButtonDown(0))
                {
                    //Click Izquierdo para interactuar
                    GameObject canvasDialog = transform.GetChild(0).gameObject;
                    canvasDialog = canvasDialog.transform.GetChild(1).gameObject;
                    canvasDialog.SetActive(true);
                    Debug.Log(canvasDialog.name);
                }

            }

        }
        else
        {

            Debug.DrawRay(reference.transform.position, transform.forward * distancia, Color.red, duracion);

            /*
            if (taking)
            {
                
            }
            else
            {
                instruccion.text = "";
            }
            */
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position, new Vector3(area_Size,0.1f,area_Size));

        camX = reference.transform.position.x;
        camY = reference.transform.position.y;
        camZ = reference.transform.position.z;

        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.

        if (guizmoShowed)
        {

            Gizmos.DrawCube(
            new Vector3(camX, camY, camZ + 1),
            new Vector3(0.1f, 0.1f, 2));
        }

        if (rayShowed)
        {
            Gizmos.DrawRay(reference.transform.position, transform.forward * distancia);
        }


        //Gizmos.DrawMesh(spawnerMesh, transform.position, Quaternion.identity,Vector3.one); // posicion, rotacion,escala

        //Gizmos.DrawWireSphere(transform.position,1);

        //Gizmos.DrawIcon(transform.position,"spawn.png");
    }

}
