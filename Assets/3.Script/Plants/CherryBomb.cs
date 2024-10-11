using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }

    public void Bomb()
    {
        //애니메이션 마지막 
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D col in hit)
        {
            if (col.CompareTag("Zombie"))
            {
                Debug.Log(col.gameObject);
                Destroy(col.gameObject);
            }
        }

        Destroy(this.gameObject);
    }


}