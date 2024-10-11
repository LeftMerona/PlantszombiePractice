using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Event : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;
    bool isReady = false;
    [SerializeField] Button spawnButton;
    [SerializeField] GameObject checkimage;
    [SerializeField] Image coolimage;

    ContainerManager containerManager;


    private void Start()
    {
        containerManager = FindObjectOfType<ContainerManager>().gameObject.GetComponent<ContainerManager>();
 
    }

    private void Update()
    {
        SpawnPlant();


    }

    public void SetCheck()
    {
        if (!checkimage.gameObject.activeSelf)
        {
            checkimage.SetActive(true);
            isReady = true;
        }
        else
        {
            checkimage.SetActive(false);
            isReady = false;
        }
    }


    public void SpawnPlant()
    {
        if (!isReady) return;

        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(Input.GetMouseButtonDown(0) && isReady)
        {
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit2d.collider != null)
            {
             //   containerManager.ViewTileCross(hit2d.collider.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0) && isReady)
        {
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit2d.collider != null)
            {
                if (hit2d.collider.transform.CompareTag("Tile"))
                {
                    Vector3 planPos = hit2d.collider.transform.position;

                    int layerMask = 1 << LayerMask.NameToLayer("Plant");
                    hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
                  //  containerManager.ResetViewTileCross(hit2d.collider.transform.position);

                    if (hit2d.collider == null)
                    {
                        Instantiate(prefabs, planPos, Quaternion.identity);
                        StartCoroutine(CoolTime_co(5));
                        SetCheck();
                    }

                }

            }

        }

    }


    private bool InUnitInstalled(Vector3 pos)
    {
        //위에 썼던것
        //if (!InUnitInstalled(hit2d.collider.transform.position))
        //{
        //    Instantiate(prefabs, hit2d.collider.transform.position, Quaternion.identity);
        //    StartCoroutine(CoolTime_co(10f));
        //}
        //else
        //{
        //    Debug.Log("이미 설치되있음 ");
        //}

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 0.5f);

        //foreach (Collider2D collider in colliders)
        //{
        //    Debug.Log(collider.tag);
        //    if (collider.CompareTag("Unit"))
        //    {
        //        return true;
        //    }
        //}

        //    Debug.Log(collider.tag);
        //    if (collider.CompareTag("Unit"))
        //    {
        //        return true;
        //    }

        return false;

    }

    private IEnumerator CoolTime_co(float cool)
    {
        if (!coolimage.gameObject.activeSelf)
        {
            coolimage.fillAmount = 0;
            coolimage.gameObject.SetActive(true);
        }

        float time = 0;
        while (cool > time)
        {
            time += Time.deltaTime;
            coolimage.fillAmount = (time / cool);
            yield return new WaitForFixedUpdate();
            spawnButton.enabled = false;
        }

        spawnButton.enabled = true;
        coolimage.gameObject.SetActive(false);
    }


}
