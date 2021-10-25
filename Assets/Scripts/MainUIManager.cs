using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{

    [SerializeField] List<Rank> ranks;
    [SerializeField] Image currentRankIcon;
    [SerializeField] Text currentAchivementText;


    private void Start()
    {
        LoadAchiement();
    }

    private void LoadAchiement()
    {
        int numberOfWins = DataManager.Instance.NumberOfWins;
        int currentRankIndex = 0;
        if(ranks != null && ranks.Count > 0)
        {
            for (int i = 0; i < ranks.Count; i++)
            {
                if(numberOfWins >= ranks[i].requireNumber)
                {
                    currentRankIndex = i;
                }
            }
        }

        Rank currentRank = ranks[currentRankIndex];

        currentRankIcon.sprite = currentRank.rankIcon;
        currentAchivementText.text = string.Format("WIN: {0} MATCHS", numberOfWins);

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    [System.Serializable]
    public class Rank
    {
        public Sprite rankIcon;
        public int requireNumber;
    }
}
