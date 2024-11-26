using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class myCharz : MonoBehaviour
{
    public Message dataController;
    private Rigidbody2D rd;
    private Vector2 vectorMove;
    private SpriteRenderer sp;
    private SpriteRenderer sp2;
    private Animator animator;
    public GameObject attackPos;
    public Vector3 vectorMoveWithMouse;
    public int cHP;
    private bool facingRight;
    public Slider slider;



    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sp2= attackPos.GetComponent<SpriteRenderer>();
        vectorMoveWithMouse = Vector3.zero;
        cHP = dataController.cHPMax;
        slider.maxValue = dataController.cHPMax;
    }


    void FixedUpdate()
    {
        if(StartDie())
        {
            return;
        }
        vectorMove.x = Input.GetAxisRaw("Horizontal");
        vectorMove.y = Input.GetAxisRaw("Vertical");
        vectorMove.Normalize();
        rd.velocity = dataController.CharMoveSpeed * vectorMove;
        animator.SetBool("isRun", vectorMove.x != 0 || vectorMove.y != 0 || vectorMoveWithMouse != Vector3.zero);
        Flip();
        UpdateKeyTouchController();
        UpdateClickMouse();
        MoveToFocus();
        UpdateHPChar();

    }
    public void UpdateHPChar()
    {
        slider.value = cHP;
    }

    public bool StartDie()
    {
        return cHP <= 0;
    }
     
    public void Attack2()
    {
        attackPos.SetActive(true);
        StartCoroutine(DisnableObj(0.5f));
    }

    public void UpdateKeyTouchController()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("isAttack");
            Attack2();
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(Attack3());
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(Attack4());
        }
    }
    public void TakeDame(int dame)
    {
        cHP -= dame;
        if (cHP <= 0)
        {
            animator.SetTrigger("isMeDead");
        }
    }
    public IEnumerator Attack3()
    {
        for(int i = 0; i< dataController.SoDan; i++)
        {
            GameObject newBullet = Instantiate(dataController.attackEffectVienDan, transform.position, Quaternion.identity);
            VienDan vienDan = newBullet.GetComponent<VienDan>();
            vienDan.getGameObject = gameObject;
            yield return new WaitForSeconds(dataController.Sleep);
        }

    }

    public IEnumerator Attack4()
    {
        for (int i = 0; i < dataController.SoDan; i++)
        {
            GameObject newBullet = Instantiate(dataController.attackEffecBoom, new Vector3(UnityEngine.Random.Range(-36, 23), 16,0), Quaternion.identity);
            yield return new WaitForSeconds(dataController.Sleep);
        }
    }

    public void UpdateClickMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vector.z = 0;
            vectorMoveWithMouse = vector;

        }
    }

    public void MoveToFocus()
    {
        if(vectorMoveWithMouse != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, vectorMoveWithMouse) < 0.1f)
            {
                vectorMoveWithMouse = Vector3.zero;
            }
            transform.position = Vector3.MoveTowards(transform.position, vectorMoveWithMouse, dataController.CharMoveSpeed * Time.deltaTime);
        }
        
    }

    private IEnumerator DisnableObj(float time)
    {
        yield return new WaitForSeconds(time);
        attackPos.SetActive(false);
    }
    void FlipX()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void Flip()
    {
        if ((vectorMove.x > 0 || (vectorMoveWithMouse.x >= transform.position.x && vectorMoveWithMouse != Vector3.zero)) && facingRight)
        {
            FlipX();
        }
        if ((vectorMove.x < 0 || (vectorMoveWithMouse.x < transform.position.x && vectorMoveWithMouse != Vector3.zero)) && !facingRight)
        {
            FlipX();
        }
    }
}
