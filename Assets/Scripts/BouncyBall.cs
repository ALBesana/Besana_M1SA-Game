using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BouncyBall : MonoBehaviour
{
    public float bounceForce = 12f;

    void Start()
    {
        // Make sure it’s a trigger for CharacterController
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls pc = other.GetComponent<PlayerControls>();
            if (pc != null)
            {
                pc.ApplyBounce(bounceForce);

                // Tiny nudge so CharacterController isn't stuck inside the ball
                CharacterController cc = other.GetComponent<CharacterController>();
                if (cc != null)
                    cc.Move(Vector3.up * 0.05f);

                Debug.Log("Bounce applied: " + bounceForce);
            }

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayBounce();
        }
    }
}
