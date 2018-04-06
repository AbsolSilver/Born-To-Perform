using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabilizer : MonoBehaviour
{
    public GameObject car;
    public float CarX, CarY, CarZ;

    // Update is called once per frame
    void Update()
    {
        // Getting the car's transform on the x, y and z axis.
        CarX = car.transform.eulerAngles.x;
        CarY = car.transform.eulerAngles.y;
        CarZ = car.transform.eulerAngles.z;

        // Transform the looking cube
        transform.eulerAngles = new Vector3(-CarX, CarY, -CarZ);
    }
}
