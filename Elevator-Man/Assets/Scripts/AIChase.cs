using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float distanceBetween;
    [SerializeField] private float attackDamage = 5;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    private float distance;

    // Update is called once per frame
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if(distance < distanceBetween)
        {
        EnemyFlip(direction);
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed<=canAttack)
            {
                other.gameObject.GetComponent<Health>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else{
                canAttack +=Time.deltaTime;
            }
        }
    }

    void EnemyFlip(Vector2 direction)
    {
        if (direction.x >= 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            transform.localScale = localScale;
        }
    }

    /*public void SetEnemyTarget(GameObject newplayer)
    {
        player = newplayer;
    }*/
}
