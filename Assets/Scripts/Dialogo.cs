using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Dialogo_", menuName = "Crear Dialogo", order = 1)]

public class Dialogo : ScriptableObject
{
    [System.Serializable]
    public struct Mensaje
    {
        public int ID;

        [TextArea(10, 10)]

        public string dialogos;
    }

    public Mensaje[] mensaje;

    public Mensaje getDatosMensaje(int i)
    {
        return mensaje[i];
    }

    public int getCantidadMensajes()
    {
        return mensaje.Length;
    }
}
