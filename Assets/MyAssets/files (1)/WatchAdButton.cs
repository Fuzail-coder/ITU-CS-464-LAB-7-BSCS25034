using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// The green "300 + film icon" button. Fake ad: waits a moment, then gives gems.
public class WatchAdButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int reward = 300;
    [SerializeField] private float fakeAdSeconds = 1f;

    void Awake() { button.onClick.AddListener(OnClick); }
    void OnEnable() { button.interactable = true; }

    void OnClick() { StartCoroutine(FakeAd()); }

    IEnumerator FakeAd()
    {
        button.interactable = false;
        ToastMessage.Show("Watching ad...");
        yield return new WaitForSecondsRealtime(fakeAdSeconds);
        GameData.AddGems(reward);
        ToastMessage.Show("+" + reward + " gems!");
        button.interactable = true;
    }
}
