using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    Vector3 nowPos;
    Vector3 targetPos;
    public IEnumerator MoveCamera_co()
    {
        float elasped = 0f;
        float duration = 6f;

        nowPos = transform.position;
        targetPos = new Vector3(6f, 0f, -10f);
        while (elasped < duration)
        {
            transform.position = Vector3.Lerp(nowPos, targetPos, elasped / duration);
            elasped += Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                transform.position = targetPos;
                yield break;
            }

            yield return null;
        }

      
      
    }

    public IEnumerator MoveCamera_Zero()
    {
        nowPos = transform.position;
        targetPos = new Vector3(0f, 0f, -10f);

        transform.DOMoveX(Vector3.zero.x, 100f * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            Debug.Log("Èì");
            transform.position = targetPos;
        }

        yield return null;
    }


}


