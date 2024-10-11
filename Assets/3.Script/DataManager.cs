using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager 
{
    private static DataManager instance;
    private Dictionary<int, Plants_Data> dicPlantsData;

    private DataManager()
    {
        dicPlantsData = new Dictionary<int, Plants_Data>();
        LoadAllDatas();
    }

    public static DataManager GetInstance()
    {
        if(DataManager.instance == null)
        {
            DataManager.instance = new DataManager();

            DataManager.instance.LoadAllDatas();
        }

        return DataManager.instance;
    }

    private void LoadAllDatas()
    {
        LoadPlantsData();
    }

    private void LoadPlantsData()
    {
        var plantjson = Resources.Load<TextAsset>("Data/Plants_Data").text;
        var plantDatas = JsonConvert.DeserializeObject<Plants_Data[]>(plantjson).ToDictionary(x => x.id, x => x);
        this.dicPlantsData = plantDatas;

    }


    public Dictionary<int, Plants_Data> GetPlantDatas()
    {
        return this.dicPlantsData;
    }

    public Plants_Data GetPlantDataById(int idindex)
    {
        Plants_Data plants_Data = new Plants_Data();
        plants_Data.id = this.dicPlantsData[idindex].id;
        plants_Data._name = this.dicPlantsData[idindex]._name;
        plants_Data._image_name = this.dicPlantsData[idindex]._image_name;
        plants_Data._cost = this.dicPlantsData[idindex]._cost;
        plants_Data._e_type = this.dicPlantsData[idindex]._e_type;
        plants_Data._attack_time = this.dicPlantsData[idindex]._attack_time;
        plants_Data._MaxHP = this.dicPlantsData[idindex]._MaxHP;
        plants_Data._CurrentHP = this.dicPlantsData[idindex]._CurrentHP;
        plants_Data._RespawnTime = this.dicPlantsData[idindex]._RespawnTime;

        return plants_Data;

    }
 
    

}
