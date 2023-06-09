using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Duel duel;

    private void Awake()
    {
        duel = GetComponent<Duel>();
    }
    void Start()
    {
        duel.startDuel = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
