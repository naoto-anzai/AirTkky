using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_cursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 worldPos = myTransform.position;
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            worldPos.x = (mousePos.x)/100 - 4;
            worldPos.z = (mousePos.y)/100 - 3;
            myTransform.position = worldPos;
        }
    }
}
