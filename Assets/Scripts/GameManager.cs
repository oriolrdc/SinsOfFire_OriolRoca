using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _diamonds = 0;
    public Text diamondsText;
    private int _Kills = 0;
    public Text KillsText;

    void Awake()
    {

    }

    void Start()
    {
        diamondsText.text = "0" + _diamonds.ToString();
        KillsText.text = "0" + _Kills.ToString();
    }

    public void AddDiamonds()
    {
        _diamonds++;
        diamondsText.text = "0" + _diamonds.ToString();
    }

    public void Kills()
    {
        _Kills++;
        KillsText.text = "0" + _Kills.ToString();
    }
}
