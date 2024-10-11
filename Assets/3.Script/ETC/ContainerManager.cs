using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    // 9 x 5 
    GameObject[,] containers;

    private void Awake()
    {
        containers = new GameObject[5, 9];
        int index = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                containers[i, j] = this.gameObject.transform.GetChild(index).gameObject;
                index++;
            }
        }

    }

    public void ViewTileCross(Vector2 pos)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (containers[i, j].transform.position.x == pos.x || containers[i, j].transform.position.y == pos.y)
                {
                    var constainer = containers[i, j].gameObject.GetComponent<SpriteRenderer>();
                    constainer.color = new Color(constainer.color.r, constainer.color.g, constainer.color.b, 0.7f);
                }
            }
        }

    }

    public void ResetViewTileCross(Vector2 pos)
    {
        Debug.Log("¸®¼Â");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {

                var constainer = containers[i, j].gameObject.GetComponent<SpriteRenderer>();
                if (constainer.color.a != 0f)
                {
                   constainer.color = new Color(constainer.color.r, constainer.color.g, constainer.color.b, 0f);
                }

            }
        }
    }

}
