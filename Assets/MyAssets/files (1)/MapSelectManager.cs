using UnityEngine;

// On the Maps panel. Rename this file to MapSelectManager.cs (replace the old one).
// Locked map rules:
//   - not enough gems  -> "Not enough gems!" and nothing is bought
//   - enough gems      -> first tap asks to confirm, second tap unlocks (if Require Confirm is on)
public class MapSelectManager : MonoBehaviour
{
    [SerializeField] private MapCard[] cards;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private int gamePanelIndex = 5;      // placeholder "Game" panel
    [SerializeField] private bool requireConfirm = true;  // tap twice to buy
    [SerializeField] private float confirmWindow = 3f;    // seconds to confirm

    private MapCard pendingCard;
    private float pendingTime;

    void Awake()
    {
        foreach (MapCard card in cards)
        {
            MapCard c = card;   // local copy for the lambda
            c.cardButton.onClick.AddListener(() => OnCardClicked(c));
        }
    }

    void OnEnable()
    {
        pendingCard = null;
        RefreshAll();
    }

    void OnCardClicked(MapCard card)
    {
        // Unlocked map: just select it
        if (GameData.IsUnlocked(card.mapId))
        {
            GameData.SelectedMap = card.mapId;
            pendingCard = null;
            RefreshAll();
            return;
        }

        // Locked map: need enough gems first
        if (GameData.Gems < card.price)
        {
            ToastMessage.Show("Not enough gems! Need " + card.price);
            pendingCard = null;
            return;
        }

        // Enough gems: ask for a second, deliberate tap
        bool confirmed = pendingCard == card && Time.unscaledTime - pendingTime <= confirmWindow;
        if (requireConfirm && !confirmed)
        {
            pendingCard = card;
            pendingTime = Time.unscaledTime;
            ToastMessage.Show("Tap again to unlock for " + card.price + " gems");
            return;
        }

        // Buy it
        if (GameData.SpendGems(card.price))
        {
            GameData.Unlock(card.mapId);
            GameData.SelectedMap = card.mapId;
            ToastMessage.Show("Unlocked!");
        }
        pendingCard = null;
        RefreshAll();
    }

    void RefreshAll()
    {
        foreach (MapCard c in cards) c.Refresh();
    }

    // Hook to the PLAY button on the Maps screen
    public void OnPlayPressed()
    {
        Debug.Log("Selected map: " + GameData.SelectedMap);
        menuManager.ShowPanel(gamePanelIndex);
    }
}