using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectPlant : MonoBehaviour
{
    // -260 -1065 > 80
    GameObject content;
    [SerializeField] Image titleImage;
    [SerializeField] Sprite[] buttonSprite;
    // 1 0 3 2 5 7 54
    Vector3[] canvasPlantPos;
    Dictionary<int, Plants_Data> dicPlantData;

    public Vector3[] CanvasPlantPos => canvasPlantPos;

    private void Awake()
    {
        dicPlantData = DataManager.GetInstance().GetPlantDatas();
        canvasPlantPos = new Vector3[dicPlantData.Count];
        content = GameObject.FindGameObjectWithTag("ScrollList");
    }

    private void Start()
    {
        //CreateScrollViewList();
    }


    public void CreateScrollViewList()
    {
        float x = 190f;
        float y = 120f;
        Vector3 nowPos = content.gameObject.transform.GetChild(0).transform.position;
        Vector3 canPos = GameObject.Find("viewpivot").transform.position;
        float nowX = nowPos.x;
        float canPosX = canPos.x;

        //리소스로드 / 스프라이트 이미지
         GameObject prefabs = Resources.Load("Prefabs/SelectImage") as GameObject;
        var title = prefabs.GetComponent<Image>();
        title.sprite = buttonSprite[0];

        //인스턴스화 밑에 깔꺼
        var selectImage = Instantiate(prefabs, nowPos, Quaternion.identity);
        selectImage.transform.SetParent(content.transform);
        var title1 = selectImage.GetComponent<Image>();
        title1.color = new Color(title1.color.r, title1.color.g, title1.color.b, 0.5f);

       // 선택하면 움직일거 > 버튼 프리팹으로 변경
        string path = "Prefabs/Button_Plant/Button_" + dicPlantData[100]._name;
        prefabs = Resources.Load(path) as GameObject;
        var movePrefab = Instantiate(prefabs, canPos, Quaternion.identity);
        movePrefab.transform.SetParent(GameObject.Find("Canvas").transform);
 
        Button_Plants btnplants = movePrefab.GetComponent<Button_Plants>();
        movePrefab.GetComponent<Button_Plants>().ID = 100;

        Button moveButton = movePrefab.GetComponent<Button>();
        moveButton.enabled = true;
        moveButton.onClick.RemoveAllListeners();
        moveButton.onClick.AddListener(() => btnplants.ClickMoveEmpty(EventSystem.current.currentSelectedGameObject));
        movePrefab.transform.GetChild(0).gameObject.SetActive(false);
        movePrefab.transform.GetChild(1).gameObject.SetActive(true);
        movePrefab.transform.GetChild(2).gameObject.SetActive(true);
        movePrefab.transform.GetChild(3).gameObject.SetActive(false);
        canvasPlantPos[0] = canPos;

        var cost = movePrefab.transform.GetChild(2).GetComponent<Text>();
        cost.text = DataManager.GetInstance().GetPlantDataById(100)._cost.ToString();

        nowPos = new Vector3(nowPos.x + x, nowPos.y, nowPos.z);
        canPos = new Vector3(canPos.x + x, canPos.y, canPos.z);

        int count = 1;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                if (count >= 6)
                {
                    count = 6;
                    prefabs = Resources.Load("Prefabs/SelectImage") as GameObject;
                    title = prefabs.GetComponent<Image>();
                    title.sprite = buttonSprite[count];
                    prefabs.transform.GetChild(0).gameObject.SetActive(false);
                    prefabs.transform.GetChild(1).gameObject.SetActive(false);
                    prefabs.transform.GetComponent<Button>().enabled = false;

                    selectImage = Instantiate(prefabs, nowPos, Quaternion.identity);
                    selectImage.transform.SetParent(content.transform);

                    nowPos = new Vector3(nowPos.x + x, nowPos.y, nowPos.z);

                }
                else
                {
                    prefabs = Resources.Load("Prefabs/SelectImage") as GameObject;
                    title = prefabs.GetComponent<Image>();
                    title.sprite = buttonSprite[count];
                    selectImage = Instantiate(prefabs, nowPos, Quaternion.identity);
                    selectImage.transform.SetParent(content.transform);
                    title1 = selectImage.GetComponent<Image>();
                    title1.color = new Color(title1.color.r, title1.color.g, title1.color.b, 0.5f);

                    path = "Prefabs/Button_Plant/Button_" + dicPlantData[count + 100]._name;
                    prefabs = Resources.Load(path) as GameObject;

                    movePrefab = Instantiate(prefabs, canPos, Quaternion.identity);
                    movePrefab.transform.SetParent(GameObject.Find("Canvas").transform);
                    btnplants = movePrefab.GetComponent<Button_Plants>();

                    moveButton = movePrefab.GetComponent<Button>();
                    moveButton.enabled = true;
                    moveButton.onClick.RemoveAllListeners();
                    moveButton.onClick.AddListener(() => btnplants.ClickMoveEmpty(EventSystem.current.currentSelectedGameObject));
                    movePrefab.GetComponent<Button_Plants>().ID = count + 100;
                    movePrefab.transform.GetChild(0).gameObject.SetActive(false);
                    movePrefab.transform.GetChild(1).gameObject.SetActive(true);
                    movePrefab.transform.GetChild(2).gameObject.SetActive(true);
                    movePrefab.transform.GetChild(3).gameObject.SetActive(false);

                    canvasPlantPos[count] = canPos;

                    cost = movePrefab.transform.GetChild(2).GetComponent<Text>();
                    cost.text = DataManager.GetInstance().GetPlantDataById(100 + count)._cost.ToString();

                    nowPos = new Vector3(nowPos.x + x, nowPos.y, nowPos.z);
                    canPos = new Vector3(canPos.x + x, canPos.y, canPos.z);

                    count++;
                }
            }
            nowPos = new Vector3(nowX, nowPos.y - y, nowPos.z);
            canPos = new Vector3(canPosX, canPos.y - y, canPos.z);
        }


    }



}
