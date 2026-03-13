using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Entities.Cards;
using BaseLib.Config;
using sts1to2card.Scripts;
using System;
using System.Linq;
using System.Reflection;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.Scripts
{
    [ModInitializer("Init")]
    public class Entry
    {
        public static void Init()
        {
            // 注册配置
            ModConfigRegistry.Register("sts1to2card", new ModConfig());

            // 自动注册卡牌
            RegisterCards();
        }

        private static void RegisterCards()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var cardTypes = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    typeof(CardModel).IsAssignableFrom(t)
                );

            foreach (var type in cardTypes)
            {
                var name = type.Name;

                // 红卡
                if (name.StartsWith("Red"))
                {
                    ModHelper.AddModelToPool(typeof(IroncladCardPool), type);
                }

                // 绿卡
                else if (name.StartsWith("Green"))
                {
                    ModHelper.AddModelToPool(typeof(SilentCardPool), type);
                }
            }
        }
    }
}