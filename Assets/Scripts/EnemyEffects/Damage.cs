using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : EnemyEffect
{
    private int _damage;

    public Damage(int damage)
    {
        _damage = damage;
    }

    public void Apply(Enemy e)
    {
        e.ReduceHealth(_damage);
        e.RemoveEffect(this);
    }
}
