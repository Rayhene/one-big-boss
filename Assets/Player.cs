using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    public GameObject Arma;

 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        RotateChar();


    }

     void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }

    void RotateChar() //Rotacionar a Arma
    {

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 VecDir = transform.position - mouseWorld;
        VecDir = VecDir.normalized;
        float dir = Vector2.SignedAngle(new Vector2(-1,0), VecDir);
        Debug.Log("dir " + dir);
        Debug.Log("dir " + VecDir);

        //  float dir = Mathf.Atan2(mouseWorld.y, mouseWorld.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, dir));

    }

    }

