using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int sun_Money;
    [SerializeField] GameObject SunMoney_ob;
    [SerializeField] Text money_text;
    [SerializeField] GameObject car;

    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = new GameManager();
        }

        return GameManager.instance;
    }


    private void Awake()
    {
        sun_Money = 50;
        StartGame();
    }

    private void Update()
    {
        if(money_text != null)
        {
          money_text.text = sun_Money.ToString();
        }
    }

    public void StartGame()
    {
        StartCoroutine("SetCar");
    }

    public IEnumerator SetCar()
    {
        var camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        yield return StartCoroutine(camera.MoveCamera_co());

        GameObject.Find("SelectList").transform.DOLocalMoveX(Vector3.zero.x, 30f * Time.deltaTime);
        GameObject.Find("SelectPlant").transform.DOLocalMoveY(84f, 30f * Time.deltaTime);
        GameObject.Find("SelectPlant").transform.GetComponent<UISelectPlant>().CreateScrollViewList();

        bool pushButton = false;
        GameObject.Find("Button_Play").transform.GetComponent<Button>().onClick.AddListener(() => pushButton = true);
        yield return new WaitUntil(() => pushButton);

        yield return StartCoroutine(camera.MoveCamera_Zero());

        var carPos = GameObject.Find("Carpivot").transform.position;
        var firstInsPos = new Vector3(-10f, -3.5f, 0f);
        float posY = 1.7f;

        for (int i = 0; i < 5; i++)
        {
            var carOb = Instantiate(car, firstInsPos, Quaternion.identity);
            carOb.transform.DOMove(carPos, 25f * Time.deltaTime);

            if (Vector3.Distance(carOb.transform.position, carPos) < 0.4f)
            {
                carOb.transform.position = carPos;
            }
            new WaitForSeconds(1f);
            yield return new WaitForSeconds(1f);
            firstInsPos = new Vector3(-10f, firstInsPos.y + posY, 0f);
            carPos = new Vector3(carPos.x, carPos.y + posY, 0f);
        }

        GameObject.Find("SunSpawner").transform.GetComponent<Sun>().enabled = true;
        SunMoney_ob.SetActive(true);

        var timer = GameObject.Find("GameTimer");

        timer.transform.DOMoveY(SunMoney_ob.transform.position.y, 10f * Time.deltaTime);

        if((SunMoney_ob.transform.position.y - timer.transform.position.y) < 0.1f)
        {
            timer.transform.position = new Vector3(timer.transform.position.x, SunMoney_ob.transform.position.y, 0f);
        }

        var zombies = GameObject.Find("ZBSpawnManager").transform.GetComponent<ZombieSpawnManager>();
        zombies.MakeZB();

        
        GameObject.Find("GameTimer").GetComponent<GameTimer>().IsStart = true;
    }






}
