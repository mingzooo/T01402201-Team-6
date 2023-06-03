using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance 
    { 
        get {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton object");
            }
            return _instance; 
        } 
    }

    public int playerHp;

    private void Awake()
    {
        if (_instance = null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        playerHp = 5;
    }

    public void PlayerDamaged(int damage)
    {
        playerHp -= damage;
    }

}