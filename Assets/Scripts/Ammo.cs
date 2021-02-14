using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [System.Serializable]
    class AmmoSlot
    {
        public AmmoTypes ammoType;
        public int ammoLeft;
        public int maxAmmo;
    }

    [SerializeField] AmmoSlot[] ammoSlots;
    AmmoSlot currentAmmoSlot;
    public void AdjustAmmo(int a) {
        currentAmmoSlot.ammoLeft += a;
        currentAmmoSlot.ammoLeft = Mathf.Clamp(currentAmmoSlot.ammoLeft, 0, currentAmmoSlot.maxAmmo);
    }

    public void AdjustAmmo(int type, int amount) {
        ammoSlots[type].ammoLeft += amount;
        ammoSlots[type].ammoLeft = Mathf.Clamp(ammoSlots[type].ammoLeft, 0, ammoSlots[type].maxAmmo);
    }

    public void SetAmmoType(int newType) {
        currentAmmoSlot = ammoSlots[newType];
    }

    public int GetAmmo() {
        return currentAmmoSlot.ammoLeft;
    }

    void Start() {
        currentAmmoSlot = ammoSlots[0];
    }

    void Update() {

    }
}
