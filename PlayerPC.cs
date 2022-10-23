using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPC : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
    private Vector2 move;
    private Vector2 contMove;
    public Joystick joystick;
    public Type type;
    public enum Type{Joystick, Buttons} 
    private bool facingRight = true;
    private Animator anim;
    public GameObject buttons;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

   public void ChangeType()
    {
        if (type == Type.Joystick)
        {
            type = Type.Buttons;
        }
        else if (type == Type.Buttons)
        {
            type = Type.Joystick;
        }
    }
   
    void Update()
    {
       
        if(type == Type.Joystick)
        {
            joystick.gameObject.SetActive(true);
            buttons.SetActive(false);
            move = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        else if(type == Type.Buttons)
        {
            joystick.gameObject.SetActive(false);
            buttons.SetActive(true);
        }

        contMove = move.normalized * speed;

        if(!facingRight && move.x > 0)
        {
            Flip();
        }
        else if(facingRight && move.x < 0)
        {
            Flip();
        }
        if(move.x == 0 && move.y == 0)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + contMove * Time.deltaTime);
    }

    public void Move(string moving)
    {
        if(type == Type.Buttons && moving == "up")
        {
            move.y = 1;
        }
        else if(type == Type.Buttons && moving == "down")
        {
            move.y = -1;
        }
        else if(type == Type.Buttons && moving == "left")
        {
            move.x = -1;
        }
        else if(type == Type.Buttons && moving == "right")
        {
            move.x = 1;
        }
        else if(type == Type.Buttons && moving == "")
        {
            move.x = 0;
            move.y = 0;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
