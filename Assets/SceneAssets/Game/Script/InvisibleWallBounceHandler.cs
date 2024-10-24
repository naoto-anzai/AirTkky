using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallBounceHandler : MonoBehaviour
{
    [SerializeField] Transform puck;

    private void Awake()
    {
        Physics.IgnoreCollision(puck.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
