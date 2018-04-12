using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Screen Size and load and options bools
    public float scrW = Screen.width / 16;
    public float scrH = Screen.height / 9;
    public bool loadScreen = true;
    public bool showOptions = false;
    #endregion

    #region Keybinding
    public KeyCode forward;
    public KeyCode reverse;
    public KeyCode TurnLeft;
    public KeyCode TurnRight;
    public KeyCode holdingKey;
    #endregion

    #region Audio and Brightness
    public AudioSource mainMusic;
    public Light brightness;
    public float audioSlider, brightnessSlider, amBrightSlider;
    public bool isMute;
    #endregion

    #region Resolutions 
    public Vector2[] res;
    public int index;
    public bool showDropdown = true;
    public Vector2 scrollPos;
    public string resolution = "Resolution";
    public bool isFullscreen, isWindowed;
    #endregion

    // Use this for initialization
    void Start()
    {
        #region Screen References
        loadScreen = true;
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        #endregion

        #region Audio and Brightness References
        brightness = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Light>();
        mainMusic = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Mute"))
        {
            RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("AmLight");
            brightness.intensity = PlayerPrefs.GetFloat("DirLight");
            brightnessSlider = brightness.intensity;
            amBrightSlider = RenderSettings.ambientIntensity;
            audioSlider = PlayerPrefs.GetFloat("MainMusicVolume");

            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                isMute = true;
                mainMusic.volume = 0;
            }
            else
            {
                isMute = false;
                audioSlider = mainMusic.volume;
            }
        }
        else
        {
            audioSlider = mainMusic.volume;
            brightnessSlider = brightness.intensity;
            amBrightSlider = RenderSettings.ambientIntensity;
        }
        #endregion

        #region Keybinding References
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "UpArrow"));
        reverse = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Reverse", "DownArrow"));
        TurnLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Turn Left", "LeftArrow"));
        TurnRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Turn Right", "RightArrow"));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width / 16 != scrW || Screen.height / 9 != scrH)
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
        }
        if (loadScreen == true)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("A mouse click has been detected");
                loadScreen = false;
            }
        }
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
    }

    public void OnGUI()
    {
        if (loadScreen == true)
        {
            #region Background, Logo and Any Key to continue
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(2 * scrW, 2f * scrH, 12 * scrW, 2 * scrH), "Born To Perform");
            GUI.Box(new Rect(4 * scrW, 6.5f * scrH, 8 * scrW, 1 * scrH), "Click To Continue");
            #endregion
        }
        else
        {
            if (showOptions == false)
            {
                #region Background and Logo
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.Box(new Rect(2 * scrW, 2f * scrH, 12 * scrW, 2 * scrH), "Born To Perform");
                #endregion
                #region Play
                if (GUI.Button(new Rect(4 * scrW, 4.5f * scrH, 8 * scrW, 1 * scrH), "Play"))
                {
                    SceneManager.LoadScene(1);
                }
                #endregion
                #region Options
                if (GUI.Button(new Rect(4 * scrW, 5.5f * scrH, 8 * scrW, 1 * scrH), "Options"))
                {
                    showOptions = true;
                }
                #endregion
                #region Quit
                if (GUI.Button(new Rect(4 * scrW, 6.5f * scrH, 8 * scrW, 1 * scrH), "Exit"))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
                #endregion
            }
            if (showOptions == true)
            {
                #region Backgrounds
                GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                GUI.Box(new Rect(8.125f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                #endregion
                #region Headings
                GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1f * scrH), "Audio");
                audioSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 1f * scrH, 7.125f * scrW, 0.25f * scrH), audioSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 1.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(audioSlider * 100).ToString());

                GUI.Box(new Rect(0.5f * scrW, 1.5f * scrH, 7.125f * scrW, 1f * scrH), "Brightness");
                brightnessSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 2f * scrH, 7.125f * scrW, 0.25f * scrH), brightnessSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 2.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(brightnessSlider * 100).ToString());

                GUI.Box(new Rect(0.5f * scrW, 2.5f * scrH, 7.125f * scrW, 6f * scrH), "Resolutions");

                GUI.Box(new Rect(8.4f * scrW, 0.25f * scrH, 7.125f * scrW, 8f * scrH), "Controls");
                GUI.Box(new Rect(9.75f * scrW, 1f * scrH, 1f * scrW, .5f * scrH), "Forward");
                GUI.Box(new Rect(12.75f * scrW, 1f * scrH, 1f * scrW, .5f * scrH), "Reverse");
                GUI.Box(new Rect(9.75f * scrW, 2f * scrH, 1f * scrW, .5f * scrH), "Turn Left");
                GUI.Box(new Rect(12.75f * scrW, 2f * scrH, 1f * scrW, .5f * scrH), "Turn Right");
                Event e = Event.current;
                #endregion
                #region Fullscreen and Windowed Toggles
                if (GUI.Toggle(new Rect(5.5f * scrW, 4.5f * scrH, 2 * scrW, 0.5f * scrH), isFullscreen, "Fullscreen"))
                {
                    Screen.fullScreen = true;
                    isFullscreen = true;
                    isWindowed = false;
                }
                if (GUI.Toggle(new Rect(5.5f * scrW, 6f * scrH, 2 * scrW, 0.5f * scrH), isWindowed, "Windowed"))
                {
                    Screen.fullScreen = false;
                    isFullscreen = false;
                    isWindowed = true;
                }
                #endregion
                #region Resolution dropdown
                if (GUI.Button(new Rect(scrW, 4 * scrH, 2 * scrW, 0.5f * scrH), resolution)) // button with the resolution label
                {
                    showDropdown = !showDropdown; //toggle our resolution drop down on or off
                }
                if (showDropdown) // if the drop down is on
                {
                    // our scroll position is equal to our position in our scroll view
                    // start of GUI elements in scroll view
                    scrollPos = GUI.BeginScrollView(new Rect(scrW, 4.5f * scrH, 2.5f * scrW, 2f * scrH), scrollPos, new Rect(0, 0, 2 * scrW, 3.5f * scrH), false, true);
                    for (int i = 0; i < res.Length; i++) // for all the options
                    {
                        // create a button in the top corner of our scroll view thats labeled the resolution
                        if (GUI.Button(new Rect(0, 0 + (0.5f * scrH) * i, 1.75f * scrW, 0.5f * scrH), res[i].x + "x" + res[i].y))
                        {
                            // when pressed set resolution to our current selected index resolution values
                            Screen.SetResolution((int)res[i].x, (int)res[i].y, isFullscreen);
                            // hide dropdown
                            showDropdown = false;
                            // set drop down label to the selected resolution
                            resolution = res[i].x + "x" + res[i].y;
                        }
                    }
                    // end of GUI elements inside scroll view
                    GUI.EndScrollView();
                }
                #endregion
                #region Keybinding
                // FORWARD
                if (!(reverse == KeyCode.None || TurnLeft == KeyCode.None || TurnRight == KeyCode.None))
                {
                    if (GUI.Button(new Rect(9f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), forward.ToString()))
                    {
                        holdingKey = forward;
                        forward = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(9f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), forward.ToString());
                }
                // REVERSE
                if (!(forward == KeyCode.None || TurnLeft == KeyCode.None || TurnRight == KeyCode.None))
                {
                    if (GUI.Button(new Rect(12f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), reverse.ToString()))
                    {
                        holdingKey = reverse;
                        reverse = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(12f * scrW, 1.5f * scrH, 2.5f * scrW, .5f * scrH), reverse.ToString());
                }
                // TurnLeft
                if (!(forward == KeyCode.None || reverse == KeyCode.None || TurnRight == KeyCode.None))
                {
                    if (GUI.Button(new Rect(9f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnLeft.ToString()))
                    {
                        holdingKey = TurnLeft;
                        TurnLeft = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(9f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnLeft.ToString());
                }
                // TurnRight
                if (!(forward == KeyCode.None || reverse == KeyCode.None || TurnLeft == KeyCode.None))
                {
                    if (GUI.Button(new Rect(12f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnRight.ToString()))
                    {
                        holdingKey = TurnRight;
                        TurnRight = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(12f * scrW, 2.5f * scrH, 2.5f * scrW, .5f * scrH), TurnRight.ToString());
                }

                if (forward == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            forward = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            forward = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }

                if (reverse == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        if (!(e.keyCode == forward || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            reverse = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            reverse = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                if (TurnLeft == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            TurnLeft = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            TurnLeft = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }

                if (TurnRight == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);
                        if (!(e.keyCode == reverse || e.keyCode == TurnLeft || e.keyCode == TurnRight))
                        {
                            TurnRight = e.keyCode;
                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            TurnRight = holdingKey;
                            holdingKey = KeyCode.None;
                        }

                    }
                }
                #endregion

                #region Save & Return to Main Menu

                if (GUI.Button(new Rect(12f * scrW, 8.35f * scrH, 1.5f * scrW, .25f * scrH), "Apply"))
                {
                    PlayerPrefs.SetString("Forward", forward.ToString());
                    PlayerPrefs.SetString("Reverse", reverse.ToString());
                    PlayerPrefs.SetString("Turn Left", TurnLeft.ToString());
                    PlayerPrefs.SetString("Turn Right", TurnRight.ToString());
                }
                if (GUI.Button(new Rect(14f * scrW, 8.35f * scrH, 1.5f * scrW, .25f * scrH), "Back"))
                {
                    showOptions = false;
                }
                #endregion
            }
        }
    }
}
