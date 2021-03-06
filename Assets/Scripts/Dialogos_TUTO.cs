using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogos_TUTO : MonoBehaviour
{
    [SerializeField]
    Dialogo dialogo;

    public int indexActual;

    [SerializeField]
    public TextMeshProUGUI texto;

    bool inicio = false;

    bool cambioEscena = false;

    // Start is called before the first frame update
    void Start()
    {
        indexActual = -1;

        texto.text = "Bienvenido al tutorial. Presiona X para continuar";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (indexActual == -1)
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

        if (indexActual == dialogo.getCantidadMensajes() - 1)
        {
            cambioEscena = true;
        }

        if (Input.GetKeyDown(KeyCode.C)) // Adelante
        {

            if (indexActual < dialogo.getCantidadMensajes() - 1)
            {
                indexActual++;
                texto.text = dialogo.getDatosMensaje(indexActual).dialogos;

                texto.richText = true;
                texto.maxVisibleCharacters = 0;
                StopAllCoroutines();
                StartCoroutine("mostrarTexto");
            }

        }

        if (Input.GetKeyDown(KeyCode.C) && cambioEscena == true)
        {
            SceneManager.LoadScene(1);
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

            Debug.Log("Ejecuci?n Corrutina");
            yield return new WaitForSeconds(0.05f);

            if (texto.maxVisibleCharacters == largo)
            {
                estado = false;
            }

        }

        Debug.Log("Corrutina detenida");
        StopCoroutine("mostrarTexto");
    }
}