using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float rof = 3f;
    [SerializeField] int damage = 10;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoTypes ammoType;
    [SerializeField] TextMeshProUGUI ammoText;

    DateTime lastShot;
    void Start() {
        lastShot = DateTime.Now;
    }

    private void OnEnable() {
        lastShot = DateTime.Now;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) Shoot();
        DisplayAmmo();
    }

    void DisplayAmmo() {
        ammoText.text = ammoType.ToString() + " ammo: " + ammoSlot.GetAmmo();
    }

    private void Shoot() {
        TimeSpan ts = DateTime.Now - lastShot;
        double seconds = ts.TotalSeconds;
        print(seconds);
        if (seconds < 1 / rof) return;
        if (ammoSlot.GetAmmo() == 0) return;
        ammoSlot.AdjustAmmo(-1);
        lastShot = DateTime.Now;
        muzzleFlash.Play();
        ProcessRayCast();
    }

    private void ProcessRayCast() {
        RaycastHit hit;
        Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward, Color.red, 10, false);
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
    }

    private void CreateHitImpact(RaycastHit hit) {
        GameObject hitEffectInstance = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitEffectInstance, 0.1f);
    }
}
