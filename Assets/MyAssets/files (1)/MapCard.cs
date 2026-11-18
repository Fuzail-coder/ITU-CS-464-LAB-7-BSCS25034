using TMPro;
using UnityEngine;
using UnityEngine.UI;

// One per map card (Original, Forest, Neon City).
public class MapCard : MonoBehaviour
{
    public string mapId = "Original";   // unique: Original / Forest / NeonCity
    public int price = 0;               // 0 / 150 / 350

    public Button cardButton;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject priceTag;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject selectedBadge;

    public void Refresh()
    {
        bool unlocked = GameData.IsUnlocked(mapId);
        if (lockIcon != null) lockIcon.SetActive(!unlocked);
        if (priceTag != null) priceTag.SetActive(!unlocked);
        if (priceText != null) priceText.text = price.ToString();
        if (selectedBadge != null) selectedBadge.SetActive(unlocked && GameData.SelectedMap == mapId);
    }
}
