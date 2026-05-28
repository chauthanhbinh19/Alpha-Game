using System;
using System.Globalization;
using System.Text;


public class StringConverter
{
    public static string SnakeCaseToTitleCase(string snakeCase)
    {
        if (string.IsNullOrEmpty(snakeCase)) return string.Empty;

        // Tách chuỗi bằng ký tự '_'
        string[] words = snakeCase.Split('_');
        StringBuilder titleCase = new StringBuilder();

        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                // Viết hoa chữ cái đầu, phần còn lại viết thường
                titleCase.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()));
                titleCase.Append(" "); // Thêm dấu cách
            }
        }

        // Loại bỏ khoảng trắng thừa ở cuối
        return titleCase.ToString().TrimEnd();
    }
}
