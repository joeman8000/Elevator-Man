using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static int _amountofEnemies;
    
    public void SetEnemiesAmount(int x)
    {
        _amountofEnemies = x;
    }

    public bool EnemyDied()
    {
        --_amountofEnemies;
        if(_amountofEnemies == 0)
        {
            return true;
        }
        return false;
    }
    public int AmountofEnemies()
    {
        return _amountofEnemies;
    }
}
