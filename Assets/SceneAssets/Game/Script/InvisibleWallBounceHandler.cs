using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallBounceHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle")) return;
        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
    }
}
