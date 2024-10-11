using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControll : MonoBehaviour
{
    List<GameObject> plantList;
    bool isReady;
    Queue<GameObject> selectPlant;

    public bool IsReady
    {
        get => isReady;
        set => isReady = value;
    }

    public List<GameObject> PlantList
    {
        get => plantList;
        set => plantList = value;
    }


    private void Awake()
    {
        plantList = new List<GameObject>();
        selectPlant = new Queue<GameObject>();
    }

    private void Update()
    {
        GetSun();
    }




    public void SelectOnlyOne(GameObject target)
    {
        if (selectPlant.Count > 0)
        {
            for (int i = 0; i < selectPlant.Count; i++)
            {
                var goway = selectPlant.Dequeue();
                goway.transform.GetComponent<Button_Plants>().SetCheck();
            }
        }

        if (selectPlant.Count == 0)
        {
            selectPlant.Enqueue(target);
        }
    }

    public void SelectEmpty()
    {
        if (selectPlant.Count > 0)
        {
            for (int i = 0; i < selectPlant.Count; i++)
            {
                var goway = selectPlant.Dequeue();
                goway.transform.GetComponent<Button_Plants>().SetCheck();
            }
        }
    }

    public void GetSun()
    {
        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit2d.collider != null)
            {
                if(hit2d.collider.transform.CompareTag("SUN"))
                {
                    GameManager.sun_Money += 50;
                    hit2d.collider.gameObject.SetActive(false);
                    Destroy(hit2d.collider.gameObject);
                }
            }
        }

    }




}
