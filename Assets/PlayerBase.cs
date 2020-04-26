using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    public int baseHealth = 10;

    public GameObject healthUITxt;
    private Text healthtxt;

    void Start() {
        healthtxt = healthUITxt.GetComponent<Text>();
        healthtxt.text = baseHealth.ToString();
    }


    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        Invoke("BaseTakesDamage", 3f);
    }

    private void BaseTakesDamage() {
        baseHealth--;
        healthtxt.text = baseHealth.ToString();
        if (baseHealth > 0) {
            Debug.Log("Base has " + baseHealth + " health left");
        } else {
            Debug.Log("Game Over");
        }
    }

}
