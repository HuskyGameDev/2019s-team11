using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    private new Animator animation;
    private Vector2 direction;
    Vector2 relativePosition;
    bool isWalking = false;


    void Start()
    {
        animation = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    // Called once per physics step
    private void FixedUpdate()
    {
        move();
    }

    void move()
    {
        Vector3 movment = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        transform.position = transform.position + movment * MoveSpeed;
    }

    void Animate()
    {
        //Vector2 relativePosition = new Vector2(GetComponent<Transform>().position.x - Input.mousePosition.x, GetComponent<Transform>().position.y - Input.mousePosition.y);
        animation.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animation.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            animation.SetBool("isWalking", false);
            animation.speed = 0;
        } else
        {
            animation.SetBool("isWalking", true);
            animation.speed = 1;
        }

    }

    public Vector2 getDirection()
    {
        //Move North
        if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            direction = Vector2.up;
        }

        //Move West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            direction = Vector2.left;
        }

        //Move South
        else if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            direction = Vector2.down;
        }

        //Move East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            direction = Vector2.right;
        }

        return direction;
    }
}
