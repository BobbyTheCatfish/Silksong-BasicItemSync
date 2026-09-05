using System.Linq;

namespace BasicItemSync.Modules
{
    internal class ToolHelper
    {
        public static void GiveTool(string key, bool state, bool isCrest)
        {
            if (isCrest)
            {
                var crest = ToolItemManager.GetCrestByName(key);
                if (crest) crest.Unlock();
                else Log.LogWarning($"[CLI: TH.GiveTool] Unknown crest '{key}'");
            }
            else
            {
                var tool = ToolItemManager.GetToolByName(key);
                if (!tool)
                {
                    Log.LogWarning($"[CLI: TH.GiveTool] Unknown tool '{key}'");
                    return;
                }

                PlayerData.instance.SeenToolGetPrompt = true;
                PlayerData.instance.SeenToolWeaponGetPrompt = true;

                if (state)
                {
                    tool.Unlock();
                    GiveSilkSkill(tool);
                }
                else tool.Lock();
            }
        }

        public static void GiveSilkSkill(string flagName)
        {
            var toolName = flagName switch
            {
                nameof(PlayerData.hasNeedleThrow) => "Silk Spear",
                nameof(PlayerData.hasThreadSphere) => "Thread Sphere",
                nameof(PlayerData.hasParry) => "Parry",
                nameof(PlayerData.hasSilkCharge) => "Silk Charge",
                nameof(PlayerData.hasSilkBomb) => "Silk Bomb",
                nameof(PlayerData.hasSilkBossNeedle) => "Silk Boss Needle",
                _ => ""
            };

            var tool = ToolItemManager.GetToolByName(toolName);
            if (!tool) return;

            GiveSilkSkill(tool);
        }

        public static void GiveSilkSkill(ToolItem tool)
        {
            // Equip silk skill if one isn't equipped yet
            if (tool.Type == ToolItemType.Skill && !ToolItemManager.GetCurrentEquippedTools().Any(t => t && t.Type == ToolItemType.Skill))
            {
                ToolItemManager.AutoEquip(tool);
            }
        }
    }
}
