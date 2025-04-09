using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Vector2 dir;
    public int speed;
    private GameObject player;
    private bool isRunningAway;
    private Animator animator;
    public bool canMove;
    private char lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        player = this.gameObject;
        dir = new Vector2(0, 0);
        speed = 10;
        isRunningAway = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove == true)
        {
            movementKeyboard();

            sprinting();
        }
    }

    void sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                speed = 20;
                animator.SetBool("IsIdle", false);
            }
        }
        else if (isRunningAway == true)
        {
            speed = 30;
            animator.SetBool("IsIdle", false);
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            speed = 0;
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", false);
            animator.SetBool("IsIdle", true);
        }
        else
        {
            speed = 10;
            animator.SetBool("IsIdle", false);
        }
    }

    IEnumerator WaitForFiveSeconds()
    {
        yield return new WaitForSeconds(5.0f);
    }

    public void RunningAway()
    {
        Debug.Log("Running Way");
        isRunningAway = true;
        StartCoroutine(WaitForFiveSeconds());
        RevertSpeed();
    }

    void RevertSpeed()
    {
        isRunningAway = false;
        speed = 10;
    }



    void movementKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("MoveUp", true);
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", false);
            dir = new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveLeft", true);
            animator.SetBool("MoveRight", false);
            dir = new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", true);
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", false);
            dir = new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", true);
            dir = new Vector2(1, 0);
        }

        // =====================
        // Applying the Movement
        // =====================

        player.GetComponent<Rigidbody2D>().AddForce(dir * speed);

        // =============
        // Stop Movement
        // =============


    }

    public Vector2 getDirection()
    {
        return dir;
    }
}
