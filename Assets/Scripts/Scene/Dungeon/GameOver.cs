using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameover_ui_;
    
    // Start is called before the first frame update
    void Start()
    {
        gameover_ui_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverExec()
    {
        gameover_ui_.SetActive(true);
        DungeonInfo.DungeonState = DungeonInfo.State.GameOver;
    }

    public void RetryButtonClick()
    {

    }

    public void TitleButtonClick()
    {
        SceneController.LoadScene("Title");
    }
}
