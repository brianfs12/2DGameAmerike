using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Rigidbody rigi;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public EnemyController controller;

    [Header("Estadisticas")]
    public int Health;
    public float TimeOfInvulnerability;
    public float TimeUntilNextAttack;

    bool canAttack = true;//Para esperar entre cada ataque
    bool invulnerable = false;//Para hacer inmune al jugador por unos segundos despues de ser golpeado

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rigi = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.L)) && canAttack)
        {
            Attack();
            audioManager.playSound(Sounds.attack);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !invulnerable && collision.GetContact(0).normal.y > 0)
        {
            rigi.AddRelativeForce(Vector3.up * 15.0f, ForceMode.Impulse);
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(1);
            //Aun no funciona bien
        }
        if (collision.gameObject.CompareTag("Enemy") && !invulnerable && collision.GetContact(0).normal.y <= 0)
        {        
            takeDamage(collision.GetContact(0).normal);
        }
    }

    void Attack()
    {
        ///Play animation
        //animator.SetTrigger("Attack");
        ///Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);
        ///Deal damage
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(attackDamage);
        }
        StartCoroutine(waitToAttack());
    }

    public void takeDamage(Vector3 _direction)
    {
        Health -= 1;
        rigi.AddForce(_direction * 10.0f, ForceMode.Impulse); //Impulsar al jugador hacia atras cuando recibe daño
        rigi.AddRelativeForce(Vector3.up * 10.0f, ForceMode.Impulse);
        StartCoroutine(waitToTakeDamage());
    }

    //Esperar para volver a tomar daño
    IEnumerator waitToTakeDamage()
    {
        invulnerable = true;
        yield return new WaitForSeconds(TimeOfInvulnerability);
        invulnerable = false;
    }

    //Esperar para poder volver a atacar
    IEnumerator waitToAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(TimeUntilNextAttack);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
