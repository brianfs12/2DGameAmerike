using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public int health = 1;
    public int damageOfEnemy = 1;
    Transform playerLocation;
    public Vector3[] locations;
    public float speed;
    public int chosen;
    public int plusminus;
    public bool playerDetected;

    public bool vulnerable = true;
    WaitForSeconds invulneravility = new WaitForSeconds(1f);

    float targetRange = 5f;

    void Start()
    {

        playerDetected = false;
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Controller();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerDetected = true;
            playerLocation = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }*/

    void Findtarget()
    {
        if(Vector3.Distance(transform.position, playerLocation.position) < targetRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void Controller()
    {
        if (health > 0)
        {
            MovementController();
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        //Se necesita pulir codigo para animacion antes de morir
        Destroy(this.gameObject);
    }

    void MovementController()
    {
        Findtarget();
        if (playerDetected)
        {
            FollowPlayer();
        }
        else
        {
            MoveLocations();
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, speed  * Time.deltaTime);
    }

    void MoveLocations()
    {
        transform.position = Vector3.MoveTowards(transform.position, locations[chosen], speed * Time.deltaTime);
        Vector3 position = gameObject.transform.position;
        if (position == locations[chosen])
        {
            chosen = chosen + plusminus;
        }
        if (chosen == (locations.Length - 1))
        {
            plusminus = -1;
        }
        if (chosen == 0)
        {
            plusminus = 1;
        }
        if (transform.position.x < locations[chosen].x)
        {
            //SIRVE PARA DAR PERSPECTIVA 2D A UN OBJETO 3D
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -0.25f);
        }
        else
        {
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.25f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<TestJumpToKill>().TakeDamage(damageOfEnemy);
        }
    }

    public void TakeDamage(int damage)
    {
        if (vulnerable)
        {
            StartCoroutine(GetDamage(damage));
        }
    }

    IEnumerator GetDamage(int damage)
    {
        health -= damage;
        vulnerable = false;
        yield return invulneravility;
        vulnerable = true;
    }
}
