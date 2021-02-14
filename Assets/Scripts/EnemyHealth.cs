using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(int dmg) {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= dmg;
        print("Hit points remaining: " + hitPoints);
        if (hitPoints <= 0) {
            gameObject.GetComponent<Animator>().SetBool("Die", true);
            gameObject.GetComponent<EnemyAI>().Die();
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.01f);
        }
    }
}
