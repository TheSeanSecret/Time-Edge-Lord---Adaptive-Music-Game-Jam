using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject TurretHead;
    public GameObject TurretBarrel;
    public GameObject BulletSpawn;

    public Vector3 targetPosition;
    public float smoothRotationSpeed = 5;

    public bool rotateTurret;
    Vector3 turretAim;
    public GameObject fakeRayCast;

    public Transform enemyToAimAt;

    void Start()
    {
        // some particle effect when created

        //transform.rotation.y 

    }

    void Update()
    {
        // If there are enemies left then execute LookAtEnemyAndShoot();
        LookAtEnemyAndShoot();
        RotateTurret();
    }

    void LookAtEnemyAndShoot()
    {
        // If an enemy is the closest to us AND is within a certain boundary

        

        // We aim at that position
        // We instantiate a bullet at a certain intervall
        // give it the closest enemy transform
        //      
    }

    void RotateTurret()
    {
        Vector3 rotateHeadTowardsTargetPos = new Vector3(enemyToAimAt.position.x, transform.position.y, enemyToAimAt.position.z);

        // Rotate head only on the Y axis
        TurretHead.transform.rotation = Quaternion.Lerp(TurretHead.transform.rotation, Quaternion.LookRotation(rotateHeadTowardsTargetPos + transform.position), smoothRotationSpeed * 2 * Time.deltaTime);

        // Rotate barrel only on the X axis from its edge

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyToAimAt = other.gameObject.transform;
            Debug.Log("Entered Range");
        }
    }
}
