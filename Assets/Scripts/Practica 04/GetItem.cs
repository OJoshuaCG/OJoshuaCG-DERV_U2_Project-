using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{   
    public GameObject [] items;

    public GameObject area;
    SpawnArea spawn;

    void Start() {
        spawn = area.GetComponent<SpawnArea>();               
    }


    private void OnCollisionEnter(Collision other){        
        string tag = other.gameObject.tag;
        string nombre = other.gameObject.name;
        GameObject obj = GameObject.Find(nombre);

        if(tag.Equals("Item")){
            Destroy(obj);
            EnabledItem(nombre);
        }
        else if(tag.Equals("Enemy")){
            // idk
        }
    }

    private void EnabledItem(string nombre){
        for(int i = 0; i < items.Length; i++){
            
            if(items[i].name.Equals(nombre)){
                items[i].SetActive(true);
                StartSpawn(i);
            }
            
            else{
                GameObject obj = GameObject.Find(items[i].name);
                obj.SetActive(false);
            }
            
        }
    }

    private void StartSpawn(int numberSprite){
        spawn.sprite = numberSprite;
        spawn.isEnabled = true;
    }
}
