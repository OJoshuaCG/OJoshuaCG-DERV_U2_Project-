using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogo_SO : MonoBehaviour
{
    [SerializeField]
    Dialogo dialogo;

    public int indexActual;

    [SerializeField]
    public TextMeshProUGUI texto;

    bool inicio = false;

    // Start is called before the first frame update
    void Start()
    {
        indexActual = -1;

        texto.text = "Estás por empezar el tutorial... Presiona X para continuar";

        int nummensajes = dialogo.getCantidadMensajes();

        Debug.Log(nummensajes);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(indexActual == -1)
            {
                inicio = true;
                indexActual++;
                texto.text = dialogo.getDatosMensaje(indexActual).dialogos;
                texto.richText = true;
                texto.maxVisibleCharacters = 0;
                StopAllCoroutines();
                StartCoroutine("mostrarTexto");
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && inicio == true) // Adelante
        {

            if (indexActual < dialogo.getCantidadMensajes() -1)
            {
                indexActual++;
                texto.text = dialogo.getDatosMensaje(indexActual).dialogos;

                texto.richText = true;
                texto.maxVisibleCharacters = 0;
                StopAllCoroutines();
                StartCoroutine("mostrarTexto");

                
            }

            /*
            if (indexActual == dialogo.getCantidadMensajes() - 1)
            {
                StopAllCoroutines();
                StartCoroutine("TimeBurner");
                //SceneManager.LoadScene("Sample Scene");
                Debug.Log("Cambio de escena...");
            }
            */
        }
    }

    IEnumerator mostrarTexto()
    {

        bool estado;

        estado = true;

        while (estado == true)
        {

            float largo = texto.text.Length;

            if (texto.maxVisibleCharacters < largo)
            {
                texto.maxVisibleCharacters += 1;
            }

            Debug.Log("Ejecución Corrutina");
            yield return new WaitForSeconds(0.05f);

            if(texto.maxVisibleCharacters == largo)
            {
                estado = false;
            }

        }

        Debug.Log("Corrutina detenida");
        StopCoroutine("mostrarTexto");
    }

    IEnumerator TimeBurner()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
