using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Translucid : MonoBehaviour
{
    GameObject player;
    public LayerMask objects;
    Color opacity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //NO ESTA TERMINADO
        Vector3 dir = player.transform.position - transform.position; //Direccion de la camara al jugador

        Debug.DrawRay(transform.position, dir, Color.green);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, 10.0f, objects))
        {
            Color mat = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
            opacity = new Color(mat.r, mat.g, mat.b, 0.5f);
            hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = opacity;
        }
    }
}
