using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    private Transform player;
    private Vector2 target;
    private Rigidbody2D rgd;
    Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    public void Inicialization(Vector2 dir)
    {
        this.direction = dir;
        rgd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        // Bala perseguidora
        // direction = (player.transform.position - transform.position).normalized;



        rgd.velocity = direction * speed;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            DestroyProjectile();
        //if (transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    DestroyProjectile();
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
