using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;

public class PlayFabController : MonoBehaviour
{
    public Score scoreScript;

    public int total_score;
    public int high_score;
    public int current_score;

    public void Start()
    {


    }

    public void SetStats()
    {
        current_score = scoreScript.score;
        total_score = scoreScript.totalScore;
        high_score = scoreScript.highScore;

        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "PlayerTotalScore", Value = PlayerPrefs.GetInt("totalScore")},
                new StatisticUpdate { StatisticName = "PlayerHighScore", Value = PlayerPrefs.GetInt("highScore")},

            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }
    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            //Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "PlayerTotalScore":
                    total_score = eachStat.Value;
                    scoreScript.totalScore = eachStat.Value;
                    break;
                case "PlayerHighScore":
                    high_score = eachStat.Value;
                    scoreScript.highScore = eachStat.Value;
                    break;
            }
        }
    }
}