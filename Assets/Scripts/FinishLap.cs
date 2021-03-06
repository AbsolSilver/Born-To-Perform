﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BornToPerform
{
    public class FinishLap : MonoBehaviour
    {

        public GameObject FinishTrig, HalfTrig;

        public GameObject MinsDisplay, SecsDisplay, MillisDisplay;

        public GameObject LapTimeBox;

        // when we enter the collider, show the player's best record time
        void OnTriggerEnter(Collider other)
        {
            if (LapTimeManager.SecsCount <= 9)
            {
                SecsDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecsCount + ".";
            }
            else
            {
                SecsDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecsCount + ".";
            }

            if (LapTimeManager.MinsCount <= 9)
            {
                MinsDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinsCount + ":";
            }
            else
            {
                MinsDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinsCount + ":";
            }

            if (LapTimeManager.SecsCount <= 9)
            {
                SecsDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecsCount + ".";
            }
            else
            {
                SecsDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecsCount + ".";
            }

            if (other.tag == "Car")
            {
                // Turn on the halfway trigger and turn off the finish trigger.
                FinishTrig.SetActive(false);
                HalfTrig.SetActive(true);
                Debug.Log("Finish mark has been entered");
            }
        }
    }
}


