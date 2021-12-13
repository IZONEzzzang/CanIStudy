using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Insta : MonoBehaviour
{
    public float speed = 8f;
    public static int enemyCount = 0;
    public float startHealth = 100;
    private float health;
    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        health = startHealth;
        target = Waypoints.points[0];
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);

        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Enemy.enemyCount--;
    }

    void Update()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            enemyCount--;
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
