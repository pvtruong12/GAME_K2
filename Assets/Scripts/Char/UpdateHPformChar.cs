using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHPformChar : MonoBehaviour
{
    public GameObject getGameObject;
    public Message messagedata;
    private myCharz scriptMyCharz;
    void Start()
    {
        scriptMyCharz = getGameObject.GetComponent<myCharz>();
    }

    void Update()
    {

    }
}
