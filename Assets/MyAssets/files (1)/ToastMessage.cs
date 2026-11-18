using System.Collections;
using TMPro;
using UnityEngine;

// One message label for the whole UI ("Not enough gems!", "Unlocked!").
// Put on an always-active object (MainCanvas). Call ToastMessage.Show("text") from anywhere.
public class ToastMessage : MonoBehaviour
{
    public static ToastMessage Instance { get; private set; }

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float duration = 1.5f;

    void Awake()
    {
        Instance = this;
        messageText.text = "";
    }

    public static void Show(string msg)
    {
        if (Instance == null) { Debug.Log(msg); return; }
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(Instance.Routine(msg));
    }

    IEnumerator Routine(string msg)
    {
        messageText.text = msg;
        yield return new WaitForSecondsRealtime(duration);
        messageText.text = "";
    }
}
