using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Objects to rotate
    [Header("Objects To Rotate")]
    public GameObject partToRotate;
    public float rotationSpeed = 10f;

    // Fire Bullet
    [Header("Fire Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    // Target
    [Header("Target")]
    public Transform target;
    public float range = 1000f;
    public string enemyTag = "Enemy";
    public bool nearestEnemyIsInsideCollider = false;



    void Start()
    {
        // some particle effect when created
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && this.gameObject.GetComponent<Collider>().bounds.Contains(enemy.transform.position))
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            // Assign our target
            target = nearestEnemy.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        Debug.DrawLine(transform.position, target.position, Color.red);

        // Rotation - makes things stretchy and weird?
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 newRotation = lookRotation.eulerAngles;
        partToRotate.transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, 0f);


        // Bullet Fire
        if (fireCountdown <= 0f)
        {
            ShootBullet();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void ShootBullet()
    {
        // Debug.Log("Shoot");
        GameObject firedBullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        BulletScript bullet = firedBullet.GetComponent<BulletScript>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    /*
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == nearestEnemy)
        {
            nearestEnemyIsInsideCollider = true;
            Debug.Log(other.gameObject + " Entered");
        }
        else
        {
            nearestEnemyIsInsideCollider = false;
        }
    }
    */
}
