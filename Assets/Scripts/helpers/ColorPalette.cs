using UnityEngine;

public static class ColorPalette
{
    public static Color base03 = HexToColor("#002b36");
    public static Color base02 = HexToColor("#073642");
    public static Color base01 = HexToColor("#586e75");
    public static Color base00 = HexToColor("#657b83");
    public static Color base0 = HexToColor("#839496");
    public static Color base1 = HexToColor("#93a1a1");
    public static Color base2 = HexToColor("#eee8d5");
    public static Color base3 = HexToColor("#fdf6e3");
    public static Color yellow = HexToColor("#b58900");
    public static Color orange = HexToColor("#cb4b16");
    public static Color red = HexToColor("#dc322f");
    public static Color magenta = HexToColor("#d33682");
    public static Color violet = HexToColor("#6c71c4");
    public static Color blue = HexToColor("#268bd2");
    public static Color cyan = HexToColor("#2aa198");
    public static Color green = HexToColor("#859900");

    private static Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
