using UnityEngine;

public class BallCarousel : MonoBehaviour
{
    [SerializeField] private float spacing = 150f;      // balls ke beech distance
    [SerializeField] private float selectedScale = 1.5f; // beech wala ball kitna bada
    [SerializeField] private float smooth = 10f;         // movement ki speed

    private RectTransform[] items;
    private int center;

    void Start()
    {
        items = new RectTransform[transform.childCount];
        for (int i = 0; i < items.Length; i++)
            items[i] = (RectTransform)transform.GetChild(i);

        center = PlayerPrefs.GetInt("SelectedBall", 0) % items.Length;
    }

    // Right arrow
    public void Next()
    {
        center = (center + 1) % items.Length;
        PlayerPrefs.SetInt("SelectedBall", center);
    }

    // Left arrow
    public void Prev()
    {
        center = (center - 1 + items.Length) % items.Length;
        PlayerPrefs.SetInt("SelectedBall", center);
    }

    void Update()
    {
        int n = items.Length;
        for (int i = 0; i < n; i++)
        {
            int offset = i - center;
            if (offset > n / 2) offset -= n;
            else if (offset < -n / 2) offset += n;

            Vector2 targetPos = new Vector2(offset * spacing, 0f);
            float s = (offset == 0) ? selectedScale : 1f;

            items[i].anchoredPosition = Vector2.Lerp(items[i].anchoredPosition, targetPos, Time.deltaTime * smooth);
            items[i].localScale = Vector3.Lerp(items[i].localScale, Vector3.one * s, Time.deltaTime * smooth);
        }
    }
}