using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyEffect
{
    /**
     * Function that runs every tick on an Enemy
     * */
    void Apply(Enemy e);

}
