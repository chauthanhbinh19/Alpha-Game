using System;
using System.Collections.Generic;
public static class SkillHelper
{
    public static string GetBackgroundForSkill(string type)
    {
        switch (type)
        {
            case AppConstants.Skill.ALTERNATIVE:
                return ImageConstants.Background.SKILL_ALTERNATIVE_BACKGROUND_URL;
            case AppConstants.Skill.CELESTIAL:
                return ImageConstants.Background.SKILL_CELESTIAL_BACKGROUND_URL;
            case AppConstants.Skill.DIVINE:
                return ImageConstants.Background.SKILL_DIVINE_BACKGROUND_URL;
            case AppConstants.Skill.FORCES:
                return ImageConstants.Background.SKILL_FORCES_BACKGROUND_URL;
            case AppConstants.Skill.MAIN:
                return ImageConstants.Background.SKILL_MAIN_bACKGROUND_URL;
            case AppConstants.Skill.NORMAL:
                return ImageConstants.Background.SKILL_NORMAL_BACKGROUND_URL;
            case AppConstants.Skill.TEAMWORK:
                return ImageConstants.Background.SKILL_TEAMWORK_bACKGROUND_URL;
            case AppConstants.Skill.TRANSCENDENCE:
                return ImageConstants.Background.SKILL_TRANSCENDENCE_BACKGROUND_URL;
            default:
                return ImageConstants.Background.SKILL_ALTERNATIVE_BACKGROUND_URL;
        }
    }
}