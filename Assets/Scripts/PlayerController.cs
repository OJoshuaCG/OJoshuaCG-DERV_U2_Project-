using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    public static PlayerController playCont;

    [SerializeField]
    GameObject dialogController;

    [SerializeField]
    TextMeshProUGUI textoCanvas;
    [SerializeField]
    GameObject camReference;
    [SerializeField]
    GameObject canvasDialogo;
    [SerializeField]
    GameObject objDirectionRC;

    [SerializeField]
    bool canShoot = false;
    bool canTalk = false;
    bool canDrop = false;
    bool canEnter = false;
    bool canMove = true;
    bool isTalking = false;
    
    [SerializeField]
    bool missionFinished = false;
    // 0 = ninguna mision   1 = tomar hongos    2 = ir a la cueva
    [SerializeField]
    short mission = 0;
    short contMush = 0;

    GameObject dad, son, dad_cave, nuts;
    
    void Awake(){
        if(PlayerController.playCont == null){
            PlayerController.playCont = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    
    void Start()
    {
        dad = GameObject.Find("Squirrel_Dad");
        son = GameObject.Find("Squirrel_Son");
        dad_cave = GameObject.Find("Squirrel_Dad_Cave");
        nuts = GameObject.Find("Nuts");
        
        dad_cave.SetActive(false);
        nuts.SetActive(false);
    }

    
    void Update()
    {
        rayCast();
    }
       
    float camX, camY, camZ;
    float distancia = 5.0f;

private void rayCast(){
    camX = camReference.transform.position.x;
    camY = camReference.transform.position.y;
    camZ = camReference.transform.position.z;
    Vector3 origen = camReference.transform.position;
    Vector3 direccion = objDirectionRC.transform.forward;        
    RaycastHit hit;
    //Se emite el rayo y se comprueba la colision
    if (Physics.Raycast(origen, direccion, out hit, distancia))
    {
        Debug.Log(hit.collider.tag);
        string tag = hit.collider.tag;
        string name = hit.collider.gameObject.name;

        if (tag.Equals("talkeable")){
            textoCanvas.text = "Click izquierdo para interactuar";
            canTalk = true;

            /*
            if (Input.GetMouseButtonDown(0) && canTalk)    // Click Izquierdo
            {    
                setDialogObj(name);                    
            }*/
        }
        else if (tag.Equals("mission1") && missionFinished==false){
            canTalk = true;
            if(name.Contains("Mushroom") && mission == 1){
                textoCanvas.text = "Click izquierdo para tomar";   
                canDrop = true;             
                if (Input.GetMouseButtonDown(0))    // Click Izquierdo
                {                      
                    canDrop = false;
                    // Buscar el objeto que tomo
                    GameObject dropObject = GameObject.Find(name);                   
                    Destroy(dropObject);
                    contMush++;
                    if(contMush == 5){                            
                        //mission++;
                        missionFinished = true;
                        // Desaparecemos las ardillas 
                        //prepareMission2(true);
                    }
                }
            }
            else{
                textoCanvas.text = "Click izquierdo para Interactuar";
                if (Input.GetMouseButtonDown(0) && canTalk)    // Click Izquierdo
                {
                    setDialogObj(name);                    
                }
            }
        }  

        else if(tag.Equals("mission2") && mission == 2){
            if(name == "Nuts"){
                textoCanvas.text = "Click izquierdo para tomar";   
                if(Input.GetMouseButtonDown(0)){
                    GameObject.Find(name).SetActive(false);
                    canShoot = true;
                }                    
            }
        }

        else if(tag.Equals("Finish")) {
            textoCanvas.text = "";               
        }
        canvasDialogo.transform.LookAt(camReference.transform);
    }            
}

    
    private void setDialogObj(string name){
        canTalk = false; 
        isTalking = true;        
        // Buscar el objeto con quien esta hablando
        GameObject talkObject = GameObject.Find(name);
        
        Vector3 vector = new Vector3(talkObject.transform.position.x, 
                                    talkObject.transform.position.y + 2, 
                                    talkObject.transform.position.z);
        
        canvasDialogo.SetActive(true);
        
        //dialogController.GetComponent<GameObject>().GetComponent<Script>().
        // Mover canvas dialogo arriba del personaje con quien esta hablando
        canvasDialogo.transform.position = vector;
        canvasDialogo.transform.rotation = talkObject.transform.rotation; 
    }


    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;
        if(name.Equals("Mine Cave") && mission == 2 && !missionFinished && canShoot){
            SceneManager.LoadScene(2);
            Vector3 vector = new Vector3(0, 2, -9);
            this.transform.position = vector;              
            canEnter = false;
        }
        if(name.Equals("Exit")){
            canShoot = false;
            missionFinished = true;

            SceneManager.LoadScene(1);            
            Vector3 vector = new Vector3(57, 2, 66);
            Quaternion quaternion = new Quaternion(0, -90, 0, 1);

            this.transform.position = vector;
            this.transform.rotation = quaternion;
            prepareMission2(false);
        }
    }

    private void prepareMission2(bool state){                
        dad.SetActive(!state);
        son.SetActive(!state);
        
        GameObject mom = GameObject.Find("Squirrel_Mom");
        mom.tag = "mission2";        

        dad_cave.SetActive(state);
        nuts.SetActive(state);
    }

    public static bool getStateShoot(){
        return playCont.canShoot;
    }

    public static bool getMissionState() {
        return playCont.missionFinished;
    }    

}
