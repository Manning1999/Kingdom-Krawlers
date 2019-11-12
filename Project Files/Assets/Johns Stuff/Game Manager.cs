using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Title,
        Play,
        Win,
    }

    public int playerLevel = 1;
    public float playerHealthPenalty = 0;
    public int playerCoins;


    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Play;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       switch (m_GameState)
        {
            case GameState.Title:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_GameState = GameState.Play;
                }
                        break;


            case GameState.Play:
                break;

            case GameState.Win:
                break;
        } 
    }
}
