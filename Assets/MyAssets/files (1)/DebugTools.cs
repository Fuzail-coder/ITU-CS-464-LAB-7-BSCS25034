using UnityEngine;
using UnityEngine.SceneManagement;

// Optional testing buttons. Put on GameManager.
public class DebugTools : MonoBehaviour
{
    public void Add100Gems() { GameData.AddGems(100); }

    public void AddRandomScore() { GameData.AddScore(Random.Range(100, 5000)); }

    public void ResetEverything()
    {
        GameData.ResetAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
