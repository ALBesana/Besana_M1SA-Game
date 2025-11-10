using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectibleCoin : MonoBehaviour
{
    public int value = 1;
    public AudioClip collectSound;
    public float shrinkDuration = 0.4f;
    private AudioSource audioSource;
    private bool collected = false;

    [Header("Visual Settings")]
    public Vector3 rotationSpeed = new Vector3(0, 90f, 0); // degrees per second

    void Start()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Rotate coin continuously
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (other.CompareTag("Player"))
        {
            collected = true;
            if (collectSound != null) audioSource.PlayOneShot(collectSound);
            ScoreManager.instance?.AddScore(value);
            StartCoroutine(ShrinkAndDestroy());
        }
    }

    IEnumerator ShrinkAndDestroy()
    {
        Vector3 orig = transform.localScale;
        float t = 0;
        while (t < shrinkDuration)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(orig, Vector3.zero, t / shrinkDuration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
