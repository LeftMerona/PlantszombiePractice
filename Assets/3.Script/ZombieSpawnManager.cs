using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    private static ZombieSpawnManager instance = null;

    public static ZombieSpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ZombieSpawnManager();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject nomalzb_prefabs;

    private void Start()
    {

    }

    public void MakeZB()
    {
        StartCoroutine(MakeNomalZombie_co());
    }

    public void StopMakeZB()
    {
        StopCoroutine(MakeNomalZombie_co());
    }

    public void MakeNomalZombie()
    {
        int num = Random.Range(0, 5);
        Vector3 pos = this.gameObject.transform.GetChild(num).transform.position;
        Instantiate(nomalzb_prefabs, pos, Quaternion.identity);
    }

    public IEnumerator MakeNomalZombie_co()
    {
        yield return new WaitForSeconds(1f);

        //forπÆ¿∫ ¿·Ω√ ª°∏Æ ªÃ±‚ ¿ß«ÿ
        for(int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, 5);
            Vector3 pos = this.gameObject.transform.GetChild(num).transform.position;
            Instantiate(nomalzb_prefabs, pos, Quaternion.identity);
        }

        yield return new WaitForSecondsRealtime(1f);

    }


}
