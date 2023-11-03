using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColor
{
    public SpriteRenderer spriteRenderer { get; set; }
    public int colorIndex { get; set; }
}
