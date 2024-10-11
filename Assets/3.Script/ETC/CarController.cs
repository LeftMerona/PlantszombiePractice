using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarController : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            var target = GameObject.Find("Field").transform.GetChild(1).transform.position;
            this.gameObject.transform.DOMoveX(target.x, 10f);
            Destroy(collision.gameObject);
            if((target.x -  this.gameObject.transform.position.x) < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
