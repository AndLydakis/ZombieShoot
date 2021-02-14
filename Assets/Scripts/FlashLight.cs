using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = .1f;
    [SerializeField] float minAngle = 5f;
    [SerializeField] float minIntensity = 0.1f;
    [SerializeField] float defaultIntensity = 20f;
    [SerializeField] float defaultAngle = 70f;
    [SerializeField] TextMeshProUGUI flashlightText;

    bool isOn = false;
    Light self;
    void Start() {
        self = GetComponent<Light>();
        Recharge(defaultIntensity, defaultAngle);
    }

    void DecreaseAngle() {
        self.spotAngle = Mathf.Max(minAngle, self.spotAngle - angleDecay * Time.deltaTime);
    }

    void DecreaseIntensity() {
        self.intensity = Mathf.Max(minIntensity, self.intensity - lightDecay * Time.deltaTime);
        Debug.Log(self.intensity);
    }

    void Update() {
        DecreaseAngle();
        DecreaseIntensity();
        int percentage = (int)((self.intensity / defaultIntensity) * 100f);
        flashlightText.text = "Battery: " + percentage + "%";
    }

    public void Recharge(float intensity, float angle) {
        self.spotAngle = Mathf.Min(defaultAngle, self.spotAngle + intensity);
        self.intensity = Mathf.Min(defaultIntensity, self.spotAngle + angle);
    }
}
