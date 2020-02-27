using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Help : MonoBehaviour
{
    /// <summary>
    /// パネルを格納する変数
    /// </summary>
    [SerializeField] GameObject help_panel;
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 操作方法ボタンが押されたときHelpPanelをアクティブにする
    /// </summary>
    public void SelectHelpOpenBotton()
    {
        help_panel.SetActive(true);
    }

    public void Close()
    {
        help_panel.SetActive(false);
    }
}
