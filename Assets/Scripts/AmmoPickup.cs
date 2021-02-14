using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    [SerializeField] int ammo = 10;
    [SerializeField] AmmoTypes type;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Ammo>().AdjustAmmo((int)type, ammo);
            Debug.Log("Picked up "+ ammo + " " + type.ToString() + " ammo");
            Destroy(gameObject);
        }
    }

    private void Update() {
        transform.Rotate(Vector3.up * -90.0f * Time.deltaTime);
    }

}
