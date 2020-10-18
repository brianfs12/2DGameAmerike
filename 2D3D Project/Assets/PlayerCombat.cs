using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public EnemyController controller;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Button X"))
        {
            Attack();
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
            Debug.Log("golpeado" + enemy.name);
        }


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
