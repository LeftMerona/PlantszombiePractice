using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectList : MonoBehaviour
{
    // X -330  > 0
    Dictionary<Vector3, bool> dicemptyList;
    List<GameObject> emtyList_ob;
    List<GameObject> selecList_ob;

    public List<GameObject> EmtyList_ob => emtyList_ob;
    public Dictionary<Vector3, bool> DicemptyList
    {
        get => dicemptyList;
        set => dicemptyList = value;
    }

    public List<GameObject> SelecList_ob
    {
        get => selecList_ob;
        set => selecList_ob = value;
    }


    private void Awake()
    {
        dicemptyList = new Dictionary<Vector3, bool>();
        emtyList_ob = new List<GameObject>();
        selecList_ob = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            emtyList_ob.Add(transform.GetChild(i).gameObject);
            dicemptyList.Add(transform.GetChild(i).transform.position, false);
        }

    }

    public void SortSelect()
    {
        if(selecList_ob.Count > 0)
        {
            for (int i = 0; i < selecList_ob.Count; i++)
            {                
                selecList_ob[i].transform.position = emtyList_ob[i].transform.position;
            }
        }

    }



}
