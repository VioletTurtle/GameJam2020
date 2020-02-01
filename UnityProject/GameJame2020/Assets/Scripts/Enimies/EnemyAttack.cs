using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeBtwAttacks;
    private float startTimeBtwAttacks;
    private StaticPlayerController SPC;
    public AudioSource enemyAttackSound;
    public AudioClip attackSound;



    // Start is called before the first frame update
    void Start()
    {
        SPC = GameObject.FindGameObjectWithTag("StaticPlayerController").GetComponent<StaticPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttacks <= 0)
        {
            {
                DealDamage();

                timeBtwAttacks = startTimeBtwAttacks;
            }
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2d(Collider2D other)
    {

    }

    public void DealDamage()
    {

    }
}
