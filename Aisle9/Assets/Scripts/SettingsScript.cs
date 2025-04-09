using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    public static float Volume;
    private GameObject settingsObject;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        settingsObject = this.gameObject;
        Volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(settingsObject);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 10);
    }
}
