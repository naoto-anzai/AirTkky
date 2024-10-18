using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaddleManager : MonoBehaviour
{
    Vector3 position;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if(plane.Raycast(ray, out float enter))
        {
            Vector3 mousePos = ray.GetPoint(enter);
            transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);
        }
    }
}
