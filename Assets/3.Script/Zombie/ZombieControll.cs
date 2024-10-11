using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControll : MonoBehaviour
{
    // ����� << �� �����δ� (Movement2D ��ũ��Ʈ�� �ذ�)
    // õõ�� �����̸� Plant�� ������ �����Ѵ� 

    private float _MaxHp;
    private float _CurrentHp;

    private Animator zombie_ani;
    [SerializeField] private Sprite currentBody;
    [SerializeField] private Sprite losthead;
    [SerializeField] private float MaxHp => _MaxHp;
    [SerializeField] private float CurrentHp => _CurrentHp;



    private int hitcount;

    public int Hitcount { get => hitcount; set => hitcount = value; }

    private void Start()
    {
        hitcount = 0;
        _MaxHp = 10;
        _CurrentHp = _MaxHp;

        TryGetComponent(out currentBody);
        TryGetComponent(out zombie_ani);


    }

    private void Update()
    {
        if(hitcount >= 3)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Plant"))
        {
            this.gameObject.transform.GetComponent<Movement2D>().enabled = false;
            var ani = this.gameObject.GetComponent<Animator>();
            ani.SetTrigger("FrontPlant");
            
        }

    }


    private void ChangeBody()
    {
        zombie_ani.SetBool("isHalfHP", true);
        currentBody = losthead;
        
              
    }


    private void AttackPlant()
    {

    }

}
