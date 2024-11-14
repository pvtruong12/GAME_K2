using System;
using System.Collections;
using UnityEngine;

public class myCharz : MonoBehaviour
{
    public float MoveSpeed = 2f;
    private bool isFlipX = true;
    private Rigidbody2D rd;
    private Vector2 vectorMove;
    private SpriteRenderer sp;
    private SpriteRenderer sp2;
    private Animator animator;
    public GameObject attackPos;
    public GameObject attackEffectVienDan;
    public Vector3 vectorMoveWithMouse;



    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sp2= attackPos.GetComponent<SpriteRenderer>();
        vectorMoveWithMouse = Vector3.zero;
    }


    void Update()
    {
        vectorMove.x = Input.GetAxisRaw("Horizontal");
        vectorMove.y = Input.GetAxisRaw("Vertical");
        vectorMove.Normalize();
        animator.SetBool("isRun", vectorMove.x != 0 || vectorMove.y != 0 || vectorMoveWithMouse != Vector3.zero);
        Flip();
        UpdateKeyTouchController();
        UpdateClickMouse();
        MoveToFocus();


    }

    private void FixedUpdate()
    {
        rd.velocity = MoveSpeed * vectorMove;
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
            Attack3();
        }
    }
    public void Attack3()
    {
        GameObject newBullet = Instantiate(attackEffectVienDan, transform.position, Quaternion.identity);
        VienDan vienDan = newBullet.GetComponent<VienDan>();
        vienDan.getGameObject = gameObject;

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
            transform.position = Vector3.MoveTowards(transform.position, vectorMoveWithMouse, MoveSpeed * Time.deltaTime);
        }
        
    }

    private IEnumerator DisnableObj(float time)
    {
        yield return new WaitForSeconds(time);
        attackPos.SetActive(false);
    }
    public void Flip()
    {
        float originalX = attackPos.transform.localPosition.x;
        if (vectorMove.x > 0 || (vectorMoveWithMouse.x >= transform.position.x && vectorMoveWithMouse != Vector3.zero))
        {
            sp.flipX = false;
            sp2.flipX = false;
            attackPos.transform.localPosition = new Vector3(Mathf.Abs(originalX), attackPos.transform.localPosition.y, 0);
        }
        if (vectorMove.x < 0 || (vectorMoveWithMouse.x < transform.position.x && vectorMoveWithMouse != Vector3.zero))
        {
            sp.flipX = true;
            sp2.flipX = true;
            attackPos.transform.localPosition = new Vector3(originalX < 0 ? originalX : -originalX, attackPos.transform.localPosition.y, 0);
        }
    }
}
