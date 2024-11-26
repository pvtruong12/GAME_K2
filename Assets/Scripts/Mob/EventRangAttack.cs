using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRangAttack : MonoBehaviour
{
    private GameObject getGameObjetc;
    private GameObject getCharGameObj;
    private MobController scriptMob;
    private myCharz scriptChar;
    private long LasttimeCooldown;
    void Start()
    {
        getGameObjetc = GameObject.Find("Mobs");
        getCharGameObj = GameObject.Find("Char");
        scriptMob = getGameObjetc.GetComponent<MobController>();
        scriptChar = getCharGameObj.GetComponent<myCharz>();
    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    return;
    //    if (collision.CompareTag("CharChild") && !scriptMob.isMovetoLocal && !scriptChar.StartDie())
    //    {
    //        scriptMob.isMovetoChar = true;
    //    } 
    //}
}
