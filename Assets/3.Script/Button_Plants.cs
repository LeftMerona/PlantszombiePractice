using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Button_Plants : MonoBehaviour
{
    private int id;
    public int ID
    {
        get => id;
        set => id = value;
    }

    [SerializeField] private GameObject prefabs;
    bool isReady;
    [SerializeField] Button spawnButton;
    [SerializeField] GameObject checkimage;
    [SerializeField] Image coolimage;

    SelectList selectList_ob;
    Vector3[] canvasPlantPos;

    Dictionary<int, Plants_Data> dicPlantData;
    ContainerManager containerManager;
    Button_PlayControll btnControll;
    PlayerControll player;
    int plantcost;

    bool isPlay = false;

    public bool IsPlay { get => isPlay; set => isPlay = value; }

    private void Awake()
    {
        dicPlantData = DataManager.GetInstance().GetPlantDatas();
        containerManager = FindObjectOfType<ContainerManager>().gameObject.GetComponent<ContainerManager>();
        coolimage.gameObject.SetActive(false);
        selectList_ob = GameObject.Find("SelectList").GetComponent<SelectList>();
        canvasPlantPos = GameObject.Find("SelectPlant").GetComponent<UISelectPlant>().CanvasPlantPos;
        btnControll = GameObject.Find("Button_Play").GetComponent<Button_PlayControll>();
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    private void Update()
    {
        SpawnPlant();
        CheckMoney();
    }


    private void CheckMoney()
    {
        plantcost = DataManager.GetInstance().GetPlantDataById(id)._cost;

        if (isPlay == true)
        {
            if (GameManager.sun_Money >= plantcost)
            {
                coolimage.fillAmount = 0;
                coolimage.gameObject.SetActive(false);
                spawnButton.enabled = true;
            }

            if (GameManager.sun_Money < plantcost)
            {
                coolimage.fillAmount = 1;
                coolimage.gameObject.SetActive(true);
                spawnButton.enabled = false;
            }
        }

    }

    public void ClickMoveEmpty(GameObject target)
    {
        StartCoroutine(SortSelectList_co(target));
    }

    private IEnumerator SortSelectList_co(GameObject target)
    {
        selectList_ob.SortSelect();

        selectList_ob.SelecList_ob.Add(target);
        if (selectList_ob.SelecList_ob.Count > 6)
        {
            selectList_ob.SelecList_ob.RemoveAt(7);
            yield return null;
        }

        if (selectList_ob.SelecList_ob.Count >= 0)
        {

            for (int i = 0; i < selectList_ob.SelecList_ob.Count; i++)
            {
                if (!selectList_ob.SelecList_ob[i].transform.position.Equals(selectList_ob.EmtyList_ob[i].transform.position))
                {
                    while (true)
                    {
                        target.transform.DOMove(selectList_ob.EmtyList_ob[i].transform.position, 50f * Time.deltaTime);
                        if (Vector3.Distance(target.transform.position, selectList_ob.EmtyList_ob[i].transform.position) < 0.5f)
                        {
                            target.transform.position = selectList_ob.EmtyList_ob[i].transform.position;
                            Button btn = target.GetComponent<Button>();
                            btnControll.CheckPlay(selectList_ob.SelecList_ob.Count);
                            btn.onClick.RemoveAllListeners();
                            btn.onClick.AddListener(() => ClickBackMoveList(EventSystem.current.currentSelectedGameObject));
                            yield break;
                        }
                        yield return null;
                    }
                }
            }

        }
    }

    public void ClickBackMoveList(GameObject target)
    {
        StartCoroutine(SortAndBackSelectList_co(target));
    }

    private IEnumerator SortAndBackSelectList_co(GameObject target)
    {

        int inputid = target.GetComponent<Button_Plants>().ID - 100;

        if (selectList_ob.SelecList_ob.Count >= 0)
        {
            if (target.transform.position != canvasPlantPos[inputid])
            {
                while (true)
                {
                    target.transform.DOMove(canvasPlantPos[inputid], 50f * Time.deltaTime);
                    if (Vector3.Distance(target.transform.position, canvasPlantPos[inputid]) < 0.5f)
                    {
                        target.transform.position = canvasPlantPos[inputid];
                        Button btn = target.GetComponent<Button>();
                        selectList_ob.SelecList_ob.Remove(target);
                        btnControll.CheckPlay(selectList_ob.SelecList_ob.Count);
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(() => ClickMoveEmpty(EventSystem.current.currentSelectedGameObject));
                        selectList_ob.SortSelect();
                        yield break;
                    }
                    yield return null;
                }
            }

        }

    }




    public void SetCheck()
    {
        if (!checkimage.gameObject.activeSelf)
        {
            checkimage.SetActive(true);
            isReady = true;
            player.SelectOnlyOne(EventSystem.current.currentSelectedGameObject);
        }
        else
        {
            checkimage.SetActive(false);
            isReady = false;
            player.SelectEmpty();
        }
    }


    public void SpawnPlant()
    {
        if (!isReady) return;

        plantcost = DataManager.GetInstance().GetPlantDataById(id)._cost;

        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0) && isReady)
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
                        GameManager.sun_Money -= plantcost;
                        StartCoroutine(CoolTime_co(5));
                        player.SelectEmpty();

                    }

                }

            }

        }

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
