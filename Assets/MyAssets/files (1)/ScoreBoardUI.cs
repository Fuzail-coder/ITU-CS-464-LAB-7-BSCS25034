using TMPro;
using UnityEngine;

// On the Score Board panel. Fills 5 rows with the saved top scores.
public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] rows;   // 5 text objects

    void OnEnable()
    {
        int[] scores = GameData.GetHighScores();
        for (int i = 0; i < rows.Length && i < scores.Length; i++)
            rows[i].text = (i + 1) + ".   " + scores[i];
    }
}
