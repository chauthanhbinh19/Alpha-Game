using System;
using System.Collections.Generic;
using System.Text;

public static class JsonHelper
{
    /// <summary>
    /// Chuyển đổi chuỗi JSON của MySQL thành danh sách Emblem (Không dùng thư viện)
    /// </summary>
    public static List<Emblems> DeserializeEmblems(string json)
    {
        List<Emblems> emblems = new List<Emblems>();

        // Kiểm tra các trường hợp chuỗi rỗng hoặc rỗng theo kiểu JSON của MySQL
        if (string.IsNullOrEmpty(json) || json == "[]" || json == "[null]")
        {
            return emblems;
        }

        try
        {
            // Loại bỏ dấu ngoặc vuông ở 2 đầu chuỗi
            string cleanJson = json.Trim('[', ']');

            // Tách chuỗi thành từng object emblem riêng biệt bằng cụm "}, {"
            string[] objectStrings = cleanJson.Split(new string[] { "}, {" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string objStr in objectStrings)
            {
                string cleanObj = objStr.Trim('{', '}');
                Emblems e = new Emblems();

                // Tách từng cặp key-value bằng dấu phẩy
                string[] pairs = cleanObj.Split(new string[] { "\", \"" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string pair in pairs)
                {
                    string cleanPair = pair.Replace("\"", ""); // Bỏ hết dấu ngoặc kép
                    string[] kv = cleanPair.Split(new char[] { ':' }, 2); // Tách key và value

                    if (kv.Length == 2)
                    {
                        string key = kv[0].Trim();
                        string value = kv[1].Trim();

                        switch (key)
                        {
                            case "id":
                                e.Id = value;
                                break;
                            case "name":
                                e.Name = value;
                                break;
                            case "image":
                                e.Image = value;
                                break;
                            case "type":
                                e.Type = value;
                                break;
                        }
                    }
                }

                emblems.Add(e);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonHelper Error]: {ex.Message}");
        }

        return emblems;
    }

    /// <summary>
    /// Chuyển đổi danh sách Emblem thành chuỗi JSON chuẩn (Không dùng thư viện)
    /// </summary>
    public static string SerializeEmblems(List<Emblems> emblems)
    {
        if (emblems == null || emblems.Count == 0)
        {
            return "[]";
        }

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");

        for (int i = 0; i < emblems.Count; i++)
        {
            Emblems e = emblems[i];
            
            // Xây dựng chuỗi JSON thô: {"id":"1","name":"Hero A","image":"img.png","type":"Type A"}
            jsonBuilder.Append("{");
            jsonBuilder.Append($"\"id\":\"{EscapeString(e.Id)}\",");
            jsonBuilder.Append($"\"name\":\"{EscapeString(e.Name)}\",");
            jsonBuilder.Append($"\"image\":\"{EscapeString(e.Image)}\",");
            jsonBuilder.Append($"\"type\":\"{EscapeString(e.Type)}\"");
            jsonBuilder.Append("}");

            // Nếu không phải phần tử cuối cùng thì thêm dấu phẩy
            if (i < emblems.Count - 1)
            {
                jsonBuilder.Append(",");
            }
        }

        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }

    /// <summary>
    /// Chuyển đổi chuỗi JSON của MySQL thành danh sách Class (Không dùng thư viện)
    /// </summary>
    public static List<Classes> DeserializeClasses(string json)
    {
        List<Classes> classes = new List<Classes>();

        // Kiểm tra các trường hợp chuỗi rỗng hoặc rỗng theo kiểu JSON của MySQL
        if (string.IsNullOrEmpty(json) || json == "[]" || json == "[null]")
        {
            return classes;
        }

        try
        {
            // Loại bỏ dấu ngoặc vuông ở 2 đầu chuỗi
            string cleanJson = json.Trim('[', ']');

            // Tách chuỗi thành từng object emblem riêng biệt bằng cụm "}, {"
            string[] objectStrings = cleanJson.Split(new string[] { "}, {" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string objStr in objectStrings)
            {
                string cleanObj = objStr.Trim('{', '}');
                Classes c = new Classes();

                // Tách từng cặp key-value bằng dấu phẩy
                string[] pairs = cleanObj.Split(new string[] { "\", \"" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string pair in pairs)
                {
                    string cleanPair = pair.Replace("\"", ""); // Bỏ hết dấu ngoặc kép
                    string[] kv = cleanPair.Split(new char[] { ':' }, 2); // Tách key và value

                    if (kv.Length == 2)
                    {
                        string key = kv[0].Trim();
                        string value = kv[1].Trim();

                        switch (key)
                        {
                            case "id":
                                c.Id = value;
                                break;
                            case "sub_type":
                                c.SubType = value;
                                break;
                            case "sub_image":
                                c.SubImage = value;
                                break;
                            case "main_type":
                                c.MainType = value;
                                break;
                            case "main_image":
                                c.MainImage = value;
                                break;
                        }
                    }
                }

                classes.Add(c);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonHelper Error]: {ex.Message}");
        }

        return classes;
    }

    /// <summary>
    /// Chuyển đổi danh sách Class thành chuỗi JSON chuẩn (Không dùng thư viện)
    /// </summary>
    public static string SerializeClasses(List<Classes> classes)
    {
        if (classes == null || classes.Count == 0)
        {
            return "[]";
        }

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");

        for (int i = 0; i < classes.Count; i++)
        {
            Classes c = classes[i];
            
            // Xây dựng chuỗi JSON thô: {"id":"1","name":"Hero A","image":"img.png","type":"Type A"}
            jsonBuilder.Append("{");
            jsonBuilder.Append($"\"id\":\"{EscapeString(c.Id)}\",");
            jsonBuilder.Append($"\"name\":\"{EscapeString(c.SubType)}\",");
            jsonBuilder.Append($"\"image\":\"{EscapeString(c.SubImage)}\",");
            jsonBuilder.Append($"\"image\":\"{EscapeString(c.MainType)}\",");
            jsonBuilder.Append($"\"type\":\"{EscapeString(c.MainImage)}\"");
            jsonBuilder.Append("}");

            // Nếu không phải phần tử cuối cùng thì thêm dấu phẩy
            if (i < classes.Count - 1)
            {
                jsonBuilder.Append(",");
            }
        }

        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    /// <summary>
    /// Hàm phụ giúp xử lý các ký tự đặc biệt để chuỗi JSON sinh ra không bị lỗi
    /// </summary>
    private static string EscapeString(string str)
    {
        if (string.IsNullOrEmpty(str)) return "";
        
        return str.Replace("\\", "\\\\") // Xử lý dấu gạch chéo ngược
                  .Replace("\"", "\\\"") // Xử lý dấu nháy kép
                  .Replace("\n", "\\n")   // Xử lý xuống dòng
                  .Replace("\r", "\\r");
    }
}