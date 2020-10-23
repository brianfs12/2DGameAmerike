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
        if (Camera_Transition.ortho)
        {
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                transform.position = new Vector3(4.0f, transform.position.y, transform.position.z); //Mover al jugador
            }
            else
            {
                transform.position = new Vector3(4.0f, originalPosition.y, originalPosition.z);
            }
        }

        if ((Input.GetButtonDown("ChangeCamera") || Input.GetKeyDown(KeyCode.R)))
        {
            if (!Camera_Transition.ortho)
            {
                posX = transform.position.x;
            }
            else
            {
                if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
                {
                    transform.position = new Vector3(posX, transform.position.y, transform.position.z); //Mover al jugador
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
    }
}
