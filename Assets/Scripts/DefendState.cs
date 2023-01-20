using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class DefendState : IState
{
    private static DefendState instance = null;

    private DefendState() { }

    public static DefendState GetState()
    {
        if (instance == null) //vérfie l'état actuel du pattern
        {
            instance = new DefendState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy() //peut attaquer l'ennemi ?
    {
        return false;
    }

    public bool GoToBase() //peut rentrer à sa base ?
    {
        return false;
    }

    public bool DefendBase() //peut défendre sa base ?
    {
        return true;
    }
}
