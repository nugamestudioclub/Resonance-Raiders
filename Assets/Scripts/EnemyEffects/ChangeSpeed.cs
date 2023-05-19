using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : EnemyEffect
{

    private float _scale;
    private float _curDuration = 0;
    private float _duration;

    public ChangeSpeed(float scale, float duration)
    {
        _scale = scale;
        _duration = duration;
    }

    public void Apply(Enemy e)
    {
        e.speed = Mathf.Min(e.baseSpeed * _scale, e.speed);
        _curDuration += Time.deltaTime;
        if (_curDuration >= _duration)
        {
            e.speed = e.baseSpeed;
            e.RemoveEffect(this);
        }
    }

}
