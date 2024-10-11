using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    [SerializeField] private GameObject deletepivot;

    private void Awake()
    {
        if (deletepivot == null)
        {
            deletepivot = GameObject.Find("Max");
        }
    }

    private void Update()
    {
        if (this.gameObject.transform.position.x >= deletepivot.transform.position.x)
        {
            this.gameObject.SetActive(false);
            this.gameObject.transform.position = new Vector3(-0.184f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            Debug.Log("트리거");
            StartCoroutine(HitZombie(collision.gameObject));

            
        }
    }



    public IEnumerator HitZombie(GameObject zombie)
    {
        Debug.Log("좀비힛");
        var colob = zombie.transform.GetComponent<SpriteRenderer>();
        var oricolor = colob.color;

        colob.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        colob.color = oricolor;

        zombie.transform.GetComponent<ZombieControll>().Hitcount += 1;
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = new Vector3(-0.184f, 0f, 0f);
    }

}


