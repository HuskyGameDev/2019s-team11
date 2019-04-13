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
        animate();
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

    void animate()
    {
        animation.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animation.SetFloat("Vertical", Input.GetAxis("Vetical"));
    }
}
