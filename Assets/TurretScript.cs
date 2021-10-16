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



    void Start()
    {
        // some particle effect when created

        //transform.rotation.y 

    }

    void Update()
    {
        // If there are enemies left then execute LookAtEnemyAndShoot();
        LookAtEnemyAndShoot();
    }

    void LookAtEnemyAndShoot()
    {
        // If an enemy is the closest to us AND is within a certain boundary

        

        // We aim at that position
        // We instantiate a bullet at a certain intervall
        // give it the closest enemy transform
        //      
    }


}
