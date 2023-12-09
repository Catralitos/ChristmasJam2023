using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "4a8b58af00e63e002050cd4e3032b81bdff156b9db83774cf2bf0b87ef5804c5";

    private void Start()
    {
        GetLeaderBoard();
    }
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();

            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            LeaderboardCreator.ResetPlayer();
            GetLeaderBoard();
        }));
    }
}
