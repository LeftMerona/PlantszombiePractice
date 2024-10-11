using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunFlower : MonoBehaviour
{
    int id = 100;
    string _name = "SunFlower";
    int _image_name = 0;
    int _cost = 50;
    Plant_Type _e_type = Plant_Type.Support;
    float _attack_time = 10;
    int _MaxHP = 7;
    int _CurrentHP = 7;
    float _RespawnTime = 15;

    private Color originColor;
    private GameObject sun_ob;
    private Vector3 spawnSun;

    private void Start()
    {
        originColor = transform.GetComponent<SpriteRenderer>().color;
        sun_ob = GameObject.Find("SunSpawner").GetComponent<Sun>().Prefabs_Sun;
        spawnSun = new Vector3(this.gameObject.transform.position.x - 0.5f, this.transform.position.y, 0f);

        StartCoroutine("MakeSun");
    }

    public IEnumerator MakeSun()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            var sunImage = transform.GetComponent<SpriteRenderer>();
            sunImage.color = Color.blue;


            yield return new WaitForSeconds(0.5f);
            sunImage.color = originColor;

            Instantiate(sun_ob, spawnSun, Quaternion.identity);
        }

    }

}
