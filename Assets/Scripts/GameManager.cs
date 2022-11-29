using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;
    public Image fadeImage;

    private Blade blade;
    private Spawner spawner;
    

    private int score = 0;

    private void Awake()
    {
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

    }

    public void IncreaseScore()
    {
        score++;

        scoreText.text = "Score: " + score.ToString();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;
        blade.enabled = true;
        spawner.enabled = true;
        
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        
        ClearScene();

    }

    private void Start()
    {
        NewGame();
    }


    public void Explode()
    {
        spawner.enabled = false;
        blade.enabled = false;
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {


            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        
        NewGame();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);
            
            elapsed += Time.unscaledDeltaTime;
            
            yield return null;
        }
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
            
        }

        Bombe[] bombes = FindObjectsOfType<Bombe>();
        foreach (Bombe bombe in bombes)
        {
            Destroy(bombe.gameObject);
        }
    }
}
