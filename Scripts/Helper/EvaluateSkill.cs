using System;
using System.Collections.Generic;
public static class EvaluateSkill
{
    public static string GetBackgroundForSkill(string type)
    {
        switch (type)
        {
            case AppConstants.Skill.ALTERNATIVE:
                return ImageConstants.Background.SkillAlternativeBackground;
            case AppConstants.Skill.CELESTIAL:
                return ImageConstants.Background.SkillCelestialBackground;
            case AppConstants.Skill.DIVINE:
                return ImageConstants.Background.SkillDivineBackground;
            case AppConstants.Skill.FORCES:
                return ImageConstants.Background.SkillForcesBackground;
            case AppConstants.Skill.MAIN:
                return ImageConstants.Background.SkillMainBackground;
            case AppConstants.Skill.NORMAL:
                return ImageConstants.Background.SkillNormalBackground;
            case AppConstants.Skill.TEAMWORK:
                return ImageConstants.Background.SkillTeamworkBackground;
            case AppConstants.Skill.TRANSCENDENCE:
                return ImageConstants.Background.SkillTranscendenceBackground;
            default:
                return ImageConstants.Background.SkillAlternativeBackground;
        }
    }
}