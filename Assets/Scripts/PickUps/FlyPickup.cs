using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPickup : MonoBehaviour
{
    public float flyDuration = 5f;

    private MeshRenderer meshRenderer;

    void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();

        // Disable the MeshRenderer initially
        meshRenderer.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartFlying();
                StartCoroutine(FlyDurationCoroutine(playerMovement));
                Destroy(gameObject); // Destroy the FlyPickup
            }
        }
    }

    IEnumerator FlyDurationCoroutine(PlayerMovement playerMovement)
    {
        yield return new WaitForSeconds(flyDuration);
        playerMovement.StopFlying();
    }
}
