using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

namespace BornToPerform
{
    public class PitstopDialogue : MonoBehaviour
    {

        public string[] repairText;

        public string response1, response2;

        private string[] text;

        public int index, option;

        public bool showDlg;

        public bool showRepairOptions;


        public GameObject player;
        public GameObject mainCam;

        public string npcName;


        // Use this for initialization
        void Start()
        {
            // Find the player and main camera objects
            player = GameObject.FindGameObjectWithTag("Player");
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");

            text = new string[2];

            for (int i = 0; i < text.Length; i++)
            {
                text[i] = repairText[i];
            }
        }

        void OnGUI()
        {
            // if our dialogue can be seen on screen
            if (showDlg)
            {
                // setting the screen resolution
                float scrW = Screen.width / 16;
                float scrH = Screen.height / 9;
                //the dialogue box will appears and displays the NPC's name and beginning conversation dialogue
                GUI.Box(new Rect(0, 6 * scrH, Screen.width, 3 * scrH), npcName + ": " + text[index]);
                // if not at the end of the dialogue or not at the options part
                if (!(index + 1 >= text.Length || index == option))
                {
                    // next button allows us to skip forward to the next line or dialogue
                    if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), ">>>"))
                    {
                        index++;
                    }
                }
                // else if we are in options
                else if (index == option)
                {
                    // the button closes the dialogue UI
                    if (GUI.Button(new Rect(15 * scrW, 8.5f, scrW, 0.5f * scrH), response1))
                    {
                        index++;
                        showRepairOptions = true;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(15 * scrW, 8.5f, scrW, 0.5f * scrH), "See Ya!"))
                    {
                        // close the dialogue box
                        showDlg = false;
                        // set index back to 0
                        index = 0;
                        // allow the camera to be turned back on
                        player.GetComponent<Camera>().enabled = true;
                        // turns the car controller on
                        player.GetComponent<CarController>().enabled = true;
                        // turns the car's audio on
                        player.GetComponent<CarAudio>().enabled = true;
                        // turns the car's user input on
                        player.GetComponent<CarUserControl>().enabled = true;
                    }
                }
            }
        }

    }
}
