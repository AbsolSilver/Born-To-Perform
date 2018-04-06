using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BornToPerform
{
    public class LapTimeManager : MonoBehaviour
    {
        public static int MinsCount;
        public static int SecsCount;
        public static float MillisCount;
        public string MillisDisplay;

        public GameObject MinsText;
        public GameObject SecsText;
        public GameObject MillisText;

        // Update is called once per frame
        void Update()
        {
            MillisCount += Time.deltaTime * 10;
            MillisDisplay = MillisCount.ToString("F0");
            MillisText.GetComponent<Text>().text = "." + MillisDisplay;

            // 10 milliseconds = 1 second
            if (MillisCount >= 10)
            {
                MillisCount = 0;
                SecsCount += 1;
            }
            // Update the second count
            if (SecsCount <= 9)
            {
                SecsText.GetComponent<Text>().text = "0" + SecsCount + ".";
            }
            // its a double digit number, so dont update it
            else
            {
                SecsText.GetComponent<Text>().text = "" + SecsCount + ".";
            }

            // 60 seconds = 1 minute
            if (SecsCount >= 60)
            {
                SecsCount = 0;
                MinsCount += 1;
            }
            // Update the minute count
            if (MinsCount <= 9)
            {
                MinsText.GetComponent<Text>().text = "0" + MinsCount + ":";
            }
            // its a double digit number, dont update it.
            else
            {
                MinsText.GetComponent<Text>().text = "" + MinsCount + ":";
            }
        }
    }
}


