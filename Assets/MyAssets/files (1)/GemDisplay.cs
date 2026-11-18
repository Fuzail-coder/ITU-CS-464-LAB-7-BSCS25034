using TMPro;
using UnityEngine;

// Put on every gem counter (Maps, Shop...). Updates live when gems change.
public class GemDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text gemText;

    void OnEnable()
    {
        GameData.OnGemsChanged += Refresh;
        Refresh();
    }

    void OnDisable() { GameData.OnGemsChanged -= Refresh; }

    void Refresh() { gemText.text = GameData.Gems.ToString(); }
}
