using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>

public class RunState : IState
{
    private static RunState instance = null;

    private RunState() { }

    public static RunState GetState()
    {
        if (instance == null) //v�rfie l'�tat actuel du pattern
        {
            instance = new RunState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy() //peut attaquer l'ennemi ?
    {
        return false;
    }

    public bool GoToBase() //peut rentrer � sa base ?
    {
        return true;
    }

    public bool DefendBase() //peut d�fendre sa base ?
    {
        return false;
    }
}
