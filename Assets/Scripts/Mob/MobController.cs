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
    private bool facingRight;
    public Slider slider;
    public Canvas canvas;
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
        slider.value = maxHp;
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
        SetFlipX();
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
        StartCoroutine(isLive());
    }
    private IEnumerator isLive()
    {
        yield return new WaitForSeconds(5f);
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
    public void SetFlipX()
    {
        if ((rb.position - targetVector).normalized.x < 0 && facingRight)
            Flip();
        else if ((rb.position - targetVector).normalized.x > 0 && !facingRight)
            Flip();
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void RandomVector()
    {
        float x = Random.Range(defaultVector.x + KhuVucDichuyen.x / 2, defaultVector.x - KhuVucDichuyen.x / 2);
        float y = Random.Range(defaultVector.y + KhuVucDichuyen.y, defaultVector.y - KhuVucDichuyen.y);
        targetVector = new Vector2(x, y); 
    }
}
