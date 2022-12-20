using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public int health = 0;
    public Animator animator;
    [SerializeField] private int maxHealth = 3;
    //public GameManager gm;

    private void Start(){
        health = maxHealth;
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
            if (fellOffMap == true)
            {
                animator.SetBool("fDead", true);
            }
            else{
                animator.SetBool("Dead", true);
            }  
        }

    }

    public int AccessHealth()
    {
        return health;
    }
}
