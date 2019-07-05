using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public enum StateType
    {

        Chase1, Chase2, Idle

    }

    public int health;
    public int playerDamage = 3;
    public float speed;

    private Transform player;

    public GameObject[] projectile;

    public float idleTime;

    private float timeBtwShots;
    public float startTimeBtwShots;



    public Slider healthBar;

    FiniteStateMachine finiteStateMachine;


    private float lastShoot;
    [SerializeField] private float shootCooldown;


    [SerializeField] private StateType myState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

        finiteStateMachine = new FiniteStateMachine();

        lastShoot = -shootCooldown;
        
    }


    void Update()
    {
        
        if (finiteStateMachine != null && finiteStateMachine.currentlyRunningState == null)
        {

            int r = Random.Range(0, 3);

            myState = (StateType)r;

            State nextState = null;

            switch (myState)
            {
                case StateType.Chase1:
                    nextState = new ChasePlayer(this, player, speed, projectile[0]);
                    break;
                case StateType.Chase2:
                    nextState = new ChasePlayer2(this, player, speed, projectile[1]);
                    break;
                case StateType.Idle:
                    nextState = new IdleState(idleTime);
                    break;
                default:
                    break;
            }
            finiteStateMachine.ReciveState(nextState);




        }

        finiteStateMachine.ExecuteState();

        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        //if (timeBtwShots <= 0)
        //{
        //    Instantiate(projectile, transform.position, Quaternion.identity);
        //    timeBtwShots = startTimeBtwShots;
        //}
        //else
        //{
        //    timeBtwShots -= Time.deltaTime;
        //}

        healthBar.value = health;
    }

    public bool Shoot(GameObject prefab, Vector3 dir)
    {
        if (Time.time > lastShoot + shootCooldown)
        {
            Projectile projectile = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Projectile>();

            projectile.Inicialization(dir);

            lastShoot = Time.time;
            return true;
        }
        return false;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arma")
        {
            health -= playerDamage;

        }


    }



}
