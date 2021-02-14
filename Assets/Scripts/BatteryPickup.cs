using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{

    [SerializeField] float recInt = 10f;
    [SerializeField] float recAngle = 5f;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponentInChildren<FlashLight>().Recharge(recInt, recAngle);
            Debug.Log("Battery Recharged");
            Destroy(gameObject);
        }
    }

    private void Update() {
        transform.Rotate(Vector3.up * -90.0f * Time.deltaTime);
    }

}
