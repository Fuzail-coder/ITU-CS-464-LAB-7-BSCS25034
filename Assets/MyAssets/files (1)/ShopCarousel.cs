using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CarouselItem
{
    public string id;               // unique, e.g. "Neon", "Soccer"
    public int price;               // 0 = free / owned from start
    public RectTransform view;      // the item image
    public GameObject lockIcon;     // child lock image
    public GameObject selectedBadge;// child green "SELECTED" badge
}

// Used for BOTH the Balls panel and the Tracks panel (set 'category' differently).
public class ShopCarousel : MonoBehaviour
{
    [SerializeField] private string category = "Ball";   // "Ball" or "Track"
    [SerializeField] private CarouselItem[] items;

    [Header("Look")]
    [SerializeField] private float spacing = 220f;
    [SerializeField] private float centerScale = 1.5f;
    [SerializeField] private float sideScale = 1f;
    [SerializeField] private float moveSpeed = 12f;

    [Header("Controls")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button actionButton;        // gold PICK / PICKED / price button
    [SerializeField] private TMP_Text actionLabel;
    [SerializeField] private GameObject diamondIcon;     // diamond next to the price

    private int index;

    void Awake()
    {
        prevButton.onClick.AddListener(() => Move(-1));
        nextButton.onClick.AddListener(() => Move(1));
        actionButton.onClick.AddListener(OnActionPressed);
    }

    void OnEnable()
    {
        // start on the currently selected item
        string selected = GameData.GetSelected(category, items[0].id);
        index = 0;
        for (int i = 0; i < items.Length; i++)
            if (items[i].id == selected) index = i;

        Layout(1f);   // snap, no animation
        Refresh();
    }

    void Update() { Layout(1f - Mathf.Exp(-moveSpeed * Time.unscaledDeltaTime)); }

    void Layout(float t)
    {
        for (int i = 0; i < items.Length; i++)
        {
            RectTransform v = items[i].view;
            float offset = i - index;
            float scale = (i == index) ? centerScale : sideScale;
            v.anchoredPosition = Vector2.Lerp(v.anchoredPosition, new Vector2(offset * spacing, 0f), t);
            v.localScale = Vector3.Lerp(v.localScale, Vector3.one * scale, t);
        }
    }

    void Move(int dir)
    {
        index = Mathf.Clamp(index + dir, 0, items.Length - 1);
        Refresh();
    }

    void OnActionPressed()
    {
        CarouselItem cur = items[index];

        if (IsUnlocked(cur))
        {
            GameData.SetSelected(category, cur.id);
        }
        else if (GameData.SpendGems(cur.price))
        {
            GameData.Unlock(category, cur.id);
            GameData.SetSelected(category, cur.id);
            ToastMessage.Show("Unlocked!");
        }
        else
        {
            ToastMessage.Show("Not enough gems!");
        }
        Refresh();
    }

    bool IsUnlocked(CarouselItem item)
    {
        return GameData.IsUnlocked(category, item.id, item.price <= 0);
    }

    void Refresh()
    {
        string selected = GameData.GetSelected(category, items[0].id);

        foreach (CarouselItem item in items)
        {
            bool unlocked = IsUnlocked(item);
            if (item.lockIcon != null) item.lockIcon.SetActive(!unlocked);
            if (item.selectedBadge != null) item.selectedBadge.SetActive(unlocked && item.id == selected);
        }

        CarouselItem cur = items[index];
        if (IsUnlocked(cur))
        {
            actionLabel.text = (cur.id == selected) ? "PICKED" : "PICK";
            if (diamondIcon != null) diamondIcon.SetActive(false);
        }
        else
        {
            actionLabel.text = cur.price.ToString();
            if (diamondIcon != null) diamondIcon.SetActive(true);
        }

        prevButton.interactable = index > 0;
        nextButton.interactable = index < items.Length - 1;
    }
}
