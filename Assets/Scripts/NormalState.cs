using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>

public class NormalState : IState
{
    private static NormalState instance = null;

    private NormalState() { }

    public static NormalState GetState()
    {
        if (instance == null) //v�rifie l'�tat actuel du pattern
        {
            instance = new NormalState(); //fait une instance du pattern
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
        return false;
    }
}
