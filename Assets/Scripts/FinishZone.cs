using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FinishZone : MonoBehaviour
{
    void Start()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
        Debug.Log($"[FinishZone] Collider set as trigger on '{gameObject.name}'.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // play win sound
            AudioManager.Instance?.PlayWin();

            if (GameManager.Instance == null)
            {
                Debug.LogWarning("[FinishZone] GameManager.Instance is null! Creating temporary one...");
                GameObject gmObj = new GameObject("GameManager_Temp");
                gmObj.AddComponent<GameManager>();
            }
            GameManager.Instance?.LoadNextLevelOrFinish();
        }
    }
}
