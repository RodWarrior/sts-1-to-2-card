using BaseLib.Config;

namespace sts1to2card.Scripts
{
    public class ModConfig : SimpleModConfig
    {
        // 红卡开关
        public static bool RedEvolve { get; set; } = true;
        public static bool RedPowerThrough { get; set; } = true;
        public static bool RedFlex { get; set; } = true;
        public static bool RedRecklessCharge { get; set; } = true;
        public static bool RedSpotWeakness { get; set; } = true;
        public static bool RedReaper { get; set; } = true;
        public static bool RedLimitBreak { get; set; } = true;

        // 绿卡开关
        public static bool GreenConcentrate { get; set; } = true;

        // 飞身踢和尸爆暂时保持注释
        // public static bool RedDropKick { get; set; } = false;
        // public static bool GreenCorpseExplosion { get; set; } = false;
    }
}