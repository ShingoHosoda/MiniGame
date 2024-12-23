using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーンの切り替え
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    /// <summary>
    /// シーンの読み込み
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
