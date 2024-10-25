using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] float cameraRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, cameraRotationSpeed * Time.deltaTime, 0);
    }
}
