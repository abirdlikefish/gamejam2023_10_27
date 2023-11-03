using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySkill
{
    public int skillIndex { get; set; }

    public float skill_speed { get; set; }
    public void skill_move();

    public float skill_growSpeed{ get; set; }
    public void skill_grow();

    public float skill_divisionSize { get; set; }
    public void skill_division();

    public float skill_explosionRange { get; set; }
    public void skill_explosion();
}
