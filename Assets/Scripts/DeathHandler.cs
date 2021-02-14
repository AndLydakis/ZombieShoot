using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public void Start() {
        canvas.enabled = false;
    }
    public void HandleDeath() {
        Time.timeScale = 0;
        canvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
