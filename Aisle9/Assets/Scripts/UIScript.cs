using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private GameObject canvas;
    private GameObject interact;

    public GameObject player;
    public MovementScript moveScript;

    public GameObject gameController;
    private GameScript gameScript;

    private randoGenerator randoGenerator;
    public Text responceBoxText;
    private GameObject AisleNineObj;
    private GameObject settingsMenu;
    public Text AisleNine;
    public Text ShoppingList;
    private bool settingsOpen;
    private GameObject introScreen;
    private GameObject winPanel;
    public AudioClip[] announcements;
    public GameObject settingsObject;
    public GameObject pointLight;

    // Start is called before the first frame update
    void Awake()
    {
        // Initilizing Variables
        settingsOpen = false;

        // UI Elements
        canvas = this.gameObject;

        interact = canvas.transform.GetChild(1).gameObject;
        interact.SetActive(false);

        winPanel = this.gameObject.transform.GetChild(2).gameObject;
        winPanel.SetActive(false);

        settingsMenu = canvas.transform.GetChild(3).gameObject;
        settingsMenu.SetActive(false);

        introScreen = this.gameObject.transform.GetChild(4).gameObject;

        AisleNineObj = GameObject.FindGameObjectWithTag("AisleNine");
        AisleNineObj.SetActive(false);

        // Game Controller

        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameScript = gameController.GetComponent<GameScript>();

        // Player Elements

        player = GameObject.FindGameObjectWithTag("Player");
        moveScript = player.GetComponent<MovementScript>();

        pointLight = player.transform.GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            CloseInteractionWindow();
        }
        UpdateText();
    }

    public void LoadWinPanel()
    {
        pointLight.SetActive(false);
        winPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void IntroScreen()
    {
        introScreen.SetActive(false);
        moveScript.canMove = true;
    }

    void HandleShoppingList()
    {
        if(gameScript.hasCollected == 3)
        {
            ShoppingList.text = "Find Mum!";
        }
        else
            ShoppingList.text = gameScript.list[0] + "\n" + gameScript.list[1] + "\n" + gameScript.list[2];
    }

    public void ToggleSettingsMenu()
    {
        if (settingsOpen == false)
            settingsOpen = true;
        else if (settingsOpen == true)
            settingsOpen = false;

        canvas.transform.GetChild(3).gameObject.SetActive(settingsOpen);
    }


    private IEnumerator WaitForFiveSeconds()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(5.0f);
        AisleNineObj.SetActive(false);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void OnNewZone()
    {
        player.GetComponent<AudioSource>().clip = announcements[Random.Range(0,8)];
        player.GetComponent<AudioSource>().Play();
        Debug.Log("INTO");
        AisleNineObj.SetActive(true);
        StartCoroutine(WaitForFiveSeconds());
    }

    public void LoadInteractionWindow(GameObject enabled)
    {
        if (enabled)
        {
            if(enabled.GetComponent<SpriteRenderer>().sprite.name.Contains("spr_Mother"))
            {
                gameScript.hasWon = true;
            }
            else
            {
                moveScript.canMove = false;
                randoGenerator = enabled.GetComponent<randoGenerator>();
                interact.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = enabled.GetComponent<SpriteRenderer>().sprite;
                canvas.transform.GetChild(2).gameObject.SetActive(false);
                interact.SetActive(true);
                interact.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
                interact.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("Hit");
            }
        }
    }
    private void UpdateText()
    {
        HandleShoppingList();
        if(randoGenerator != null)
        {
            Debug.Log(randoGenerator.item);
            responceBoxText.text = "The Item In This Area Is " + randoGenerator.item;
        }
    }

    public void CloseInteractionWindow()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        interact.SetActive(false);
        moveScript.canMove = true;
    }
}
