using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class randoGenerator : MonoBehaviour
{
    public GameObject NPC;
    public Sprite[] normalPeople;
    public Sprite[] oddPeople;
    public Sprite[] scaryPeople;
    public Sprite Mother;
    public Text responceBoxText;

    public GameObject gameController;
    private GameScript gameScript;
    public string item;

    private GameObject[] buttons;
    private GameObject uiCanvas;
    private UIScript uiScript;

    void Awake()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");
        uiScript = uiCanvas.GetComponent<UIScript>();

        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameScript = gameController.GetComponent<GameScript>();

        NPC = this.gameObject;

        pickRando(gameScript.hasCollected);
    }

    void pickRando(int spriteSet)
    {
        switch(spriteSet)
        {
            case 0:
                NPC.GetComponent<SpriteRenderer>().sprite = normalPeople[Random.Range(0, 7)];
                break;
            case 1:
                NPC.GetComponent<SpriteRenderer>().sprite = oddPeople[Random.Range(0, 8)];
                break;
            case 2:
                NPC.GetComponent<SpriteRenderer>().sprite = scaryPeople[Random.Range(0, 7)];
                break;
            case 3:
                NPC.GetComponent<SpriteRenderer>().sprite = Mother;
                break;
        }
    }

    public void GetItem(string temp)
    {
        item = temp;
    }
    public void OnAskPressed()
    {
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        for (int i = 0; i < 2; i++)
        {
            buttons[i].SetActive(false);
        }
    }
}
