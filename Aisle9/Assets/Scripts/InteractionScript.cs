using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    private GameObject player;
    private MovementScript moveScript;
    private GameObject controller;
    private GameScript gameScript;
    private GameObject interObject;
    private GameObject uiCanvas;
    private UIScript uiScript;

    public GameObject NPC;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");
        uiScript = uiCanvas.GetComponent<UIScript>();

        controller = GameObject.FindGameObjectWithTag("GameController");
        gameScript = controller.GetComponent<GameScript>();

        player = this.gameObject;
        moveScript = player.GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            uiScript.LoadInteractionWindow(CheckInteraction("Adult"));
            gameScript.PickedUpItem(CheckInteraction("Item"));
        }
    }

    GameObject CheckInteraction(string maskName)
    {
        Vector2 currDir = moveScript.getDirection(); // Add the movement script
        LayerMask mask = LayerMask.GetMask(maskName);
        if (currDir != new Vector2(0f, 0f))
        {
            RaycastHit2D rCast = Physics2D.Raycast(player.transform.position, currDir, 3f, mask);
            Debug.DrawRay(player.transform.position, currDir * 2, Color.green, 5f);

            if (rCast.collider != null)
            {
                interObject = rCast.collider.gameObject;
                NPC = interObject;
                return interObject;
            }
            else
            {
                Debug.Log("Hit Nothing");                    
                return null;
            }

        }
        else
            return null;
    }
}
