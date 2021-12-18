using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    short numScene = 0;
    public void IniciarJuego(){
        SceneManager.LoadScene(numScene);
    }
}
