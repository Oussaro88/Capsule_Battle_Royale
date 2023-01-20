using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class DefendState : IState
{
    private static DefendState instance = null;

    private DefendState() { }

    public static DefendState GetState()
    {
        if (instance == null) //v�rfie l'�tat actuel du pattern
        {
            instance = new DefendState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy() //peut attaquer l'ennemi ?
    {
        return false;
    }

    public bool GoToBase() //peut rentrer � sa base ?
    {
        return false;
    }

    public bool DefendBase() //peut d�fendre sa base ?
    {
        return true;
    }
}
