using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyController controller;
    public int maxHealth;
    public int currentHealth;
    public int damageOfEnemy;
    public EnemyController.EnemyType enemyType;

    public void Start()
    {
        maxHealth = controller.SetHealth(enemyType);
        currentHealth = maxHealth;
        controller.SetDamage(enemyType, damageOfEnemy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetContact(0).normal.y < 0)
        {
            print(collision.contacts[0].normal.y);
            TakeDamage(1);
            collision.rigidbody.AddRelativeForce(Vector3.up * 15.0f, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
