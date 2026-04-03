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
                Emblems emblem = new Emblems();

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
                                emblem.Id = value;
                                break;
                            case "name":
                                emblem.Name = value;
                                break;
                            case "image":
                                emblem.Image = value;
                                break;
                            case "type":
                                emblem.Type = value;
                                break;
                        }
                    }
                }

                emblems.Add(emblem);
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