using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfWayTrig : MonoBehaviour
{

    public GameObject FinishTrig;
    public GameObject HalfTrig;

    // If we enter the collider, turn on the finish collider and turn off the halfway mark trigger.
    void OnTriggerEnter(Collider other)
    {
        FinishTrig.SetActive(true);
        HalfTrig.SetActive(false);
    }
}
