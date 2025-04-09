using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private GameObject gridParent;
    private GameObject[] tileMaps; 
    private GameObject[] tileMapsTR;
    private GameObject[] tileMapsTL;
    private GameObject[] tileMapsBR;
    private GameObject[] tileMapsBL;
    private Vector2 currPos;
    private Vector2 currMiddle;
    private GameObject player;
    private GameObject toDelete;
    private GameScript gameScript;
    private GameObject newMap;

    private GameObject uiCanvas;
    private UIScript uiScript;

    private void Start()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");
        uiScript = uiCanvas.GetComponent<UIScript>();

        gameScript = this.gameObject.GetComponent<GameScript>();
        gridParent = GameObject.FindGameObjectWithTag("GridParent");
        currMiddle = new Vector2(-14, 14);
        LoadLevelOne(currMiddle);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        currPos = player.transform.position;
        updateCurrTile();
    }

    public void ResetLevel()
    {
        // Fade to black


        // Reset Level
        tileMaps = GameObject.FindGameObjectsWithTag("Tile");
        player.transform.position = new Vector2(0f, 0f);
        currMiddle = new Vector2(-14, 14);
        LoadLevelOne(currMiddle);

        // Fade in again

    }

    void LoadLevelOne(Vector2 spawn)
    {
        // Purely loading the level
        Vector2 spawn1 = spawn + new Vector2(32f, 0f);
        Vector2 spawn2 = spawn + new Vector2(-32f, 0f);
        Instantiate(Resources.Load("Tilemap_0_TLTR"), spawn, new Quaternion(0, 0, 0, 0), gridParent.transform);
        Instantiate(Resources.Load("Tilemap_Floor"), spawn, new Quaternion(0, 0, 0, 0), gridParent.transform);
        Instantiate(Resources.Load("Tilemap_1_TLBR"), spawn1, new Quaternion(0, 0, 0, 0), gridParent.transform);
        Instantiate(Resources.Load("Tilemap_Floor"), spawn1, new Quaternion(0, 0, 0, 0), gridParent.transform);
        Instantiate(Resources.Load("Tilemap_4_TRBR"), spawn2, new Quaternion(0, 0, 0, 0), gridParent.transform);
        Instantiate(Resources.Load("Tilemap_Floor"), spawn2, new Quaternion(0, 0, 0, 0), gridParent.transform);
    }

    int CheckPos()
    {
        // Left == 1
        // Right == 2

        if (currPos.x > ((currMiddle.x + 14f) + 16f))
        {
            return 1;
        }
        else if (currPos.x < ((currMiddle.x + 14f) - 16f))
        {
            return 2;
        }
        else
            return 0;
    }

    void updateCurrTile()
    {
        // Check Position
        int abc = CheckPos();
        switch (abc)
        {
            case 0:
                Debug.Log("In the Same Tile");
                break;
            case 1:
                Debug.Log("Moved to the Left");
                deleteAndReplace(abc);
                uiScript.OnNewZone();
                break;
            case 2:
                Debug.Log("Moved to the Right");
                uiScript.OnNewZone();
                deleteAndReplace(abc);
                break;
            default:
                Debug.Log("What are you doing silly, going some weird ass way");
                break;
        }
        // Check which tile you're in

        // Update the current Tile

        // Call deleteAndReplace()
    }

    void deleteAndReplace(int abc)
    {
        tileMaps = GameObject.FindGameObjectsWithTag("Tile");
        if(abc == 1)
        {
            for(int i = 0; i < tileMaps.Length; ++i)
            {
                if (tileMaps[i].transform.position.x == currMiddle.x - 32f)
                {
                    Object.Destroy(tileMaps[i]);
                }
            }
            currMiddle.x = currMiddle.x + 32f;
            randomSpawn(32f);
        }
        else if (abc == 2)
        {
            for (int i = 0; i < tileMaps.Length; ++i)
            {
                if (tileMaps[i].transform.position.x == currMiddle.x + 32f)
                {
                    Object.Destroy(tileMaps[i]);
                }
            }
            currMiddle.x = currMiddle.x - 32f;
            randomSpawn(-32f);
        }
    }

    void randomSpawn(float x)
    {
        Instantiate(Resources.Load("Tilemap_Floor"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
        switch (Random.Range(0,10))
        {
            case 0:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_0_TLTR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 1:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_1_TLBR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 2:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_2_TLBR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 3:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_3_TL"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 4:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_4_TRBR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 5:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_5_TRBL"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 6:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_6_TR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 7:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_7_BRBL"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 8:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_8_BR"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            case 9:
                newMap = (GameObject)Instantiate(Resources.Load("Tilemap_9_BL"), new Vector2(currMiddle.x + x, currMiddle.y), new Quaternion(0, 0, 0, 0), gridParent.transform);
                gameScript.SpawnNPC(newMap);
                break;
            default:

                break;
        }
    }
}