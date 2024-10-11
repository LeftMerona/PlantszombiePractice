using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    int id = 101;
    string _name = "Peashooter";
    int _image_name = 1;
    int _cost = 100;
    Plant_Type _e_type = Plant_Type.Shooter;
    float _attack_time = 10;
    int _MaxHP = 10;
    int _CurrentHP = 10;
    float _RespawnTime = 15;

    [SerializeField] GameObject[] bullet;

    private void Start()
    {
        StartCoroutine("ShootBullet");
    }

    public IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);

            for (int i = 0; i < bullet.Length; i++)
            {
                if (i > bullet.Length - 1)
                {
                    i = 0;
                }

                if (!bullet[i].activeSelf)
                {
                    bullet[i].SetActive(true);
                    yield return new WaitForSeconds(6f);
                }

            }


        }

    }

}