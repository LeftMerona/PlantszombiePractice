using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;
    //-5, 5
    private float spawnheight;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    public GameObject Prefabs_Sun { get => prefabs; }


    void Start()
    {
        spawnheight = 8;
        minX = GameObject.Find("Field").transform.GetChild(0).transform.position.x;
        minY = GameObject.Find("Field").transform.GetChild(0).transform.position.y;
        maxX = GameObject.Find("Field").transform.GetChild(1).transform.position.x;
        maxY = GameObject.Find("Field").transform.GetChild(1).transform.position.y;


        StartCoroutine(MakeSunSky());
    }

    private IEnumerator MakeSunSky()
    {
        while(true)
        {
            yield return new WaitForSeconds(6f);

            float insPosX = Random.Range(minX, maxX);
            Vector3 inspos = new Vector3(insPosX, spawnheight, 0f);
            var sun = Instantiate(prefabs, inspos, Quaternion.identity);

            if (sun != null)
            {
                float movePosY = Random.Range(minY, maxY);
                Vector3 movePos = new Vector3(insPosX, movePosY, 0f);
                sun.transform.DOMoveY(movePos.y, 10f);
                if (Vector3.Distance(sun.transform.position, movePos) < 0.3f)
                {
                    sun.transform.position = movePos;
                }
            }
        }
        
    }

    public void MakeSunSunFlower(GameObject target)
    {
        Vector3 makepos = new Vector3(target.transform.position.x - 0.5f, target.transform.position.y, 0f);
        var sun = Instantiate(prefabs, makepos, Quaternion.identity);

    }

}
