using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Button savingScoreBtn;
    public Button replayBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (ScoreManager.instance != null)
        {
            if (ScoreManager.instance.currentScore > ScoreManager.instance.highScore)
            {
                ScoreManager.instance.highScore = ScoreManager.instance.currentScore;
                scoreText.text = "New High Score: " + ScoreManager.instance.currentScore + "!!!";
                savingScoreBtn.gameObject.SetActive(true);
                replayBtn.gameObject.SetActive(false);
            }
            else
            {
                scoreText.text = "Score: " + ScoreManager.instance.currentScore;
                savingScoreBtn.gameObject.SetActive(false);
                replayBtn.gameObject.SetActive(true);
            }
            ScoreManager.instance.SaveHighScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
//        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
//        {
//            //Switch to menu scene
//            // ScoreManager.instance.tokenGUI:SetActive(true);
//            SceneManager.LoadScene(0);
//        }

//        // Quit if hit Esc
//        if (Input.GetButtonDown("Cancel"))
//        {
//#if UNITY_EDITOR
//            EditorApplication.ExitPlaymode();
//#else
//            Application.Quit();
//#endif
//        }

    }
}
