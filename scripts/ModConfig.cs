using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseLib.Config;

namespace sts1to2card.Scripts
{
    public class ModConfig : SimpleModConfig
    {
        private static readonly string RedCardsPath = "src/red/cards";
        private static readonly string GreenCardsPath = "src/green/cards";

        // 动态开关字典
        private static Dictionary<string, bool> cardSwitches = new Dictionary<string, bool>();

        static ModConfig()
        {
            // 自动读取红卡
            LoadCardSwitches(RedCardsPath, "Red");
            // 自动读取绿卡
            LoadCardSwitches(GreenCardsPath, "Green");
        }

        private static void LoadCardSwitches(string path, string colorPrefix)
        {
            if (!Directory.Exists(path)) return;

            var files = Directory.GetFiles(path, "*.cs");
            foreach (var file in files)
            {
                string cardName = Path.GetFileNameWithoutExtension(file);
                string key = $"{colorPrefix}{cardName}";
                if (!cardSwitches.ContainsKey(key))
                {
                    cardSwitches[key] = true; // 默认启用
                }
            }
        }

        // 获取开关
        public static bool GetCardEnabled(string colorPrefix, string cardName)
        {
            string key = $"{colorPrefix}{cardName}";
            return cardSwitches.TryGetValue(key, out bool enabled) && enabled;
        }

        // 设置开关
        public static void SetCardEnabled(string colorPrefix, string cardName, bool enabled)
        {
            string key = $"{colorPrefix}{cardName}";
            if (cardSwitches.ContainsKey(key))
            {
                cardSwitches[key] = enabled;
            }
        }

        // 获取所有开关（用于UI显示）
        public static Dictionary<string,bool> GetAllCardSwitches()
        {
            return cardSwitches.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}