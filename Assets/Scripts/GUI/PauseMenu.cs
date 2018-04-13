using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

namespace BornToPerform
{
    public class PauseMenu : MonoBehaviour
    {
        #region Variables
        [Header("Variables")]
        [Space(3)]
        #region Bools
        [Header("Paused and Options Menu bools")]
        public bool isPaused;
        public bool showOptions;
        public bool isMute;
        [Space(3)]
        #endregion
        #region Screen Size
        [Header("Screen Size")]
        public float scrW = Screen.width / 16;
        public float scrH = Screen.height / 9;
        [Space(3)]
        #endregion
        #region Audio and Brightness
        [Header("Audio and Brightness")]
        public float audioSlider;
        public float brightnessSlider;
        public float amBrightSlider;
        public AudioSource mainMusic;
        public Light brightness;
        [Space(3)]
        #endregion
        #region Keybinding
        [Header("Controls")]
        public KeyCode forward;
        public KeyCode reverse;
        public KeyCode TurnLeft;
        public KeyCode TurnRight;
        public KeyCode holdingKey;
        [Space(3)]
        #endregion
        #region Resolutions 
        [Header("Resolutions")]
        public bool showResolution;
        public Vector2[] res;
        public bool showDropdown = true;
        public Vector2 scrollPos;
        public string resolution = "Resolution";
        public bool isFullscreen, isWindowed;
        [Space(3)]
        #endregion
        #region GUI Skin and Styles
        [Header("GUI Skins and Style")]
        public GUISkin BTPskin;
        public GUIStyle Paused;
        public GUIStyle Resume;
        public GUIStyle Options;
        public GUIStyle Exit;
        public GUIStyle Quit;
        public GUIStyle Audio;
        public GUIStyle Brightness;
        public GUIStyle Resolutions;
        public GUIStyle Controls;
        public GUIStyle Forward;
        public GUIStyle Reverse;
        public GUIStyle Left;
        public GUIStyle Right;
        public GUIStyle Apply;
        public GUIStyle Back;
        public GUIStyle ResolutionButton;
        public GUIStyle ResSelectionButton;
        public GUIStyle ForwardButton;
        public GUIStyle ReverseButton;
        public GUIStyle LeftButton;
        public GUIStyle RightButton;
        public GUIStyle ForwardString;
        public GUIStyle ReverseString;
        public GUIStyle LeftString;
        public GUIStyle RightString;
        public GUIStyle AudioString;
        public GUIStyle BrightnessString;
        #endregion
        #endregion

        private void Start()
        {
            #region Screen References
            // When we start the program, get the set screen size.
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
            #endregion
            #region Audio and Brightness References
            // Grabbing the Audio Source and Light component from the Pause Menu game object
            brightness = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Light>();
            mainMusic = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<AudioSource>();

            // Getting the audio and light component to connect with the sliders in this script
            if (PlayerPrefs.HasKey("Mute"))
            {
                RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("AmLight");
                brightness.intensity = PlayerPrefs.GetFloat("DirLight");
                brightnessSlider = brightness.intensity;
                amBrightSlider = RenderSettings.ambientIntensity;
                audioSlider = PlayerPrefs.GetFloat("MainMusicVolume");

                // If int mute = 0, mute is on
                if (PlayerPrefs.GetInt("Mute") == 0)
                {
                    isMute = true;
                    mainMusic.volume = 0;
                }
                // if int mute > 0, mute is off
                else
                {
                    isMute = false;
                    audioSlider = mainMusic.volume;
                }
            }
            else
            {
                // Get the set audio and brightness component and slider values
                audioSlider = mainMusic.volume;
                brightnessSlider = brightness.intensity;
                amBrightSlider = RenderSettings.ambientIntensity;
            }
            #endregion
            #region Keybinding References
            // Getting the set key commands for our controls
            forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "UpArrow"));
            reverse = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Reverse", "DownArrow"));
            TurnLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Turn Left", "LeftArrow"));
            TurnRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Turn Right", "RightArrow"));
            #endregion
        }

        private void Update()
        {
            #region Screen size
            // Making sure the GUI is in the center of the screen.
            if (Screen.width / 16 != scrW || Screen.height / 9 != scrH)
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;
            }
            #endregion
            #region isPaused
            // If the Enter key is pressed, pause the game and stop the music
            if (Input.GetKey(KeyCode.Return))
            {
                isPaused = true;
                mainMusic.Stop();
                Time.timeScale = 0;
            }
            #endregion          
            #region ShowOptions
            // if showOptions = true, get and set the default values of the audio and brightness slider
            if (showOptions)
            {
                if (mainMusic.volume != audioSlider)
                {
                    mainMusic.volume = audioSlider;
                }
                if (brightness.intensity != brightnessSlider)
                {
                    brightness.intensity = brightnessSlider;
                }
                if (RenderSettings.ambientIntensity != amBrightSlider)
                {
                    RenderSettings.ambientIntensity = amBrightSlider;
                }

            }
            #endregion
        }

        void OnGUI()
        {
            // Getting the Born To Perform GUI skin
            GUI.skin = BTPskin;

            // if isPaused = true, display the Pause Menu and button
            if (isPaused == true)
            {
                GUI.Box(new Rect(6.5f * scrW, 2.5f * scrH, 3 * scrH, 5 * scrH), "");
                GUI.Box(new Rect(7.5f * scrW, 2.5f * scrH, 1 * scrW, 0.5f * scrH), "" , Paused);

                #region Resume
                // if Resume is pressed, continue where you left off in the game
                if (GUI.Button(new Rect(7.25f * scrW, 3.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Resume", Resume))
                {
                    // Disabled pause
                    isPaused = false;
                    // Resumes the game
                    Time.timeScale = 1;
                    mainMusic.Play();

                }
                #endregion
                #region Options
                // If Options button is pressed, Display the options menu
                if (GUI.Button(new Rect(7.25f * scrW, 4.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Options", Options))
                {
                    showOptions = true;
                    isPaused = false;
                    mainMusic.Play();
                }
                #endregion
                #region Exit To Main Menu
                // if Exit button is pressed, reload the game back to the loadScreen
                if (GUI.Button(new Rect(7.25f * scrW, 5.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Exit", Exit))
                {
                    SceneManager.LoadScene(0);
                }
                #endregion
                #region Quit
                // if Quit is pressed, close the application AND exit PlayMode in editor
                if (GUI.Button(new Rect(7.25f * scrW, 6.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Quit", Quit))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
                #endregion
            }
            // if showOptions = true, display the Options (Audio, Brightness, resolution dropdown, isFullscreen & isWindowed bools, Keybinding and Saving and return to the main menu
            if (showOptions == true)
            {
                #region Backgrounds
                GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                GUI.Box(new Rect(8.125f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                #endregion
                #region Headings
                // Headings from all our options
                GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1f * scrH), "Audio", Audio);
                audioSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 1f * scrH, 7.125f * scrW, 0.25f * scrH), audioSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 1.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(audioSlider * 100).ToString(), AudioString);

                GUI.Box(new Rect(0.5f * scrW, 1.5f * scrH, 7.125f * scrW, 1f * scrH), "Brightness", Brightness);
                brightnessSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 2f * scrH, 7.125f * scrW, 0.25f * scrH), brightnessSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 2.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(brightnessSlider * 100).ToString(), BrightnessString);

                GUI.Box(new Rect(0.5f * scrW, 2.5f * scrH, 7.125f * scrW, 6f * scrH), "Resolutions", Resolutions);

                GUI.Box(new Rect(8.4f * scrW, 0.25f * scrH, 7.125f * scrW, 8f * scrH), "Controls", Controls);
                GUI.Box(new Rect(9.75f * scrW, 1f * scrH, 1f * scrW, .5f * scrH), "Forward", Forward);
                GUI.Box(new Rect(12.75f * scrW, 1f * scrH, 1f * scrW, .5f * scrH), "Reverse", Reverse);
                GUI.Box(new Rect(9.75f * scrW, 2f * scrH, 1f * scrW, .5f * scrH), "Turn Left", Left);
                GUI.Box(new Rect(12.75f * scrW, 2f * scrH, 1f * scrW, .5f * scrH), "Turn Right", Right);
                Event e = Event.current;
                #endregion
                #region Fullscreen and Windowed Toggles
                // if Fullscreen toggle is pressed, enable isFullScreen and disable isWindowed
                if (GUI.Toggle(new Rect(4.5f * scrW, 4.5f * scrH, 2 * scrW, 0.5f * scrH), isFullscreen, "    Fullscreen"))
                {
                    Screen.fullScreen = true;
                    isFullscreen = true;
                    isWindowed = false;
                }
                //    if Windowed toggle is pressed, enable isWindowed and disable isFullscreen
                if (GUI.Toggle(new Rect(4.5f * scrW, 6f * scrH, 2 * scrW, 0.5f * scrH), isWindowed, "     Windowed"))
                {
                    Screen.fullScreen = false;
                    isFullscreen = false;
                    isWindowed = true;
                }
                #endregion
                #region Resolution dropdown
                // if resolution button is pressed, show the resolution selection dropdown
                if (GUI.Button(new Rect(scrW, 4 * scrH, 2 * scrW, 0.5f * scrH), resolution, ResolutionButton))
                {
                    showDropdown = !showDropdown;
                }
                // if the Dropdown bool is true, make a scrollbar
                if (showDropdown)
                {
                    // scrollPos = scroll view
                    scrollPos = GUI.BeginScrollView(new Rect(scrW, 4.5f * scrH, 2.5f * scrW, 2f * scrH), scrollPos, new Rect(0, 0, 2 * scrW, 3.5f * scrH), false, true);
                    for (int i = 0; i < res.Length; i++) // for all the options
                    {
                        // if each resolution button is pressed, set the new resolution, disable dropdown and set the new resolution to the resolution button 
                        if (GUI.Button(new Rect(0, 0 + (0.5f * scrH) * i, 1.75f * scrW, 0.5f * scrH), res[i].x + "x" + res[i].y, ResSelectionButton))
                        {
                            Screen.SetResolution((int)res[i].x, (int)res[i].y, isFullscreen);
                            showDropdown = false;
                            resolution = res[i].x + "x" + res[i].y;
                        }
                    }
                    // End the scroll view
                    GUI.EndScrollView();
                }
                #endregion
                #region Keybinding
                #region Removing old keys
                #region Forward
                // if the other keys arent assigned
                if (!(reverse == KeyCode.None || TurnLeft == KeyCode.None || TurnRight == KeyCode.None))
                {
                    // if forward is pressed, Transfer tthe old forward key to holding key
                    if (GUI.Button(new Rect(9f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), forward.ToString(), ForwardButton))
                    {
                        holdingKey = forward;
                        forward = KeyCode.None;
                    }
                }
                else
                {
                    // the default forward key
                    GUI.Box(new Rect(9f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), forward.ToString(), ForwardString);
                }
                #endregion
                #region Reverse
                // if the other keys arent assigned
                if (!(forward == KeyCode.None || TurnLeft == KeyCode.None || TurnRight == KeyCode.None))
                {
                    // if reverse is pressed, transfer the old reverse key to holding key
                    if (GUI.Button(new Rect(12f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), reverse.ToString(), ReverseButton))
                    {
                        holdingKey = reverse;
                        reverse = KeyCode.None;
                    }
                }
                else
                {
                    // the default reverse key
                    GUI.Box(new Rect(12f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), reverse.ToString(), ReverseString);
                }
                #endregion
                #region Turn Left
                // if other keys arent assigned
                if (!(forward == KeyCode.None || reverse == KeyCode.None || TurnRight == KeyCode.None))
                {
                    // if Turn Left is pressed, transfer old TurnLeft key to holding key
                    if (GUI.Button(new Rect(9f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnLeft.ToString(), LeftButton))
                    {
                        holdingKey = TurnLeft;
                        TurnLeft = KeyCode.None;
                    }
                }
                else
                {
                    // the default Turn Left key
                    GUI.Box(new Rect(9f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnLeft.ToString(), LeftString);
                }
                #endregion
                #region Turn Right
                if (!(forward == KeyCode.None || reverse == KeyCode.None || TurnLeft == KeyCode.None))
                {
                    if (GUI.Button(new Rect(12f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnRight.ToString(), RightButton))
                    {
                        holdingKey = TurnRight;
                        TurnRight = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(12f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnRight.ToString(), RightString);
                }
                #endregion
                #endregion
                #region Setting new keys
                #region Forward
                // if forward button has got no key
                if (forward == KeyCode.None)
                {
                    // if any key is pressed, show key pressed in console
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        // if the other buttons dont have the key you want to set
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            // Set new forward key, holdingKey is back to default
                            forward = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            // Forward has no key set
                            forward = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                #endregion
                #region Reverse
                // if reverse has got no key
                if (reverse == KeyCode.None)
                {
                    // if any key is pressed, show key pressed in console
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        // if the other buttons dont have the key you want to set
                        if (!(e.keyCode == forward || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            // Set new reverse key, holdingKey is back to default
                            reverse = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            // Reverse has no key set
                            reverse = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                #endregion
                #region Turn Left
                // if TurnLeft has got no key
                if (TurnLeft == KeyCode.None)
                {
                    // if any key is pressed, show key pressed in console
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        // if the other buttons dont have the key you want to set
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            // Set new TurnLeft key, holdingKey is back to default
                            TurnLeft = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            // TurnLeft has no key set
                            TurnLeft = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                #endregion
                #region Turn Right
                // if TurnRight has no key
                if (TurnRight == KeyCode.None)
                {
                    // if any key is pressed, show key pressed in console
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        // if the other buttons dont have the key you want to set
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            // Set new TurnRight key, holdingKey is back to default
                            TurnRight = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            // TurnRight has no key set
                            TurnRight = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                #endregion
                #endregion
                #endregion
                #region Save & Return to Main Menu

                // if Apply is pressed, save the custom keybinding
                if (GUI.Button(new Rect(12f * scrW, 8.35f * scrH, 1.5f * scrW, .25f * scrH), "Apply", Apply))
                {
                    PlayerPrefs.SetString("Forward", forward.ToString());
                    PlayerPrefs.SetString("Reverse", reverse.ToString());
                    PlayerPrefs.SetString("Turn Left", TurnLeft.ToString());
                    PlayerPrefs.SetString("Turn Right", TurnRight.ToString());
                }
                // if Back is pressed, disable showOptions and return to the main menu
                if (GUI.Button(new Rect(13.5f * scrW, 8.35f * scrH, 1.5f * scrW, .25f * scrH), "Back", Back))
                {
                    showOptions = false;
                    isPaused = true;
                }
                #endregion
            }
        }
    }
}

