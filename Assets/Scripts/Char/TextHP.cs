using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHP : MonoBehaviour
{
    public Message getDataChar;
    public GameObject getGameObject;
    public myCharz scrriptmhChar;
    private Vector3 offset;
    private TMP_Text textpro;
    void Start()
    {
        textpro = GetComponentInChildren<TMP_Text>();
        offset = textpro.transform.position - getGameObject.transform.position;
        scrriptmhChar = getGameObject.GetComponent<myCharz>();
    }
    void LateUpdate()
    {
        string text = "HP: "+ scrriptmhChar.cHP + "/" + getDataChar.cHPMax;
        textpro.text = $"<color=red>{text}";
        textpro.transform.position = getGameObject.transform.position + offset;
    }
}
