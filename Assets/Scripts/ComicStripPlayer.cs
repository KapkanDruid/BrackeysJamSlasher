using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComicStripPlayer : MonoBehaviour
{
    public Image[] comicImages; 
    public float interval = 2.0f; 
    public float fadeDuration = 1.0f; 

    private int currentImageIndex = 0;

    void Start()
    {        
        foreach (var image in comicImages)
        {
            SetAlpha(image, 0);
            image.gameObject.SetActive(false);
        }
        
        StartCoroutine(PlayComicStrip());
    }

    IEnumerator PlayComicStrip()
    {
        while (currentImageIndex < comicImages.Length)
        {
            yield return new WaitForSeconds(interval);

            currentImageIndex++;

            if (currentImageIndex < comicImages.Length)
            {
                StartCoroutine(FadeIn(comicImages[currentImageIndex]));
            }
        }
    }

    IEnumerator FadeIn(Image image)
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetAlpha(image, alpha);
            yield return null;
        }
    }

    void SetAlpha(Image image, float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
