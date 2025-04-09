using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject canvas;
    private GameObject settingsPanel;
    static public float abc = 0;

    void Start()
    {
        canvas = this.gameObject;
        settingsPanel = canvas.gameObject.transform.GetChild(1).gameObject;
        settingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(true);
    }
}
