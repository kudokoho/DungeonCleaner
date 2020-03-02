using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Help : MonoBehaviour
{
    // ヘルプパネル
    [SerializeField] GameObject help_panel = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ヘルプボタンが押されたときHelpPanelをアクティブにする
    /// </summary>
    public void OnOpenBottonClick()
    {
        help_panel.SetActive(true);
    }


    public void OnCloseBottonClick()
    {
        help_panel.SetActive(false);
    }
}
