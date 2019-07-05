using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float force = 100;
    public float health;
    public int bossDamage = 10;
    public Slider healthBar;


    private Vector2 moveVelocity;
    private Rigidbody2D rb;

    public GameObject Aura;
    public GameObject Arma;
    private Rigidbody2D ARb;
    private Vector2 ArmaPosInicial;
    private float TimeHolding;
    public float MaxTimeHolding = 2;

    [SerializeField]
    private bool Cooldown = false;
    private bool Throwed = false;
    private bool Cancel = false;
    private bool isMoving = false;

    private float CooldownTime = 0;
    private GameOverControl gOver;
    

 
    void Start()
    {
        gOver = Camera.main.GetComponent<GameOverControl>();
        rb = GetComponent<Rigidbody2D>();
        ARb = Arma.GetComponent<Rigidbody2D>();
        ARb.isKinematic = true;
        ArmaPosInicial = Arma.transform.localPosition;
        Aura.SetActive(false);
    }

   
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        RotateChar();
        ArmaControl();

        if (moveVelocity != Vector2.zero)
        {
            isMoving = true;
            
        }
        else
            isMoving = false;

        healthBar.value = health;

        if(health <= 0)
        {
            gOver.GameOver();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }

        if (gOver.GameOverCheck)
        {
            this.gameObject.GetComponent<Player>().enabled = false;
        }



    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

    }

    void ArmaControl()
    {

        float ThrowForce;
        
        //Cooldown Control
        if (Cooldown)
        {
            if (CooldownTime < 0.2f) CooldownTime += Time.deltaTime;
            else Cooldown = false;

            Aura.SetActive(false);

        }
        else CooldownTime = 0;
        //
        
        //Carregar Disparo
        if(Input.GetKey(KeyCode.Mouse0) && !Throwed && !Cancel && !Cooldown)
        {
            if (TimeHolding < MaxTimeHolding) TimeHolding += Time.deltaTime  * 2;
            else TimeHolding = MaxTimeHolding;

            if (Input.GetKeyDown(KeyCode.Mouse1)) Cancel = true;

            Arma.transform.localPosition = new Vector2(Mathf.Lerp(ArmaPosInicial.x, -ArmaPosInicial.x, TimeHolding/MaxTimeHolding),ArmaPosInicial.y);

        }
        //
        //Disparar
        if(Input.GetKeyUp(KeyCode.Mouse0) | Input.GetKeyUp(KeyCode.Mouse1))
        {
            Aura.SetActive(false);

            if (!Cancel && !Throwed && !Cooldown)
            {
                ARb.isKinematic = false;
                ThrowForce = TimeHolding * force;
                Debug.Log("Atirou");
                
                Arma.transform.parent = null;
                Debug.Log(Arma.transform.right.normalized * ThrowForce);
                ARb.AddForce(Arma.transform.right.normalized * ThrowForce);
                Throwed = true;


            }

            if (Cancel && !Throwed)
            {
                Arma.transform.localPosition = ArmaPosInicial;
                Cooldown = true;

            }

        }
        //
        //Puxar de volta
        if (Input.GetKey(KeyCode.LeftShift) && Throwed && !Cancel && !Cooldown) // && !isMoving
        {
            Aura.SetActive(true);

            if (TimeHolding < MaxTimeHolding) TimeHolding += Time.deltaTime;
            else TimeHolding = MaxTimeHolding;

            if (Input.GetKeyDown(KeyCode.Mouse1)) Cancel = true;

            Vector3 VecDir = transform.position - Arma.transform.position;
            VecDir = VecDir.normalized;
            float dir = Vector2.SignedAngle(new Vector2(-1, 0), VecDir);
            Arma.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, dir));
            ARb.isKinematic = false;
            ARb.AddForce(VecDir * TimeHolding * 1.5f);

        }


    }



    void RotateChar() //Rotacionar a Arma
    {

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 VecDir = transform.position - mouseWorld;
        VecDir = VecDir.normalized;
        float dir = Vector2.SignedAngle(new Vector2(-1,0), VecDir);
       
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, dir));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arma")
        {
            ARb.isKinematic = true;
            Arma.transform.rotation = Quaternion.identity;
            Arma.transform.parent = this.transform;
            Arma.transform.localPosition = ArmaPosInicial;
            Arma.transform.localRotation = Quaternion.identity;
            Throwed = false;
            TimeHolding = 0;
            Cooldown = true;
        }

        if (collision.tag == "Inimigo")
        {
                health -= bossDamage;
            Debug.Log(health);

        }


        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            health -= bossDamage * Time.deltaTime;
           // Debug.Log(health);

        }
    }

}

