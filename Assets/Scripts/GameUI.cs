using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;
using System;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour, IOnScoreChange, IOnGameOver
{
    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    [SerializeField] GameObject gameOverDialog;
    [SerializeField] Text scoreTableTittleText;
    [SerializeField] Text scoreTable;
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_SCORE_CHANGE , OnScoreChange);
        EventManager.StartListening(EventName.ON_GAME_OVER, OnGameOVer);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_SCORE_CHANGE, OnScoreChange);
        EventManager.StopListening(EventName.ON_GAME_OVER, OnGameOVer);
    }

    public void OnScoreChange()
    {
        var scores =  EventManager.GetSender(EventName.ON_SCORE_CHANGE) as Tuple<int,int>;
        playerScoreText.text = scores.Item1.ToString();
        botScoreText.text = scores.Item2.ToString();
    }

    public void OnGameOVer()
    {
        var scores = EventManager.GetSender(EventName.ON_GAME_OVER) as Tuple<int, int>;

        int playerScore = scores.Item1;

        int botScore = scores.Item2;

        bool isPlayerWin = playerScore > botScore;

        if (isPlayerWin)
        {
            scoreTableTittleText.text = "YOU WIN";
            DataManager.Instance.NumberOfWins++;
        }
        else
        {
            scoreTableTittleText.text = "YOU LOSS";
        }

        scoreTable.text = string.Format("{0} : {1}", playerScore, botScore);

        StartCoroutine(ShowGameOverDialog());
    }

    IEnumerator ShowGameOverDialog()
    {
        yield return new WaitForSeconds(2f);

        gameOverDialog.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MenuScene");
    }
}
