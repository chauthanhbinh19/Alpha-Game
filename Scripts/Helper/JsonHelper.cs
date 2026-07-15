using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class FlatEffectArrayWrapper
{
    public List<FlatEffectDTO> items;
}

public static class JsonHelper
{
    /// <summary>
    /// Chuyển đổi chuỗi JSON của MySQL thành danh sách Emblem (Không dùng thư viện)
    /// </summary>
    public static List<Emblems> DeserializeEmblems(string json)
    {
        List<Emblems> emblemsList = new List<Emblems>();

        // 1. Kiểm tra nhanh chuỗi rỗng từ MySQL
        if (string.IsNullOrEmpty(json)) return emblemsList;

        json = json.Trim();
        if (json == "[]" || json == "[null]" || json == "")
        {
            return emblemsList;
        }

        try
        {
            // 2. Sử dụng Newtonsoft để parse thẳng mảng JSON thành List DTO
            // Tự động bỏ qua lỗi null thừa từ JSON_ARRAYAGG
            List<FlatEmblemDTO> dtoList = JsonConvert.DeserializeObject<List<FlatEmblemDTO>>(json);

            if (dtoList == null) return emblemsList;

            foreach (var dto in dtoList)
            {
                if (dto == null) continue;

                // 3. Ánh xạ trực tiếp sang đối tượng Emblems thực tế của game
                Emblems e = new Emblems
                {
                    Id = dto.id,
                    Name = dto.name,
                    Image = dto.image,
                    Type = dto.type
                };

                emblemsList.Add(e);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Deserialize Emblems Error]: {ex.Message}\n{ex.StackTrace}");
        }

        return emblemsList;
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

        try
        {
            // 1. Ánh xạ danh sách Emblems thực tế sang danh sách FlatEmblemDTO trung gian
            List<FlatEmblemDTO> dtoList = new List<FlatEmblemDTO>();

            foreach (var e in emblems)
            {
                if (e == null) continue;

                FlatEmblemDTO dto = new FlatEmblemDTO
                {
                    id = e.Id,
                    name = e.Name,
                    image = e.Image,
                    type = e.Type
                };

                dtoList.Add(dto);
            }

            // 2. Chuyển đổi List DTO thành chuỗi JSON chuẩn chỉ trong một nốt nhạc
            // Sử dụng Formatting.None để chuỗi JSON nhẹ nhất, tối ưu khi lưu xuống Database MySQL
            return JsonConvert.SerializeObject(dtoList, Formatting.None);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Serialize Emblems Error]: {ex.Message}\n{ex.StackTrace}");
            return "[]";
        }
    }

    /// <summary>
    /// Chuyển đổi chuỗi JSON của MySQL thành danh sách Class (Không dùng thư viện)
    /// </summary>
    public static Classes DeserializeClasses(string json)
    {
        // Trả về object rỗng nếu chuỗi null hoặc không có dữ liệu
        if (string.IsNullOrEmpty(json)) return new Classes();

        string cleanJson = json.Trim();
        if (cleanJson == "[]" || cleanJson == "[null]" || cleanJson == "")
        {
            return new Classes();
        }

        try
        {
            FlatClassDTO dto = null;

            // 1. Phân tích cú pháp JSON bằng JToken để tự động xử lý cả dạng mảng lẫn object lẻ
            JToken token = JToken.Parse(cleanJson);

            if (token is JArray array)
            {
                // Nếu MySQL trả về dạng mảng, bóc lấy phần tử đầu tiên hợp lệ
                if (array.Count > 0 && array[0].Type != JTokenType.Null)
                {
                    dto = array[0].ToObject<FlatClassDTO>();
                }
            }
            else if (token is JObject obj)
            {
                // Nếu là đối tượng lẻ, parse trực tiếp luôn
                dto = obj.ToObject<FlatClassDTO>();
            }

            if (dto == null) return new Classes();

            // 2. Ánh xạ dữ liệu từ DTO sang class Classes thực tế của game
            Classes c = new Classes
            {
                Id = dto.id,
                SubType = dto.sub_type,
                SubImage = dto.sub_image,
                MainType = dto.main_type,
                MainImage = dto.main_image,

                // Sử dụng toán tử ?? để gán giá trị mặc định nếu trường đó bị null hoặc không tồn tại
                MovementRange = dto.movement_range ?? 2,
                MovementPoint = dto.movement_point ?? 4,
                AttackRange = dto.attack_range ?? 4
            };

            return c;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Deserialize Classes Error]: {ex.Message}\n{ex.StackTrace}");
            return new Classes();
        }
    }

    /// <summary>
    /// Chuyển đổi danh sách Class thành chuỗi JSON chuẩn (Không dùng thư viện)
    /// </summary>
    public static string SerializeClasses(Classes c)
    {
        if (c == null)
        {
            return "{}";
        }

        try
        {
            // 1. Ánh xạ từ đối tượng Classes của game sang FlatClassDTO trung gian
            FlatClassDTO dto = new FlatClassDTO
            {
                id = c.Id,
                sub_type = c.SubType,
                sub_image = c.SubImage,
                main_type = c.MainType,
                main_image = c.MainImage,
                movement_range = c.MovementRange,
                movement_point = c.MovementPoint,
                attack_range = c.AttackRange
            };

            // 2. Serialize DTO thành chuỗi JSON chuẩn chỉ trong 1 dòng
            // Sử dụng Formatting.None để chuỗi JSON gọn nhất khi lưu vào Database
            return JsonConvert.SerializeObject(dto, Formatting.None);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Serialize Classes Error]: {ex.Message}\n{ex.StackTrace}");
            return "{}";
        }
    }

    /// <summary>
    /// Chuyển đổi chuỗi JSON phẳng từ MySQL thành List<Effects> (gồm đầy đủ Object con)
    /// </summary>
    // public static List<Effects> DeserializeEffects(string json)
    // {
    //     List<Effects> effectsList = new List<Effects>();

    //     // Kiểm tra điều kiện chuỗi rỗng của MySQL JSON
    //     if (string.IsNullOrEmpty(json) || json == "[]" || json == "[null]")
    //     {
    //         return effectsList;
    //     }

    //     try
    //     {
    //         // Bước 1: Trích xuất các cụm dữ liệu nằm trong cặp dấu ngoặc nhọn { ... }
    //         // Sử dụng Regex để bắt chính xác các Object JSON không bị lẫn dấu phẩy bên ngoài
    //         MatchCollection matches = Regex.Matches(json, @"\{([^}]+)\}");

    //         foreach (Match match in matches)
    //         {
    //             string cleanObj = match.Groups[1].Value;

    //             Effects effect = new Effects();
    //             EffectProperty effectProperty = new EffectProperty();
    //             EffectAction effectAction = new EffectAction();

    //             // Bước 2: Tách các cặp Key-Value qua Regex để xử lý an toàn thay vì dùng Split mặc định
    //             // Biểu thức này bắt cặp dạng "key":value hoặc "key":"value"
    //             MatchCollection kvPairs = Regex.Matches(cleanObj, @"\""([^\""]+)\""\s*:\s*([^,]+)");

    //             foreach (Match kv in kvPairs)
    //             {
    //                 string key = kv.Groups[1].Value.Trim();
    //                 // Loại bỏ các ký tự bọc ngoặc kép của phần Value nếu có
    //                 string value = kv.Groups[2].Value.Trim().Trim('"');

    //                 if (value == "null") continue;

    //                 switch (key)
    //                 {
    //                     // --- Thuộc tính của Effects ---
    //                     case "effect_id":
    //                         // Nếu ID trong DB là chuỗi (ví dụ 'EC20EP50'), hãy đổi kiểu dữ liệu Id trong Class sang string. 
    //                         // Nếu ID là int, sử dụng int.TryParse.
    //                         if (int.TryParse(value, out int id)) effect.Id = id;
    //                         break;
    //                     case "effect_name":
    //                         effect.Name = value;
    //                         break;
    //                     case "effect_type":
    //                         effect.EffectType = value;
    //                         break;
    //                     case "effect_description":
    //                         effect.Description = value;
    //                         break;
    //                     case "duration":
    //                         if (int.TryParse(value, out int dur)) effect.Duration = dur;
    //                         break;
    //                     case "value_type":
    //                         effect.ValueType = value;
    //                         break;
    //                     case "value":
    //                         if (int.TryParse(value, out int val)) effect.Value = val;
    //                         break;
    //                     case "scaling_factor":
    //                         if (float.TryParse(value, out float scale)) effect.ScalingFactor = scale;
    //                         break;

    //                     // --- Thuộc tính của SkillEffect ---
    //                     case "min_value":
    //                         if (int.TryParse(value, out int minValue)) effect.MinValue = minValue;
    //                         break;
    //                     case "max_value":
    //                         if (int.TryParse(value, out int maxValue)) effect.MaxValue = maxValue;
    //                         break;
    //                     case "trigger_phase":
    //                         effect.TriggerPhase = value;
    //                         break;
    //                     case "trigger_condition":
    //                         effect.TriggerCondition = value;
    //                         break;
    //                     case "is_stackable":
    //                         if (bool.TryParse(value, out bool isStackableValue)) effect.IsStackable = isStackableValue;
    //                         break;
    //                     case "is_removable":
    //                         if (bool.TryParse(value, out bool isRemovableValue)) effect.IsRemovable = isRemovableValue;
    //                         break;
    //                     case "target_id":
    //                         effect.Target.Id = value;
    //                         break;

    //                     // --- Thuộc tính của EffectProperty ---
    //                     case "property_id":
    //                         if (int.TryParse(value, out int pId)) effectProperty.PropertyId = pId;
    //                         break;
    //                     case "property_code":
    //                         effectProperty.PropertyCode = value;
    //                         break;
    //                     case "property_name":
    //                         effectProperty.PropertyName = value;
    //                         break;
    //                     case "property_description":
    //                         effectProperty.Description = value;
    //                         break;

    //                     // --- Thuộc tính của EffectAction ---
    //                     case "action_id":
    //                         if (int.TryParse(value, out int aId)) effectAction.ActionId = aId;
    //                         break;
    //                     case "action_code":
    //                         effectAction.ActionCode = value;
    //                         break;
    //                     case "action_name":
    //                         effectAction.ActionName = value;
    //                         break;
    //                     case "action_description":
    //                         effectAction.Description = value;
    //                         break;
    //                 }
    //             }

    //             // Gán các Object con vào Object Effects chính sau khi lọc xong dữ liệu của bản ghi đó
    //             effect.EffectProperty = effectProperty;
    //             effect.EffectAction = effectAction;

    //             effectsList.Add(effect);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"[JsonHelper Error]: {ex.Message}");
    //     }

    //     return effectsList;
    // }

    public static List<Effects> DeserializeEffects(string json)
    {
        List<Effects> effectsList = new List<Effects>();

        // 1. Kiểm tra nhanh chuỗi rỗng từ MySQL
        if (string.IsNullOrEmpty(json)) return effectsList;

        json = json.Trim();
        if (json == "[]" || json == "[null]" || json == "")
        {
            return effectsList;
        }

        try
        {
            // 2. Sử dụng Newtonsoft.Json để parse thẳng mảng JSON sang List<FlatEffectDTO>
            // Không cần bọc chuỗi "items", không cần class Wrapper trung gian nữa!
            List<FlatEffectDTO> dtoList = JsonConvert.DeserializeObject<List<FlatEffectDTO>>(json);

            if (dtoList == null) return effectsList;

            foreach (var dto in dtoList)
            {
                if (dto == null) continue;

                // 3. Khởi tạo và ánh xạ trực tiếp sang đối tượng Effects
                Effects effect = new Effects
                {
                    // Newtonsoft tự động xử lý ép kiểu int -> string hoặc int -> int mượt mà
                    Id = dto.effect_id,
                    Name = dto.effect_name,
                    EffectType = dto.effect_type,
                    Description = dto.effect_description,
                    Duration = dto.duration,
                    ValueType = dto.value_type,
                    Value = dto.value,
                    ScalingFactor = dto.scaling_factor,
                    MinValue = dto.min_value,
                    MaxValue = dto.max_value,
                    TriggerPhase = dto.trigger_phase,
                    TriggerCondition = dto.trigger_condition,

                    // Newtonsoft tự chuyển đổi số 1/0 từ MySQL sang true/false nếu FlatEffectDTO của bạn khai báo là bool!
                    IsStackable = dto.is_stackable == 1,
                    IsRemovable = dto.is_removable == 1
                };

                // 4. Khởi tạo an toàn các nested class (đối tượng con lồng nhau)
                effect.Target = new Targets()
                {
                    Id = dto.target_id
                };

                effect.EffectProperty = new EffectProperty
                {
                    PropertyId = dto.property_id,
                    PropertyCode = dto.property_code,
                    PropertyName = dto.property_name,
                    Description = dto.property_description
                };

                effect.EffectAction = new EffectAction
                {
                    ActionId = dto.action_id,
                    ActionCode = dto.action_code,
                    ActionName = dto.action_name,
                    Description = dto.action_description
                };

                effectsList.Add(effect);
            }
        }
        catch (Exception ex)
        {
            // Newtonsoft cung cấp StackTrace rất rõ ràng khi parse lỗi (ví dụ lỗi sai định dạng ở dòng nào, cột nào)
            Debug.LogError($"[Newtonsoft.Json Error]: {ex.Message}\n{ex.StackTrace}");
        }

        return effectsList;
    }
    /// <summary>
    /// Chuyển đổi List<Effects> ngược lại thành chuỗi JSON chuẩn (Không dùng thư viện)
    /// </summary>
    public static string SerializeEffects(List<Effects> effectsList)
    {
        if (effectsList == null || effectsList.Count == 0)
        {
            return "[]";
        }

        try
        {
            // 1. Ánh xạ ngược danh sách Effects thành danh sách Flat (giống cấu trúc MySQL nhận vào)
            List<FlatEffectDTO> dtoList = new List<FlatEffectDTO>();

            foreach (var effect in effectsList)
            {
                if (effect == null) continue;

                FlatEffectDTO dto = new FlatEffectDTO
                {
                    effect_id = effect.Id,
                    effect_name = effect.Name,
                    effect_type = effect.EffectType,
                    effect_description = effect.Description,
                    duration = effect.Duration,
                    value_type = effect.ValueType,
                    value = effect.Value,
                    scaling_factor = effect.ScalingFactor,
                    min_value = effect.MinValue,
                    max_value = effect.MaxValue,
                    trigger_phase = effect.TriggerPhase,
                    trigger_condition = effect.TriggerCondition,

                    // Nếu FlatEffectDTO khai báo là int thì ép (effect.IsStackable ? 1 : 0), 
                    // còn nếu đã đổi sang bool thì chỉ cần gán thẳng!
                    is_stackable = effect.IsStackable ? 1 : 0,
                    is_removable = effect.IsRemovable ? 1 : 0,

                    // Lấy thông tin từ các nested object nếu có
                    target_id = effect.Target?.Id,

                    property_id = effect.EffectProperty != null ? effect.EffectProperty.PropertyId : "",
                    property_code = effect.EffectProperty?.PropertyCode,
                    property_name = effect.EffectProperty?.PropertyName,
                    property_description = effect.EffectProperty?.Description,

                    action_id = effect.EffectAction != null ? effect.EffectAction.ActionId : "",
                    action_code = effect.EffectAction?.ActionCode,
                    action_name = effect.EffectAction?.ActionName,
                    action_description = effect.EffectAction?.Description
                };

                dtoList.Add(dto);
            }

            // 2. Chuyển list DTO thành chuỗi JSON cực nhanh với Newtonsoft
            // Bạn có thể bỏ qua Formatting.None nếu muốn nén JSON gọn nhất để gửi lên DB/Server.
            return JsonConvert.SerializeObject(dtoList, Formatting.None);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Serialize Error]: {ex.Message}");
            return "[]";
        }
    }

    /// <summary>
    /// Hàm phụ giúp xử lý các ký tự đặc biệt để chuỗi JSON sinh ra không bị lỗi
    /// </summary>
    private static string EscapeString(string str)
    {
        if (string.IsNullOrEmpty(str)) return "";

        return str.Replace("\\", "\\\\") // Xử lý dấu gạch chéo ngược
                  .Replace("\"", "\\\"") // Xử lý dấu nháy kép
                  .Replace("\n", "\\n")  // Xử lý xuống dòng
                  .Replace("\r", "\\r");
    }
}