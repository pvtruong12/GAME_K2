using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienDan : MonoBehaviour
{
    public GameObject getGameObject;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    public float tocdo = 2f;
    void Start()
    {
        Destroy(gameObject, 10f);
        rb = GetComponent<Rigidbody2D>();
        if (getGameObject != null)
        {
            sp = getGameObject.GetComponent<SpriteRenderer>();
        }
        tocdo = tocdo * (sp.flipX ? -1 : 1);
    }

    void FixedUpdate()
    {
        if (sp != null)
        {
            rb.velocity = new Vector3(tocdo, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("vatcan") || collision.CompareTag("Enemy"))
        {
            if(collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<MobController>().isDie(); 
            }
            Destroy(gameObject);
        }
    }
}
