using UnityEngine;

public class ShopItemSpin : MonoBehaviour
{
    [SerializeField] private Vector3 spinSpeed = new Vector3(0, 60, 0);

    void Update()
    {
        // unscaledDeltaTime taake Time.timeScale = 0 ho tab bhi chale
        transform.Rotate(spinSpeed * Time.unscaledDeltaTime, Space.Self);
    }
}