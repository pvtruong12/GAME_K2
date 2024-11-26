using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiem : MonoBehaviour
{
    public MessageVuKhi msg;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            MobController mob = collision.transform.parent.GetComponent<MobController>();
            if (mob == null)
                Debug.Log("mob is null");
            mob.TakeDame(msg.dame);
            if (mob.hp <= 0)
                mob.isDie();
        }
    }
}
