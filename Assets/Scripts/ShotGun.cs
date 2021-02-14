using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotGun : Weapon
{
    [SerializeField] float shotAngle = 1.5f;
    [SerializeField] float pelletsPerShot = 8;

    void Start() {
        Debug.Log("Shotgun Start");
        base.Start();
        Reload();
    }

    protected override void ProcessRayCast() {
        for (int i = 0; i < pelletsPerShot; ++i) {
            RaycastHit hit;
            Vector3 newVector = FPCamera.transform.forward;

            float spread_x = UnityEngine.Random.Range(-shotAngle, shotAngle);
            float spread_y = UnityEngine.Random.Range(-shotAngle, shotAngle);
            newVector = Quaternion.Euler(spread_x, spread_y, 0) * newVector;
            //Make the direction match the transform
            //It is like converting the Vector3.forward to transform.forward
            //direction = transform.TransformDirection(direction.normalized);

            Debug.DrawRay(FPCamera.transform.position, newVector, Color.red, 10, false);
            if (Physics.Raycast(FPCamera.transform.position, newVector, out hit, range)) {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) continue;
                target.TakeDamage(damage);
            }
        }
        //Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward, Color.red, 10, false);
        //if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
        //    CreateHitImpact(hit);
        //    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
        //    if (target == null) return;
        //    target.TakeDamage(damage);
        //}
    }
}
