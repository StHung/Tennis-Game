using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance { get { return _instance; } }

    private string NUMBER_OF_WINS = "NUMBER_OF_WINS";

    public int NumberOfWins { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    private void Start()
    {
        NumberOfWins = PlayerPrefs.GetInt(NUMBER_OF_WINS, 0);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(NUMBER_OF_WINS, NumberOfWins);
        PlayerPrefs.Save();
    }
}
