using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float scrW = Screen.width / 16;
    public float scrH = Screen.height / 9;
    public bool loadScreen = true;
    public bool showOptions = false;

    //  public KeyCode - Controls go here

    public AudioSource mainMusic;
    public Light brightness;
    public float audioSlider, brightnessSlider, amBrightSlider;
    public bool isMute;

    public Vector2[] res;
    public int index;
    public bool showDropdown = true;
    public Vector2 scrollPos;
    public string resolution = "Resolution";
    public bool isFullscreen, isWindowed;

    // Use this for initialization
    void Start()
    {
        loadScreen = true;
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

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

        // Controls
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
            if (Input.anyKey)
            {
                Debug.Log("A key press has been detected");
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
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(2 * scrW, 0.5f * scrH, 12 * scrW, 3 * scrH), "Born To Perform");
            GUI.Box(new Rect(4 * scrW, 4.5f * scrH, 8 * scrW, 1 * scrH), " Press any Key to continue");
        }
        else
        {
            if (showOptions == false)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.Box(new Rect(2 * scrW, 0.5f * scrH, 12 * scrW, 3 * scrH), "Born To Perform");
                if (GUI.Button(new Rect(4 * scrW, 4.5f * scrH, 8 * scrW, 1 * scrH), "Play"))
                {
                    SceneManager.LoadScene(1);
                }
                if (GUI.Button(new Rect(4 * scrW, 5.5f * scrH, 8 * scrW, 1 * scrH), "Options"))
                {
                    showOptions = true;
                }
                if (GUI.Button(new Rect(4 * scrW, 6.5f * scrH, 8 * scrW, 1 * scrH), "Exit"))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
            }
            else
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.Box(new Rect(0.25f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");
                GUI.Box(new Rect(8.125f * scrW, 0.25f * scrH, 7.625f * scrW, 8.5f * scrH), "");

                GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1f * scrH), "Audio");
                audioSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 1f * scrH, 7.125f * scrW, 0.25f * scrH), audioSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 1.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(audioSlider * 100).ToString());

                GUI.Box(new Rect(0.5f * scrW, 1.5f * scrH, 7.125f * scrW, 1f * scrH), "Brightness");
                brightnessSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 2f * scrH, 7.125f * scrW, 0.25f * scrH), brightnessSlider, 0.0f, 1.0f);
                GUI.Label(new Rect(4f * scrW, 2.125f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(brightnessSlider * 100).ToString());

                GUI.Box(new Rect(0.5f * scrW, 2.5f * scrH, 7.125f * scrW, 6f * scrH), "Resolutions");

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

                if (GUI.Button(new Rect(scrW,  4 * scrH, 2 * scrW, 0.5f * scrH), resolution)) // button with the resolution label
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
            }

        }

    }
}
