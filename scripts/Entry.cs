using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace sts1to2card.Scripts;

// 必须要加的属性，用于注册Mod。字符串和初始化函数命名一致。
[ModInitializer("Init")]
public class Entry
{

    // 初始化函数
    public static void Init()
    {
        // 传入参数随意，只要不和其他人撞车即可
        ModHelper.AddModelToPool<SilentCardPool, src.green.cards.GreenConcentrate>();
        ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedEvolve>();
        ModHelper.AddModelToPool<IroncladCardPool, src.red.cards.RedReaper>();
    }
}
