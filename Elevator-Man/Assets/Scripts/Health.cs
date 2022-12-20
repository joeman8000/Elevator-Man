using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public int health = 0;
    public Animator animator;
    [SerializeField] private int maxHealth = 3;
    private Rigidbody2D rb;
    public GameObject DeathParticle;
    //public GameManager gm;

    private void Start(){
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateHealth(int mod, bool fellOffMap)
    {
        health += mod;


        if(health > maxHealth)
        {
            health = maxHealth;
        } else if (health <= 0)
        {
            health = 0;
            Debug.Log("Player Die");
            Instantiate(DeathParticle, transform.position, Quaternion.identity);
            if (fellOffMap == true)
            {
                animator.SetBool("fDead", true);
            }
            else{
                animator.SetBool("Dead", true);
            }  
            rb.bodyType = RigidbodyType2D.Static;
        }

    }

    public int AccessHealth()
    {
        return health;
    }
}
