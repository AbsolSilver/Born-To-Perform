using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public bool showOptions;
    public bool showResolution;
    public bool isFullscreen;
    public bool isMute;
    //float values for brightness and volume
    public int index;
    public int[] resX, resY;
    //keycodes for your keybinding 

    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        if (isPaused == true)
        {
            /*
     button for return
        -toggle paused and timescale
     button for options
        -toggle bool for show options
     button for quit to menu
        -change scenes
     button for quit to desktop
        -quit
        */
        }
        if (showOptions == true)
        {
            /*
    back ground box
    sliders for volume
    sliders for brightness cur pos min  max
        -floatValue = GUI.HorizontalSlider(new Rect(x, y, x, y), floatValue, 0.0F, 1.0F);
    buttons for Keybinding
    movement and actions
    Dropdown for resolutions
        -Button that has a showResolutions bool
        if show resolutions
            -Box as a background
           Together we will make a for loop
           that has buttons which are labeled our resolutions and set the resolution
     Button toggle for mute
     button toggle for fullscreen
     button for back
        -showOptions bool toggle
     */
        }
    }

    bool TogglePause()
    {
        return true && false;
        /*
   if paused
       start time
       unpause
       lock cursor
       hider cursor
       false
   else
       stop time
       pause
       unlock cursor
       show cursor
       true
*/
    }
}
