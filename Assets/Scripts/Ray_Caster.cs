using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGame : MonoBehaviour
{
    
    public bool guizmoShowed;
    public bool rayShowed;
    public GameObject reference;
    GameObject temp;
    float camX, camY, camZ;

    float distancia = 3.0f;
    bool taking = false;

    [SerializeField]
    GameObject canvasDialog;
    void Start()
    {
        //canvasDialog = GameObject.Find("CanvasDialogos"); //.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        rayCast();        
    }

    private void rayCast(){
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
                    // Buscar el objeto con quien esta hablando
                    GameObject talkObject = GameObject.Find(hit.collider.name);
                    

                    Vector3 vector = new Vector3(talkObject.transform.position.x, 
                                                talkObject.transform.position.y + 2, 
                                                talkObject.transform.position.z);
                    Quaternion quaternion = new Quaternion(0f, talkObject.transform.rotation.y, 0f, 1f);
                    
                    canvasDialog.SetActive(true); // No funciona D:
                    
                    // Mover canvas dialogo arriba del personaje con quien esta hablando
                    canvasDialog.transform.position = vector;
                    canvasDialog.transform.rotation = quaternion;                    
                }
            }
        }        
    }
    
    
}
