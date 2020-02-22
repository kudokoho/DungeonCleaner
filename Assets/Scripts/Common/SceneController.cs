using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController:MonoBehaviour
{

    static Loading loading_ = null;
    static List<Scene> scene_list_ = new List<Scene>();

    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="scene_name"></param>
    public static void LoadScene(string scene_name)
    {
        if (loading_ == null)
        {
            loading_ = GetLoading();
        }

        loading_.Show();

        ClearScene();

        SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        // シーンロード完了後に呼ぶ関数
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// シーンロード完了
    /// </summary>
    /// <param name="nextScene">次のシーン</param>
    /// <param name="mode">ロードのモード</param>
    static void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        if (loading_ == null)
        {
            loading_ = GetLoading();
        }
        loading_.Hide();

        // クリアするためにscene_list_に追加
        scene_list_.Add(nextScene);
    }

    /// <summary>
    /// ローディング取得
    /// </summary>
    /// <returns></returns>
    static Loading GetLoading()
    {
        return FindObjectOfType<Loading>();
    }

    /// <summary>
    /// シーンの削除
    /// </summary>
    static void ClearScene()
    {
        foreach(Scene scene in scene_list_)
        {
            if (scene.name != "Boot")
            {
                Debug.Log("クリア " + scene.name);
                scene_list_.Remove(scene);
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
