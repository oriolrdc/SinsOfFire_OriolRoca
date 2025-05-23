using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    //Victoria
    [SerializeField] private Animator _animatorBosque;
    [SerializeField] private Animator _animatorDiamantes;
    [SerializeField] private Animator _animatorEnemigos;
    [SerializeField] private Animator _animatorLinea1;
    [SerializeField] private Animator _animatorLinea2;
    [SerializeField] private Animator _animatorCompletado;
    //Managers
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Text _diamantesTXT;
    [SerializeField] private Text _enemigosTXT;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Diamantes()
    {
        _animatorDiamantes.SetTrigger("Diamantes");
    }

    public void Enemigos()
    {
        _animatorEnemigos.SetTrigger("Enemigos");
    }

    public void Linea1()
    {
        _animatorLinea1.SetTrigger("Linea");
    }

    public void Linea2()
    {
        _animatorLinea2.SetTrigger("Linea");
    }

    public void Completado()
    {
        _animatorCompletado.SetTrigger("Completado");
    }

    public void SumarDiamantes()
    {
        _diamantesTXT.text = "0" + _gameManager._diamonds.ToString();
    }

    public void SumarEnemigos()
    {
        _enemigosTXT.text = "0" + _gameManager._Kills.ToString();
    }

}
