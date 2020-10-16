using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyController controller;
    public bool vulnerable = true;
    WaitForSeconds invulneravility = new WaitForSeconds(1f);
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
        if (vulnerable)
        {
            StartCoroutine(GetDamage(damage));
        }
    }

    IEnumerator GetDamage(int damage)
    {
        currentHealth -= damage;
        vulnerable = false;
        yield return invulneravility;
        vulnerable = true;
    }
}
