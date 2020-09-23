using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    Transform playerPos; //Referencia a Transform del jugador 
    public float range2Dmovement; //Rango de tolerancia del movimiento de la camara en 2D

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //Obtener Transform del jugador
    }

    void LateUpdate()
    {
        //Seguimiento de la cámara al jugador
        if (!Camera_Transition.ortho) //Si la cámara esta en modo perspectiva seguir al personaje hacia adelante y atras
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z);
        }
        else //Si la cámara esta en modo ortografico cambiar a seguimiento lateral con un rango de tolerancia
        {
            if (playerPos.position.z > transform.position.z + range2Dmovement)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z - range2Dmovement);
            }
            else if(playerPos.position.z < transform.position.z - range2Dmovement)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z + range2Dmovement);
            }
        }
    }
}
