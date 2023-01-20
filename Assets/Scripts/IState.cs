using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public interface IState
{
    bool CanAttackEnemy();
    bool GoToBase();
    bool DefendBase();
}

