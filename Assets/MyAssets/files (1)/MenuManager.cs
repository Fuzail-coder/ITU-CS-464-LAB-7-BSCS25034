using UnityEngine;

// Shows one top-level panel at a time.
// Suggested order: 0 MainMenu, 1 Maps, 2 ScoreBoard, 3 Shop, 4 Game placeholder
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private int startPanel = 0;

    void Start() { ShowPanel(startPanel); }

    // Hook any button's OnClick to this and type the panel number
    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == index);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
