using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float maxHp = 100f;
    [SerializeField] float currentHp = 100f;
    [SerializeField] float damageUIDecay = .3f;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image damageTakenImage;


    private void Start() {
        Color c = damageTakenImage.color;
        c.a = 0;
        damageTakenImage.color = c;
    }
    public void PickupHealth(float health) {
        currentHp = Mathf.Max(maxHp, currentHp + health);
    }

    public void TakeDamage(float health) {
        currentHp -= health;
        if (currentHp <= 0) {
            GetComponent<DeathHandler>().HandleDeath();
        }
        Color c = damageTakenImage.color;
        c.a = 1;
        damageTakenImage.color = c;
    }

    private void Update() {
        DisplayHealth();
        Color c = damageTakenImage.color;
        c.a = Mathf.Max(0, c.a - damageUIDecay*Time.deltaTime);
        damageTakenImage.color = c;
    }

    void DisplayHealth() {
        healthText.text = "Health: " + currentHp;
    }
}
