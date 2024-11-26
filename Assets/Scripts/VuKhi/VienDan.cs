using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienDan : MonoBehaviour
{
    public GameObject getGameObject;
    private Rigidbody2D rb;
    public MessageVuKhi msg;
    public float tocdo = 2f;
    void Start()
    {
        Destroy(gameObject, 10f);
        rb = GetComponent<Rigidbody2D>();
        bool flag = getGameObject.transform.localScale.x > 0;
        if (getGameObject == null)
            Debug.Log("isGaneObject is null");
        else
        {
            Debug.Log("Scale " + getGameObject.transform.localScale.x);
        }    
        tocdo = tocdo * (flag ? 1:-1);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(tocdo, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("vatcan") || collision.CompareTag("Enemy"))
        {
            if(collision.CompareTag("Enemy"))
            {
                MobController mob = collision.transform.parent.GetComponent<MobController>();
                if (mob == null)
                    Debug.Log("mob is null");
                mob.TakeDame(msg.dame);
                if (mob.hp <= 0)
                    mob.isDie();
            }
            Destroy(gameObject);
        }
    }
}
