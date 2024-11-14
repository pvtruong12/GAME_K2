using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Vector2 KhuVucDichuyen = new Vector2(5f, 3f);
    private Vector2 currentVector, targetVector;
    private SpriteRenderer sp;
    private BoxCollider2D col;
    private bool isDied;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        currentVector = transform.position;
        RandomVector();
    }
    void FixedUpdate()
    {
        MoveToTarget();
    }
    public void isDie()
    {

        Debug.Log("boss isDie");
        isDied = true;
        sp.enabled = false;
        col.enabled = false;
        StartCoroutine(isLive());
    }
    private IEnumerator isLive()
    {
        yield return new WaitForSeconds(5f);
        sp.enabled = true; 
        col.enabled = true;
        isDied = false;
        targetVector = transform.localPosition;
        Debug.Log("boss isLive");
    }
    private void MoveToTarget()
    {
        if (isDied)
            return;
        if((Vector2)transform.position != targetVector)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetVector, Time.deltaTime * 2);
            SetFlipX();
            return;
        }
        RandomVector();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void SetFlipX()
    {
        if (transform.position.x > targetVector.x)
            sp.flipX = true;
        else if (transform.position.x < targetVector.x)
            sp.flipX = false;
    }
    private void RandomVector()
    {
        float x = Random.Range(currentVector.x + KhuVucDichuyen.x / 2, currentVector.x - KhuVucDichuyen.x / 2);
        float y = Random.Range(currentVector.y + KhuVucDichuyen.y, currentVector.y - KhuVucDichuyen.y);
        targetVector = new Vector2(x, y); 
    }
}
