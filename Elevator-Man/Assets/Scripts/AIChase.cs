using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private float enemyhealth = 1f;
    private GameObject player;
    public float speed;
    public float distanceBetween;
    [SerializeField] private float attackDamage = 5;
    [SerializeField] private float attackSpeed = 1f;
    private static float canAttack;
    private float distance;
    //private Color c;
    //private Renderer rend;
    private GameObject ECountObj;

    // Update is called once per frame
    void Awake()
    {
        player = GameObject.Find("Player");
        ECountObj = GameObject.Find("Enemies");
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

        if(canAttack < 40)
        {
            canAttack += Time.deltaTime;
        }

        if(enemyhealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed<=canAttack)
            {
                collision.gameObject.GetComponent<Health>().UpdateHealth(-attackDamage);
                //StartCoroutine("GetInvulernable");
                canAttack = 0f;
            }
        }

        if(collision.gameObject.tag == "Bullet")
        {
            enemyhealth -= player.GetComponent<PlayerMovement>().bulletDamage;
            Destroy(collision.gameObject);
        }
    }

    /*IEnumerator GetInvulnerable()
    {

        Physics2D.IgnoreLayerCollision(8, 9, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(8,9,false);
        c.a = 1f;
        rend.material.color = c;
    }*/

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
