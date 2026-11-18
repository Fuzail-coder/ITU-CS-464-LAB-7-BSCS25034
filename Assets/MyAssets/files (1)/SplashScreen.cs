using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private float duration = 5f;
    [SerializeField] private int nextPanelIndex = 1;   // Main Menu 1

    System.Collections.IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        menuManager.ShowPanel(nextPanelIndex);
    }
}