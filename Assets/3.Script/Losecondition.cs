using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Losecondition : MonoBehaviour
{
    [SerializeField] private Button resultButton;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            if (!resultButton.gameObject.activeSelf)
            {
                resultButton.gameObject.SetActive(true);
            }

            var btn = GameObject.Find("ResultButton");
            btn.SetActive(true);
            btn.transform.GetChild(0).GetComponent<Text>().text = "패배(버튼클릭)";

            btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Lobby"));
        }
    }
}
