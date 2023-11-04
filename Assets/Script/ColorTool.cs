using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorTool
{
    public static Color IndexToColor(int index)
    {
        Color color = new Color();
        if(((index >> 0) & 1) == 1)
        {
            color.r = 1;
        }
        else
        {
            color.r = 0;
        }

        if(((index >> 1) & 1) == 1)
        {
            color.g = 1;
        }
        else
        {
            color.g = 0;
        }

        if(((index >> 2) & 1) == 1)
        {
            color.b = 1;
        }
        else
        {
            color.b = 0;
        }
        color.a = 1;
        
        return color;
    }
}
