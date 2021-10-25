using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class Ball : MonoBehaviour, IOnScoreChange, IOnGameOver
{

    [SerializeField] Player player;

    [SerializeField] int scoreToWin;
    int playerScore;
    int botScore;
    Vector3 initialPos;
    bool isGameOver;

    public string Hitter { get; set; }
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            isGameOver = value;
            if(value == true)
            {
                OnGameOVer();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !isGameOver)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialPos;

            player.Reset();

            if (Hitter == "Player")
            {
                playerScore++;
            }
            else if (Hitter == "Bot")
            {
                botScore++;
            }
            OnScoreChange();
            CheckGameOVer(playerScore, botScore);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
            if (Hitter == "Player")
            {
                botScore++;
            }
            else if (Hitter == "Bot")
            {
                playerScore++;
            }
            OnScoreChange();
            CheckGameOVer(playerScore, botScore);
        }
    }

    private void CheckGameOVer(int playerScore, int botScore)
    {
        if (playerScore == scoreToWin || botScore == scoreToWin)
        {
            IsGameOver = true;
        }
    }

    public void OnScoreChange()
    {
        var scores = new Tuple<int, int>(playerScore, botScore);
        EventManager.EmitEvent(EventName.ON_SCORE_CHANGE, 0f, scores);
    }

    public void OnGameOVer()
    {
        var scores = new Tuple<int, int>(playerScore, botScore);
        EventManager.EmitEvent(EventName.ON_GAME_OVER, 0f, scores);
    }
}
