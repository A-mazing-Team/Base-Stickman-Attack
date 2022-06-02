using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Satrtup : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("ArtSceneNewDevBackup");
    }
}
