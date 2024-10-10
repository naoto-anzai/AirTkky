using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_cup_mouse : MonoBehaviour
{
    [SerializeField] Transform cube;
    [SerializeField] float forceMultiplier = 10;
    [SerializeField] float minSpeed = 0.1f;

    Rigidbody rigidBody;
    Vector3 closestPoint;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        closestPoint = transform.position;
    }

    void Update()
    {
        MoveTo(closestPoint);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 mousePos = ray.GetPoint(enter);
            Vector3 targetPos = new Vector3(mousePos.x, transform.position.y, mousePos.z);

            closestPoint = cube.GetComponent<Collider>().ClosestPoint(targetPos);
        }
    }

    void MoveTo(Vector3 target)
    {
        transform.position = target;
        return;

        /*
        Vector3 direction = (target - rigidBody.position).normalized;
        float distance = Vector3.Distance(target, rigidBody.position);

        if (distance < minSpeed) return;

        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(direction*Mathf.Pow(distance*forceMultiplier, 2));
        */
    }
}