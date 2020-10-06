using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Level01Controller level01Controller = new Level01Controller();
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            level01Controller.TakeDamage(1);
        }
    }
}
