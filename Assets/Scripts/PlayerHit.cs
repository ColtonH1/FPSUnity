using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit");
    }
}
