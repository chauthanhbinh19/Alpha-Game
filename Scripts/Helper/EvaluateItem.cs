using System.Collections.Generic;
public static class EvaluateItem
{
    public static int CalculateMaxMaterialQuantity(int materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 500;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return totalMaterialUsed; // Tổng số material có thể sử dụng
    }
    public static int CalculateMaxMaterialLevel(int materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 500;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return levelsGained; // Tổng số material có thể sử dụng
    }
    // Trả về tổng số lượng item cần để nâng từ rankLevel hiện tại lên thêm "targetLevel" level
    public static int CalculateRequiredQuantityForLevel(int currentLevel, int targetLevel, int levelsPerSkill)
    {
        int total = 0;
        for (int l = 1; l <= targetLevel; l++)
        {
            int materialQuantity = (currentLevel == 0) 
                ? 1 
                : ((currentLevel + l - 1) % levelsPerSkill == 0 
                    ? levelsPerSkill 
                    : (currentLevel + l - 1) % levelsPerSkill);

            total += materialQuantity;
        }
        return total;
    }
}