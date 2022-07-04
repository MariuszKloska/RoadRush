using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject menu;
    public Toggle isFillScreen;
    public TMP_Dropdown resolution;
    public Slider volume;


    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolution.options.Clear(); 

        for(int i = 0; i < resolutions.Length; i++)
        {
            resolution.options.Add(new TMP_Dropdown.OptionData(ResToString(resolutions[i])));
            resolution.options[i].text = ResToString(resolutions[i]);
            if (ResToString(resolutions[i]) == (Screen.width + "x" + Screen.height))
            {
                resolution.value = i;
            }
        }
        resolution.RefreshShownValue();

        isFillScreen.isOn = Screen.fullScreen;
        volume.value = AudioListener.volume;

        volume.onValueChanged.AddListener(delegate {OnVolumeValueChange(); });
        isFillScreen.onValueChanged.AddListener(delegate {FullScreenValueChange(); });
        resolution.onValueChanged.AddListener(delegate {OnResolutionValueChange(); });
    }

    public void FullScreenValueChange()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OnVolumeValueChange()
    {
        AudioListener.volume = volume.value;
    }

    public void OnResolutionValueChange()
    {
        Screen.SetResolution(int.Parse(StringToRes(resolution.options[resolution.value].text)[0]), int.Parse(StringToRes(resolution.options[resolution.value].text)[1]),isFillScreen.isOn);
    }
    public void OnButtonBack()
    {
        this.gameObject.SetActive(false);
        menu.SetActive(true);
    }

    string[] StringToRes(string res)
    {
        return res.Split('x');
    }

    string ResToString(Resolution res)
    {
        return res.width + "x" + res.height;
    }
}
