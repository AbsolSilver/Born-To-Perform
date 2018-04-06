using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("MAIN MENU")]
    [Space(5)]
    [Header("Screens")]
    public float scrW;
    public float scrH;
    public bool loadScreen;
    public bool showOptions;

    // Loads the main titles and the screen's relative dimensions
    void Start()
    {
        loadScreen = true;
        showOptions = false;
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
    }
    // If we press any key, we close the main titles
    void Update()
    {
        if (loadScreen)
        {
            if (Input.anyKey)
            {
                Debug.Log("A key or mouse click has been detected");
                loadScreen = false;
            }
        }
    }
    void OnGUI()
    {
        // if we loadScreen, display the logo and press ___ to continue
        if (loadScreen)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(2 * scrW, 0.5f * scrH, 12 * scrW, 3 * scrH), "Logo / Title");
            GUI.Box(new Rect(4 * scrW, 4.5f * scrH, 8 * scrW, 1 * scrH), "Press AnyKey");
        }
        else
        // show the Play, Options and Exit buttons
        {
            if (showOptions == false)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                // show the logo
                GUI.Box(new Rect(2 * scrW, 0.5f * scrH, 12 * scrW, 3 * scrH), "Logo / Title");

                // if we click Play, load the game.
                if (GUI.Button(new Rect(4 * scrW, 4.5f * scrH, 8 * scrW, 1 * scrH), "Play"))
                {
                    SceneManager.LoadScene(1);
                }
                // if we click Options, open the options menu
                if (GUI.Button(new Rect(4 * scrW, 5.5f * scrH, 8 * scrW, 1 * scrH), "Options"))
                {
                    showOptions = true;
                }
                // if we click Exit, close the application aka exit the game.
                if (GUI.Button(new Rect(4 * scrW, 6.5f * scrH, 8 * scrW, 1 * scrH), "Exit"))
                {
#if UNITY_EDITOR
                    if (EditorApplication.isPlaying == false)
                    {
#endif
                        Application.Quit();
                    }

                }
                else
                // show the options menu
                {
                    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                    GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                    GUI.Box(new Rect(8.125f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                    GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1 * scrH), "Audio");
                    GUI.Box(new Rect(0.5f * scrW, 1.5f * scrH, 7.125f * scrW, 1 * scrH), "Brightness");
                    GUI.Box(new Rect(0.5f * scrW, 2.5f * scrH, 7.125f * scrW, 6 * scrH), "Resolutions");

                    // if we click Back, close the options menu and return to the main menu
                    if (GUI.Button(new Rect(12.5f * scrW, 8 * scrH, 3 * scrW, 0.5f * scrH), "Back"))
                    {
                        showOptions = false;
                    }
                }
            }
        }
    }
}
