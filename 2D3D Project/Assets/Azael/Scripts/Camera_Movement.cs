using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    //Camara que sigue al jugador dependiendo de la prespectiva 3D o 2D

    Transform playerPos; //Referencia a Transform del jugador 
    public float range2DmovementX; //Rango de tolerancia del movimiento de la camara en 2D
    float range2DmovementY = -1.4f;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //Obtener Transform del jugador
    }

    void LateUpdate()
    {
        //Seguimiento de la cámara al jugador
        if (!Camera_Transition.ortho) //Si la cámara esta en modo perspectiva seguir al personaje hacia adelante y atras
        {
            transform.position = new Vector3(playerPos.position.x, playerPos.position.y + range2DmovementY, playerPos.position.z);
        }
        else //Si la cámara esta en modo ortografico cambiar a seguimiento lateral con un rango de tolerancia
        {
            if (playerPos.position.z > transform.position.z + range2DmovementX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z - range2DmovementX);
            }
            else if(playerPos.position.z < transform.position.z - range2DmovementX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z + range2DmovementX);
            }
            if (playerPos.position.z > transform.position.y + range2DmovementY)
            {
                transform.position = new Vector3(transform.position.x, playerPos.position.y + range2DmovementY, transform.position.z);
            }
            else if (playerPos.position.z < transform.position.y - range2DmovementY)
            {
                transform.position = new Vector3(transform.position.x, playerPos.position.y + range2DmovementY, transform.position.z);
            }
        }
    }
}
