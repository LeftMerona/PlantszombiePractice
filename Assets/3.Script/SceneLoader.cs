using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneLoaderMe(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}
