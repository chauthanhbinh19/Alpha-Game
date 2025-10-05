using System;
using System.Collections.Generic;
public static class EvaluateSkill
{
    public static string GetBackgroundForSkill(string type)
    {
        switch (type)
        {
            case AppConstants.Skill.Alternative:
                return ImageConstants.Background.SkillAlternativeBackground;
            case AppConstants.Skill.Celestial:
                return ImageConstants.Background.SkillCelestialBackground;
            case AppConstants.Skill.Divine:
                return ImageConstants.Background.SkillDivineBackground;
            case AppConstants.Skill.Forces:
                return ImageConstants.Background.SkillForcesBackground;
            case AppConstants.Skill.Main:
                return ImageConstants.Background.SkillMainBackground;
            case AppConstants.Skill.Normal:
                return ImageConstants.Background.SkillNormalBackground;
            case AppConstants.Skill.Teamwork:
                return ImageConstants.Background.SkillTeamworkBackground;
            case AppConstants.Skill.Transcendence:
                return ImageConstants.Background.SkillTranscendenceBackground;
            default:
                return ImageConstants.Background.SkillAlternativeBackground;
        }
    }
}