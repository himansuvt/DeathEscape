using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu1 : MonoBehaviour
{
    public GameObject panel;
    public float fadeDuration = 0.5f; // Duration of the fade effect in seconds

    private void Start()
    {
        StartCoroutine(FadeOutPanel());
    }

    private IEnumerator FadeOutPanel()
    {
        yield return new WaitForSeconds(1f); // Wait for 5 seconds

        Image panelImage = panel.GetComponent<Image>();
        Color originalColor = panelImage.color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            panelImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        panel.SetActive(false); // Deactivate the panel after fading
    }
}
