using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    [SerializeField] Animator animation;
    int direction;
    

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

        /*
        //Move North
        if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            direction = 1;
        }
        //Move North-West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            //direction = 2;
        }
        //Move West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            direction = 2;
        }
        //Move South-West
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            //direction = 4;
        }
        //Move South
        else if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            direction = 3;
        }
        //Move South-East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            //direction = 6;
        }
        //Move East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") == 0.0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            direction = 4;
        }
        //Move North-East
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed, 0f));
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0f, 0f));
            //direction = 8;
        } else
        {
            direction = 0;
        }
        */
    }

    void Animate()
    {
        animation.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animation.SetFloat("Vertical", Input.GetAxis("Vetical"));
    }
}
