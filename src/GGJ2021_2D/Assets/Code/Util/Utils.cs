using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Utils
{
    public static void SetAlpha(Image image, float alpha)
    {
        Color col = image.color;
        col.a = alpha;
        image.color = col;
    }

    public static void SetAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color col = spriteRenderer.color;
        col.a = alpha;
        spriteRenderer.color = col;
    }

    public static void SetAlpha(TextMeshProUGUI tmp, float alpha)
    {
        Color col = tmp.color;
        col.a = alpha;
        tmp.color = col;
    }
}
