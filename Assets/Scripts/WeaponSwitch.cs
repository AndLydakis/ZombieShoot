using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    Ammo ammoManager;
    void Start() {
        ammoManager = FindObjectOfType<Ammo>();
        SetWeaponActive();
    }

    void SetWeaponActive() {
        int weaponIndex = 0;
        foreach (Transform wp in transform) {
            if (weaponIndex == currentWeapon) wp.gameObject.SetActive(true);
            else wp.gameObject.SetActive(false);
            weaponIndex++;
        }
    }

    void SetWeaponActive(int prev, int wpidx) {
        print("Previous: " + prev + " Next: " + wpidx);
        int num_children = transform.childCount;
        print(num_children + " total weapons");
        if (wpidx > num_children) return;
        transform.GetChild(prev).gameObject.SetActive(false);
        transform.GetChild(wpidx).gameObject.SetActive(true);
        ammoManager.SetAmmoType(wpidx);

    }

    void Update() {
        if (Time.timeScale == 0) return;
        int prevWeap = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();
        if (prevWeap != currentWeapon) SetWeaponActive(prevWeap, currentWeapon);
    }

    void ProcessKeyInput() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeapon = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentWeapon = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentWeapon = 2;
    }
    void ProcessScrollWheel() {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            currentWeapon = (currentWeapon + 1) % transform.childCount;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (currentWeapon == 0) currentWeapon = transform.childCount - 1;
            else currentWeapon = (currentWeapon - 1) % (transform.childCount - 1);
        }
    }
}
