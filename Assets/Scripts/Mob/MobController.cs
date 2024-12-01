using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobController : MonoBehaviour
{
    public Vector2 KhuVucDichuyen = new Vector2(5f, 3f);
    private Vector2 defaultVector, targetVector;
    private SpriteRenderer sp;
    private CircleCollider2D cc;
    private bool isDied;
    private Rigidbody2D rb;
    public float MoveSpeed = 2f;
    public bool isMovetoChar = false;
    public int hp;
    private int num = 0;
    public int maxHp = 200;
    private Animator animator;
    public bool isMovetoLocal  = false;
    private GameObject CharGameObj;
    public MessageVuKhi message;
    public myCharz scriptMyChar;
    public Slider slider;
    public Canvas canvas;
    public GameObject getXuObj;
    void Start()
    { 
        CharGameObj = GameObject.FindGameObjectWithTag("myCharz");
        scriptMyChar = CharGameObj.GetComponent<myCharz>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        cc = GetComponentInChildren<CircleCollider2D>();
        defaultVector = transform.position;
        animator = GetComponent<Animator>();
        hp = maxHp;
        RandomVector();
        slider.maxValue = maxHp;
    }
    public void TakeDame(int dame)
    {
        hp -= dame;
        slider.value = hp;
    }
    public void SetDistance(float kk)
    {
        if (!isMovetoLocal && kk <=2f && !scriptMyChar.StartDie())
        {
            isMovetoChar = true;
        }
    }
    void FixedUpdate()
    {
        float KhoangMobvaChar = Vector3.Distance(CharGameObj.transform.position, transform.position);
        float KhoangcachMobdadi = Vector3.Distance(defaultVector, transform.position);
        Flip();
        SetDistance(KhoangMobvaChar);
        animator.SetBool("isMobAttack", KhoangMobvaChar <= 1.5 && !scriptMyChar.StartDie() && !isDied);
        if (isMovetoLocal)
        {
            if (Vector3.Distance((Vector2)transform.position, defaultVector) <0.1f)
            {
                isMovetoLocal = false;
            }
            num = 0;
            targetVector = defaultVector;
        }
        else if (isMovetoChar)
        {
            targetVector = CharGameObj.transform.position;
            if (KhoangcachMobdadi > 10f || scriptMyChar.StartDie())
            {
                isMovetoChar = false;
                isMovetoLocal = true;
                num = 0;
                return;
            }
            if (KhoangMobvaChar <= 1.5f)
            {
                num++;
                if (num >= 42)
                {
                    scriptMyChar.audioAttack.Play();
                    num = 0;
                    scriptMyChar.TakeDame(message.dame);
                }
                return;
            }
        }
        MoveToTarget();
    }
    public void isDie()
    {
        isDied = true;
        sp.enabled = false;
        cc.enabled = false;
        canvas.enabled = false;
        GameObject gameObj = Instantiate(getXuObj, transform.position, Quaternion.identity);
        StartCoroutine(isLive(gameObj));
    }
    private IEnumerator isLive(GameObject game)
    {
        yield return new WaitForSeconds(5f);
        Destroy(game);
        sp.enabled = true; 
        cc.enabled = true;
        canvas.enabled = true;
        isDied = false;
        targetVector = transform.localPosition;
        hp = maxHp;
        slider.value = hp;
        Debug.Log("boss isLive");
    }
    private void MoveToTarget()
    {
        if (isDied)
            return;
        if(Vector2.Distance(rb.position, targetVector) >= 0.1f)
        {
            transform.position = Vector2.MoveTowards(rb.position, targetVector, Time.deltaTime * (isMovetoChar ? 1.5f:1)*MoveSpeed);
            return;
        }
        if (isMovetoLocal || isMovetoChar)
            return;
        RandomVector();
    }
    void Flip()
    {
        if(transform.position.x > targetVector.x)
        {
            sp.flipX = true;
        }
        else if(transform.position.x < targetVector.x)
        {
            sp.flipX = false;
        }
    }
    private void RandomVector()
    {
        float x = Random.Range(defaultVector.x + KhuVucDichuyen.x / 2, defaultVector.x - KhuVucDichuyen.x / 2);
        float y = Random.Range(defaultVector.y + KhuVucDichuyen.y, defaultVector.y - KhuVucDichuyen.y);
        targetVector = new Vector2(x, y); 
    }
}
