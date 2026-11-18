using UnityEngine;

// On the Shop object. Switches between Balls / Power Ups / Tracks.
public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tabPanels;   // 0 Balls, 1 Powerups, 2 Tracks
    [SerializeField] private int defaultTab = 0;

    void OnEnable() { ShowTab(defaultTab); }

    // Hook tab buttons: Balls -> 0, Power Ups -> 1, Tracks -> 2
    public void ShowTab(int index)
    {
        for (int i = 0; i < tabPanels.Length; i++)
            tabPanels[i].SetActive(i == index);
    }
}
