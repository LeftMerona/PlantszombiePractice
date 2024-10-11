using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_PlayControll : MonoBehaviour
{
    UISelectPlant uiselectPlant;
    SelectList selectList;
    List<GameObject> emptylist;

    private void Awake()
    {
        uiselectPlant = GameObject.Find("SelectPlant").GetComponent<UISelectPlant>();
        selectList = GameObject.Find("SelectList").GetComponent<SelectList>();
    }

    private void Start()
    {
        emptylist = selectList.EmtyList_ob;
        CheckPlay(default);
    }

    public void CheckPlay(int count)
    {
        if (count > 5)
        {
            var btnimage = gameObject.transform.GetComponent<Image>();
            btnimage.color = new Color(1f, 1f, 1f, 1f);
            gameObject.transform.GetComponent<Button>().enabled = true;
        }
        else
        {
            var btnimage = gameObject.transform.GetComponent<Image>();
            btnimage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            gameObject.transform.GetComponent<Button>().enabled = false;
        }

    }


    public void PlayStartGameInit()
    {
        // 버튼 기능 초기화 
        var btnlist = selectList.SelecList_ob;

        for (int i = 0; i < btnlist.Count; i++)
        {
            Button btn = btnlist[i].transform.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(btnlist[i].transform.GetComponent<Button_Plants>().SetCheck);

            for (int j = 0; j < btnlist[i].transform.childCount; j++)
            {
                btnlist[i].transform.GetChild(j).gameObject.SetActive(true);
            }
            btnlist[i].transform.GetChild(0).gameObject.SetActive(false);

        }


        for (int i = 0; i < emptylist.Count; i++)
        {
            Destroy(emptylist[i]);
            btnlist[i].transform.SetParent(selectList.transform);
            btnlist[i].transform.GetComponent<Button_Plants>().IsPlay = true;
        }

        GameObject.Find("Player").GetComponent<PlayerControll>().PlantList = btnlist;

        Destroy(GameObject.Find("SelectPlant").gameObject);
        Destroy(this.gameObject);        
    }



}
