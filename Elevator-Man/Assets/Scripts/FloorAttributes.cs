using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAttributes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                collision.gameObject.GetComponent<Health>().UpdateHealth(-1);
                if(collision.gameObject.GetComponent<Health>().AccessHealth() != 0)
                {
                Vector2 x = new Vector2(0f,0f);
                collision.gameObject.transform.position = x;
                }
        }
    }
}
