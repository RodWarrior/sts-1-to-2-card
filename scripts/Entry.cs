using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using BaseLib.Config;
using sts1to2card.Scripts;

namespace sts1to2card.Scripts
{
    [ModInitializer("Init")]
    public class Entry
    {
        public static void Init()
        {
            // 注册配置，让游戏内显示设置界面
            ModConfigRegistry.Register("sts1to2card", new ModConfig());

            // 红卡
            if (ModConfig.RedEvolve)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedEvolve>();

            if (ModConfig.RedPowerThrough)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedPowerThrough>();

            if (ModConfig.RedFlex)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedFlex>();

            if (ModConfig.RedRecklessCharge)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedRecklessCharge>();

            // 飞身踢待修复
            // ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedDropKick>();

            if (ModConfig.RedSpotWeakness)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedSpotWeakness>();

            if (ModConfig.RedReaper)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedReaper>();

            if (ModConfig.RedLimitBreak)
                ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedLimitBreak>();

            // 绿卡
            if (ModConfig.GreenConcentrate)
                ModHelper.AddModelToPool<SilentCardPool, src.green.cards.GreenConcentrate>();

            // 暂时无法实现尸爆效果
            // ModHelper.AddModelToPool<SilentCardPool, src.green.cards.GreenCorpseExplosion>();
        }
    }
}