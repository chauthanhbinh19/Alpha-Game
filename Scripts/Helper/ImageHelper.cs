using System;

public static class ImageHelper
{
    private static readonly string[] ImageExtension = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"};
    public static string RemoveImageExtension(string filename){
        foreach(var ext in ImageExtension){
            if(filename.EndsWith(ext, StringComparison.OrdinalIgnoreCase)){
                return filename.Substring(0, filename.Length - ext.Length);
            }
        }
        return filename;
    }
}