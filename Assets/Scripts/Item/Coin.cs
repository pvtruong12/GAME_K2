using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public int maxNhat = 5;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CharChild"))
        {
            myCharz mychar = collision.gameObject.GetComponentInParent<myCharz>();
            mychar.xu += 1;
            mychar.coinNumber.text = "Score: " + mychar.xu;
            mychar.audioPick.Play();
            Destroy(gameObject);
            if(mychar.xu >= maxNhat)
            {
                Time.timeScale = 0;
                mychar.LogoWinlose.text = "You Win";
                mychar.getPanelGameScr.SetActive(true);
            }
        }
    }
}
