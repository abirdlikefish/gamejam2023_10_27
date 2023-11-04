using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySkill
{
    public int skillIndex { get; set; }

    public float skill_speed { get; set; }
    public void skill_move();

    public void skill_grow();

    public void skill_division();

    public void skill_explosion();
}
