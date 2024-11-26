using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "ScriptsTableObject/Message", order = 0)]
public class Message : ScriptableObject
{
    public float CharMoveSpeed;
    public GameObject attackEffectVienDan;
    public GameObject attackEffecBoom;
    public int cHPMax;
    public int SoDan;
    public float Sleep;
}
