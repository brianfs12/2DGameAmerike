using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_transition : MonoBehaviour
{
    //Mecánica de girar la camara para cambiar de perspectiva

    public static bool perspective = true; //Booleano para saber si la cámara esta en 3D o 2D
    bool InTransition = false; //Booleano para saber si la cámara esta en transición
    Camera cam; //Referencia a la cámara
    public GameObject pos3Dcamera;
    public GameObject pos2DCamera;

    public float transitionTime;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //Activar cambio si no esta en transición-------------------
        if(Input.GetKeyDown(KeyCode.R) && !InTransition)
        {
            Transition();
        }
        //-------------------------------------------------------------
    }

    //Función para el movimiento y rotación de la cámara
    public void Transition()
    {
        if(perspective) //Cambiar a "2D"
        {
            InTransition = true;
            perspective = false;
            LeanTween.rotate(gameObject, new Vector3(0.0f, -90.0f, 0.0f), transitionTime).setOnComplete(ChangeBool);
            LeanTween.move(cam.gameObject, pos2DCamera.transform.position, transitionTime);
        }
        else //Cambiar a "3D"
        {
            cam.transform.SetParent(gameObject.transform);
            InTransition = true;
            perspective = true;
            cam.orthographic = false;
            LeanTween.rotate(gameObject, new Vector3(0.0f, 0.0f, 0.0f), transitionTime).setOnComplete(ChangeBool);
            LeanTween.move(cam.gameObject, pos3Dcamera.transform.position, transitionTime);
        }
    }

    //Cambiar booleano de transición al terminar el movimiento y poder volver a activarlo
    void ChangeBool()
    {
        InTransition = false;
        if(!perspective)
        {
            cam.orthographic = true;
            cam.transform.SetParent(null);
        }
    }
}
