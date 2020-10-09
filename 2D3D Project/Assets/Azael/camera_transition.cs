using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Transition : MonoBehaviour
{
    //Mecánica de girar la camara para cambiar de perspectiva, con un contador que cuando termina regresa automaticamente de 2D a 3D y pausa el tiempo mientras se ejecuta la transicion

    public static bool ortho = false; //Booleano para saber si la camara esta en modo ortografico
    bool perspective = true; //Booleano para saber si la cámara esta en 3D o 2D
    bool InTransition = false; //Booleano para saber si la cámara esta en transición
    Camera cam; //Referencia a la cámara
    GameObject pos3DCamera; //Gameobject que esta en la posicion de la camara en 3D
    GameObject pos2DCamera; //Gameobject que esta en la posicion de la camara en 2D
    Rigidbody player;

    public float transitionTime; //Tiempo que tarda en transicion
    public float returnTimer; //Tiempo en regresar de 2D a 3D

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //Obtener el componente cámara
        pos3DCamera = GameObject.FindGameObjectWithTag("3DCamPosition");
        pos2DCamera = GameObject.FindGameObjectWithTag("2DCamPosition");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Activar cambio si no esta en transición----------------------
        if (Input.GetKeyDown(KeyCode.R) && !InTransition)
        {
            Transition();
        }
        //-------------------------------------------------------------

        if (InTransition)
        {
            player.isKinematic = true;
            player.gameObject.GetComponent<Movement>().movementSpeed = 0;
        }
        else
        {
            player.isKinematic = false;
            player.gameObject.GetComponent<Movement>().movementSpeed = 7.8f;
        }
    }

    //Función para el movimiento y rotación de la cámara
    public void Transition()
    {
        if (perspective) //Cambiar a "2D"
        {
            InTransition = true;
            perspective = false;
            LeanTween.rotate(gameObject, new Vector3(0.0f, -90.0f, 0.0f), transitionTime).setOnComplete(ChangeBool);
            LeanTween.move(cam.gameObject, pos2DCamera.transform.position, transitionTime);
        }
        else //Cambiar a "3D"
        {
            ortho = false;
            InTransition = true;
            perspective = true;
            cam.orthographic = false;
            StopAllCoroutines();
            LeanTween.rotate(gameObject, new Vector3(0.0f, 0.0f, 0.0f), transitionTime).setOnComplete(ChangeBool);
            LeanTween.move(cam.gameObject, pos3DCamera.transform.position, transitionTime);
        }
    }

    //Cambiar booleano de transición al terminar el movimiento y poder volver a activarlo
    void ChangeBool()
    {
        InTransition = false;
        if (!perspective)
        {
            cam.orthographic = true; //Cambiar cámara a proyección ortografica
            ortho = true;
            StartCoroutine(Counter());
        }
    }

    //Contador para cambiar de 2D a 3D despues de un tiempo determinado (t)
    IEnumerator Counter()
    {
        yield return new WaitForSeconds(returnTimer);
        Transition();
    }
}
