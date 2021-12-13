using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    public static PlayerController playCont;

    [SerializeField]
    TextMeshProUGUI textoCanvas;
    [SerializeField]
    GameObject camReference;
    [SerializeField]
    GameObject canvasDialogo;
    [SerializeField]
    GameObject objDirectionRC;


    bool canThrow = false;
    bool canTalk = false;
    bool canDrop = false;
    bool canMove = true;
    
    bool missionFinished = false;
    // 0 = ninguna mision   1 = tomar hongos    2 = ir a la cueva
    [SerializeField]
    short mission = 0;
    short contMush = 0;
    
    void Awake(){
        if(PlayerController.playCont == null){
            PlayerController.playCont = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayCast();
    }
   
    
    float camX, camY, camZ;
    float distancia = 4.0f;

    private void rayCast(){
        camX = camReference.transform.position.x;
        camY = camReference.transform.position.y;
        camZ = camReference.transform.position.z;
        Vector3 origen = camReference.transform.position;
        Vector3 direccion = objDirectionRC.transform.forward;
        float duracion = 0.1f;
        RaycastHit hit;
        //Se emite el rayo y se comprueba la colision
        if (Physics.Raycast(origen, direccion, out hit, distancia))
        {
            Debug.Log(hit.collider.tag);
            string tag = hit.collider.tag;
            string name = hit.collider.gameObject.name;
            if (hit.collider.tag.Equals("talkeable")){
                textoCanvas.text = "Click derecho para interactuar";
                canTalk = true;
                if (Input.GetMouseButtonDown(0) && canTalk)    // Click Izquierdo
                {    
                    canTalk = false;         
                    // Buscar el objeto con quien esta hablando
                    GameObject talkObject = GameObject.Find(name);
                    
                    Vector3 vector = new Vector3(talkObject.transform.position.x, 
                                                talkObject.transform.position.y + 2, 
                                                talkObject.transform.position.z);
                    
                    canvasDialogo.SetActive(true);
                    
                    // Mover canvas dialogo arriba del personaje con quien esta hablando
                    canvasDialogo.transform.position = vector;
                    canvasDialogo.transform.rotation = talkObject.transform.rotation;                    
                    
                }
            }
            else if (hit.collider.tag.Equals("mission1") && mission == 1 && !missionFinished){
                textoCanvas.text = "Click derecho para tomar";                
                if (Input.GetMouseButtonDown(0))    // Click Izquierdo
                {                    
                    // Buscar el objeto que tomo
                    GameObject dropObject = GameObject.Find(name);                   
                    Destroy(dropObject);
                    contMush++;
                    if(contMush == 15){
                        missionFinished = true;
                    }
                }
            }
            else{
                if(!canTalk || !canDrop){
                    textoCanvas.text = "";
                }
                
            }
        }            
    }


    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;
        if(name.Equals("Mine Cave") && mission == 2 && !missionFinished){
            SceneManager.LoadScene(2);
            Vector3 vector = new Vector3(0, 2, -9);
            this.transform.position = vector;
        }
    }
}
