using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class AttackState : IState
{
    private static AttackState instance = null;

    private AttackState() { }

    public static AttackState GetState()
    {
        if (instance == null) //vérfie l'état actuel du pattern
        {
            instance = new AttackState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy() //peut attaquer l'ennemi ?
    {
        return true;
    }

    public bool GoToBase() //peut rentrer à sa base ?
    {
        return false;
    }

    public bool DefendBase() //peut défendre sa base ?
    {
        return false;
    }
}
