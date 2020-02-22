using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    
    [SerializeField]GameObject loading_obj = null;
    [SerializeField]Animator loading_animator = null;

    // Start is called before the first frame update
    void Start()
    {
        loading_obj.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 表示する
    /// </summary>
    public void Show()
    {
        loading_obj.SetActive(true);
    }

    /// <summary>
    /// 非表示にする
    /// </summary>
    public void Hide()
    {
        loading_animator.SetTrigger("Hide");
    }

    /// <summary>
    /// アニメーション完了後に消す
    /// </summary>
    public void OnHideAnimComplete()
    {
        loading_obj.SetActive(false);
    }
}
