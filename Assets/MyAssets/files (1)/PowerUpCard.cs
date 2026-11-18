using TMPro;
using UnityEngine;
using UnityEngine.UI;

// One per power-up card (Shield, Magnet, X2 Boost).
public class PowerUpCard : MonoBehaviour
{
    [SerializeField] private string powerUpId = "Shield";
    [SerializeField] private int price = 100;

    [SerializeField] private Button buyButton;     // the blue "diamond 100" button
    [SerializeField] private TMP_Text ownText;     // "OWN: 0"
    [SerializeField] private TMP_Text priceText;   // "100"

    void Awake() { buyButton.onClick.AddListener(Buy); }

    void OnEnable() { Refresh(); }

    void Buy()
    {
        if (GameData.SpendGems(price))
        {
            GameData.AddPowerUp(powerUpId, 1);
            ToastMessage.Show(powerUpId + " purchased!");
        }
        else
        {
            ToastMessage.Show("Not enough gems!");
        }
        Refresh();
    }

    void Refresh()
    {
        ownText.text = "OWN: " + GameData.GetPowerUps(powerUpId);
        priceText.text = price.ToString();
    }
}
