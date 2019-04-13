using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    private new Animator animation;
    private Vector2 direction;
    
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
        animation.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animation.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    public Vector2 getDirection()
    {
        //Move North
        if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move North-West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move South-West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move South
        else if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move South-East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        //Move North-East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            direction = new Vector2(0, 1);
        }
        else
        {
            direction = new Vector2(0, 1);
        }

        return direction;
    }
}
