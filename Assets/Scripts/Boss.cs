using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;

    public float speed;

    private Transform player;

    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Slider healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }else
        {
            timeBtwShots -= Time.deltaTime;
        }

        healthBar.value = health;
    }
}
