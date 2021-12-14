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

    [SerializeField]
    bool canShoot = false;
    bool canTalk = false;
    bool canDrop = false;
    bool canEnter = false;
    bool canMove = true;
    bool isTalking = false;
    
    [SerializeField]
    bool missionFinished = true;
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

    
    void Start()
    {
        
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
                if (Input.GetMouseButtonDown(0) && canTalk)    // Click Izquierdo
                {    
                    canTalk = false; 
                    isTalking = true;        
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
            else if (tag.Equals("mission1") && mission == 1 && !missionFinished){
                textoCanvas.text = "Click izquierdo para tomar";   
                canDrop = true;             
                if (Input.GetMouseButtonDown(0))    // Click Izquierdo
                {                      
                    canDrop = false;
                    // Buscar el objeto que tomo
                    GameObject dropObject = GameObject.Find(name);                   
                    Destroy(dropObject);
                    contMush++;
                    if(contMush == 2){
                        //missionFinished = true;                        
                        mission++;
                        prepareMission2(true);
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

            else if(tag.Equals("Untagged")) {
                textoCanvas.text = "";
                
                
            }
        }            
    }

    

    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;
        if(name.Equals("Mine Cave") && mission == 2 && !missionFinished && canShoot){
            SceneManager.LoadScene(2);
            Vector3 vector = new Vector3(0, 2, -9);
            this.transform.position = vector;  
            //canShoot = true;
            canEnter = false;
        }
        if(name.Equals("Exit") && missionFinished){
            SceneManager.LoadScene(1);
            Vector3 vector = new Vector3(57, 2, 66);
            Quaternion quaternion = new Quaternion(0, -90, 0, 1);
            canShoot = false;
            this.transform.position = vector;
            this.transform.rotation = quaternion;

        }
    }

    private void prepareMission2(bool state){        
        GameObject dad = GameObject.Find("Squirrel_Dad");
        GameObject son = GameObject.Find("Squirrel_Son");
        dad.SetActive(!state);
        son.SetActive(!state);

        GameObject mom = GameObject.Find("Squirrel_Mom");
        mom.tag = "mission2";

        GameObject dad_cave = GameObject.Find("Squirrel_Dad_Cave").GetComponent<GameObject>();
        GameObject nuts = GameObject.Find("Nuts").GetComponent<GameObject>();
        //dad_cave.enabled = state;
        //nuts.enabled = state;

        dad_cave.SetActive(state);
        nuts.SetActive(state);

        /*
        dad.enabled = !state;
        son.enabled = !state;
        dad_cave.enabled = state;
        nuts.enabled = state;
        */       
    }

    public static bool getStateShoot(){
        return playCont.canShoot;
    }
    
    // public GameObject bullet;
    // public GameObject spawner;

    // // Update is called once per frame
    // void listenKey(){
    //     if(Input.GetKeyDown(KeyCode.F) && canShoot){
    //         GameObject bull = Instantiate(bullet, spawner.transform.position, spawner.transform.rotation);
    //         Destroy(bull, 3);
    //     }       
    // }
    
}
