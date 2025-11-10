using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterDeath : MonoBehaviour
{
    [TextArea] public string deathMessage = "You fell in the water";
    void Start()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the water: " + other.name);
        if (other.CompareTag("Player"))
        {
            // Play lose sfx
            AudioManager.Instance?.PlayLose();
            // call GameManager to go to GameOver scene with message
            GameManager.Instance?.PlayerFellInWater(deathMessage);
        }
    }
}
