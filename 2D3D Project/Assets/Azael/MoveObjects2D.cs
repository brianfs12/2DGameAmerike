using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects2D : MonoBehaviour
{
    Vector3 originalPosition;//Posicion de objetos estaticos
    Vector3 playerPosition;//Posicion del jugador
    float posX;

    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
        {
            playerPosition = gameObject.transform.position; //Guardar la posicion actual del jugador
        }

        if (Camera_Transition.ortho)
        {
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                transform.position = new Vector3(4.0f, playerPosition.y, playerPosition.z); //Mover al jugador
            }
            else
            {
                transform.position = new Vector3(4.0f, originalPosition.y, originalPosition.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!Camera_Transition.ortho)
            {
                posX = transform.position.x;
            }
            else
            {
                if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
                {
                    transform.position = new Vector3(posX, playerPosition.y, playerPosition.z); //Mover al jugador
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
    }
}
