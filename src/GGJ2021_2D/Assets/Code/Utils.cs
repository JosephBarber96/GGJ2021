using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static void SetImageAlpha(Image image, float alpha)
    {
        Color col = image.color;
        col.a = alpha;
        image.color = col;
    }
}
