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
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
