using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] float gameDuration = 30f; // 게임 시간
    private float elapsedTime;
    private float lastwave;
    private bool isStart;

    [SerializeField] private Button resultButton;

    public bool IsStart { get => isStart; set => isStart = value; }

    void Start()
    {
        isStart = false;
        timerSlider.maxValue = gameDuration;
        elapsedTime = 0;
        timerSlider.value = elapsedTime;

        lastwave = gameDuration * 0.85f;
    }

    // Update is called once per frame
    void Update()
    {
        //  TimerCheck();

        if (!isStart) return;
        else
        {
            elapsedTime += Time.deltaTime;

            timerSlider.value = elapsedTime;

            if (elapsedTime >= lastwave)
            {
                PauserTimer();
                StartCoroutine(CheckFieldZombiAndResum());
            }

            //승리 패배는  losecondition;
            if (elapsedTime >= gameDuration && GameObject.FindGameObjectsWithTag("Zombie").Length <= 0)
            {
                if(!resultButton.gameObject.activeSelf)
                {
                    resultButton.gameObject.SetActive(true);
                }

                var btn = GameObject.Find("ResultButton");
                btn.SetActive(true);
                btn.transform.GetChild(0).GetComponent<Text>().text = "승리(버튼클릭)";

                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Lobby"));
            }


        }

    }

    private void TimerCheck()
    {
        while (isStart)
        {
            elapsedTime += Time.deltaTime;

            timerSlider.value = elapsedTime;

            if (elapsedTime >= lastwave)
            {
                PauserTimer();
                StartCoroutine(CheckFieldZombiAndResum());
                Debug.Log("마지막");
            }

        }

    }

    public void PauserTimer()
    {
        isStart = false;
    }

    public void ResumeTimer()
    {
        isStart = true;
    }

    public IEnumerator CheckFieldZombiAndResum()
    {
        // 좀비 없어질때까지
        while (GameObject.FindGameObjectsWithTag("Zombie").Length > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        ResumeTimer();
    }





}
