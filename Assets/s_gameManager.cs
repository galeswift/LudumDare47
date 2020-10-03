using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_gameManager : MonoBehaviour
{
    enum GameState
    { 
        Countdown,
        Playing,
    }

    public GameObject Text_MainHeader;
    public GameObject PlayerSpawner;
    public float PlayScale = 5.0f;
    public float CountdownScale = 2.0f;
    public float BeatsPerMinute = 120.0f;
    public int Tempo = 4;

    public AudioClip MusicClip;
    private GameState m_currentState;
    private float m_flStateStartTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.Countdown);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (m_currentState)
        {
            case GameState.Countdown:
                UpdateCountdown();
                break;
            case GameState.Playing:
                UpdatePlaying();
                break;
        }
    }

    void ChangeState(GameState newState)
    {
        m_currentState = newState;
        m_flStateStartTime = Time.fixedTime;
        EnteredState(newState);
    }

    void EnteredState(GameState newState)
    {
        if (newState == GameState.Playing)
        {
            Text_MainHeader.SetActive(false);
            PlayerSpawner.GetComponent<s_playerspawner>().SpawnAllPlayers();
            if (GetComponent<AudioSource>().clip == null)
            {
                GetComponent<AudioSource>().clip = MusicClip;
                GetComponent<AudioSource>().Play();
            }
        }
        else if (newState == GameState.Countdown)
        {
            Text_MainHeader.SetActive(true);
            PlayerSpawner.GetComponent<s_playerspawner>().DestroyAllPlayers();
        }
    }
    void UpdateCountdown()
    {
        float flCountdownDuration = CountdownScale * Tempo * (60.0f / BeatsPerMinute);
        float flTimeElapsed = Time.fixedTime - m_flStateStartTime;
        float flTimeRemaining = flCountdownDuration - flTimeElapsed;
        if (flTimeRemaining <= 0.0f)
        {
            ChangeState(GameState.Playing);
            m_flStateStartTime += flTimeRemaining;
        }
        Text_MainHeader.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", Mathf.CeilToInt( flTimeRemaining));
    }

    void UpdatePlaying()
    {
        float flCountdownDuration = PlayScale * Tempo * (60.0f / BeatsPerMinute);
        float flTimeElapsed = Time.fixedTime - m_flStateStartTime;
        float flTimeRemaining = flCountdownDuration - flTimeElapsed;
        if (flTimeRemaining <= 0.0f)
        {
            ChangeState(GameState.Countdown);
            m_flStateStartTime += flTimeRemaining;
        }
    }
}
