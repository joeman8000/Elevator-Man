using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [HideInInspector] public int health = 0;
    public Animator animator;
    public int maxHealth = 3;
    private Rigidbody2D rb;
    public GameObject DeathParticle;
    public AudioSource hitPlayer;
    //public GameManager gm;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Start(){
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for(int i = 0; i <hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxHealth){
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public void UpdateHealth(int mod, bool fellOffMap)
    {
        health += mod;


        if(health > maxHealth)
        {
            health = maxHealth;
            hitPlayer.Play();
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
