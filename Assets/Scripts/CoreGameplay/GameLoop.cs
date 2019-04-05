using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private Text _GameText;
    [SerializeField] private Text _TimerText;

    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _AudioClipGood;
    [SerializeField] private AudioClip _AudioClipBad;

    [SerializeField] private float _TimerLimit;
    private float _CurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentTime = _TimerLimit;
        _GameText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if(_CurrentTime > 0.0f)
        {
            _CurrentTime -= Time.deltaTime;
        }
        else
        {
            GameOver();
        }

        _TimerText.text = UnityEngine.Mathf.Round(_CurrentTime).ToString();
    }
    
    public void OnGameWin()
    {
        _GameText.text = "You win \n Your time is : " + UnityEngine.Mathf.Round((_TimerLimit - _CurrentTime)) + " seconds";
        _GameText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        _GameText.text = "You lose";
        _GameText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayGoodSound()
    {
        _AudioSource.clip = _AudioClipGood;
        _AudioSource.Play();
    }

    public void PlayBasSound()
    {
        _AudioSource.clip = _AudioClipBad;
        _AudioSource.Play();
    }
}
