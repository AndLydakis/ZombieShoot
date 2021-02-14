using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Camera FPCamera;
    [SerializeField] protected float range = 100f;
    [SerializeField] protected float rof = 3f;
    [SerializeField] protected float reloadTime = 1.5f;
    [SerializeField] protected float startReload;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected int clipSize = 10;
    [SerializeField] protected bool isAuto = false;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected Ammo ammoSlot;
    [SerializeField] protected AmmoTypes ammoType;
    [SerializeField] protected TextMeshProUGUI ammoText;
    [SerializeField] protected AudioSource fireFx;
    protected bool isFiring = false;
    protected int ammoInClip = 0;

    protected DateTime lastShot;
    protected void Start() {
        Debug.Log("Weapon Start");
        lastShot = DateTime.Now;
        startReload = -1;
        fireFx = GetComponent<AudioSource>();
        if (ammoSlot == null) Debug.LogError("NULL AMMO");
        if (fireFx == null) Debug.LogError("NULL FX");
        Reload();
    }

    protected void OnEnable() {
        lastShot = DateTime.Now;
        isFiring = false;
    }

    protected void OnDisable() {
        isFiring = false;
    }


    protected void Update() {
        if (isReloading()) return;
        if (Input.GetButtonDown("Fire1")) Shoot();
        else if (isAuto && isFiring) Shoot();
        if (Input.GetButtonUp("Fire1")) isFiring = false;
        DisplayAmmo();
    }

    protected void DisplayAmmo() {
        ammoText.text = ammoType.ToString() + " ammo: " + ammoInClip + "/" + ammoSlot.GetAmmo();
    }

    protected void Reload() {
        if (ammoSlot.GetAmmo() == 0) return;
        int ammoSpent = clipSize - ammoInClip;
        int ammoToAdd = Math.Min(ammoSlot.GetAmmo(), ammoSpent);
        ammoSlot.AdjustAmmo(-ammoToAdd);
        ammoInClip += ammoToAdd;
        startReload = Time.time;
    }

    bool isReloading() {
        return (Time.time - startReload) <= reloadTime;
    }

    protected void Shoot() {
        Debug.Log("Weapon Shoot");
        TimeSpan ts = DateTime.Now - lastShot;
        double seconds = ts.TotalSeconds;
        if (seconds < 1 / rof) return;
        //if (ammoSlot.GetAmmo() == 0) return;
        //ammoSlot.AdjustAmmo(-1);
        lastShot = DateTime.Now;
        if (ammoInClip == 0) {
            Reload();
            return;
        }
        muzzleFlash.Play();
        ProcessRayCast();
        fireFx.PlayOneShot(fireFx.clip);
        ammoInClip = Math.Max(0, ammoInClip - 1);
        if (isAuto) isFiring = true;
    }

    protected virtual void ProcessRayCast() {
        Debug.Log("Weapon RayCast");
        RaycastHit hit;
        Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward, Color.red, 10, false);
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
    }

    protected void CreateHitImpact(RaycastHit hit) {
        GameObject hitEffectInstance = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitEffectInstance, 0.1f);
    }
}
