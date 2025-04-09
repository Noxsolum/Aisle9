using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    private GameObject controller;
    private MapController map;
    private ItemSpawner iSpawner;
    private GameObject NPC;
    private GameObject uiCanvas;
    private UIScript uiScript;
    public bool hasWon;
    private string[] items;
    public string[] list;
    public int hasCollected;
    private bool gameOver;
    public AudioClip winClip;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");
        uiScript = uiCanvas.GetComponent<UIScript>();

        gameOver = false;
        hasWon = false;
        controller = this.gameObject;
        iSpawner = controller.GetComponent<ItemSpawner>();
        map = controller.GetComponent<MapController>();

        list = new string[3];
        items = new string[5];
        items[0] = "Apple";
        items[1] = "Cereal";
        items[2] = "OrangeJuice";
        items[3] = "TeddyBear";
        items[4] = "Milk";

        CreateShoppingList();
    }

    // Update is called once per frame
    void Update()
    {
        WinGame();
        LoseGame();
    }

    void WinGame()
    {
        if (hasWon == true)
        {
            uiScript.settingsObject.GetComponent<AudioSource>().clip = winClip;
            uiScript.LoadWinPanel();
            Debug.Log("You Win!");
        }
    }

    private void LoseGame()
    {
        if(gameOver == true)
        {

        }
    }

    void CreateShoppingList()
    {
        hasCollected = 0;
        for(int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 5);
            list[i] = items[j];
            Debug.Log(list[i]);
        }
    }

    public void PickedUpItem(GameObject hasPicked)
    {
        bool isOnList = false;
        if (hasPicked)
        {
            for(int i = 0; i < 3; ++i)
            {
                Debug.Log(hasPicked.name);
                if(hasPicked.name.Contains(list[i]))
                {
                    isOnList = true;
                    list[i] = "";
                    break;
                }
            }

            if(isOnList)
            {
                hasCollected++;
                Object.Destroy(hasPicked);
            }
            else
            {
                Debug.Log("This is not on your list");
            }
        }
    }

    public void SpawnNPC(GameObject obj)
    {
        int x = Random.Range(0, 4);
        Vector2 possibleSpawn = obj.transform.GetChild(x).gameObject.transform.position;

        GameObject person = (GameObject)Instantiate(Resources.Load("prefab_NPC"), possibleSpawn, new Quaternion(0, 0, 0, 0), obj.transform);
        if(x >= 3)
            SpawnItem(obj, person, obj.transform.GetChild(x - 1).gameObject.transform.position);
        else
            SpawnItem(obj, person, obj.transform.GetChild(x + 1).gameObject.transform.position);

    }

    public void SpawnItem(GameObject parent, GameObject obj, Vector2 spawn)
    {
        switch(Random.Range(0,5))
        {
            case 0:
                Instantiate(Resources.Load("Milk"), spawn, new Quaternion(0, 0, 0, 0), parent.transform);
                obj.GetComponent<randoGenerator>().GetItem("Milk");
                break;
            case 1:
                Instantiate(Resources.Load("OrangeJuice"), spawn, new Quaternion(0, 0, 0, 0), parent.transform);
                obj.GetComponent<randoGenerator>().GetItem("Orange Juice");
                break;
            case 2:
                Instantiate(Resources.Load("Cereal"), spawn, new Quaternion(0, 0, 0, 0), parent.transform);
                obj.GetComponent<randoGenerator>().GetItem("Cereal");
                break;
            case 3:
                Instantiate(Resources.Load("TeddyBear"), spawn, new Quaternion(0, 0, 0, 0), parent.transform);
                obj.GetComponent<randoGenerator>().GetItem("A Teddy Bear");
                break;
            case 4:
                Instantiate(Resources.Load("Apple"), spawn, new Quaternion(0, 0, 0, 0), parent.transform);
                obj.GetComponent<randoGenerator>().GetItem("An Apple");
                break;
        }
    }
}
