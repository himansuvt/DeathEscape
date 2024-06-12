using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    public Text mainText;
    public float fadeDuration = 2f; // Duration of the fade effect in seconds

    private void Start()
    {
        StartCoroutine(FadeOutPanelAndText());
    }

    private IEnumerator FadeOutPanelAndText()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        Image panelImage = panel.GetComponent<Image>();
        Text textComponent = mainText.GetComponent<Text>();

        Color originalPanelColor = panelImage.color;
        Color originalTextColor = textComponent.color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            panelImage.color = new Color(originalPanelColor.r, originalPanelColor.g, originalPanelColor.b, alpha);
            textComponent.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, alpha);
            yield return null;
        }

        panel.SetActive(false); // Deactivate the panel after fading
        mainText.gameObject.SetActive(false); // Deactivate the text after fading
    }
}