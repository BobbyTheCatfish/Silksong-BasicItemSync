using BasicItemSync.Data;
using BasicItemSync.Modules.Network.Client;
using HarmonyLib;
using TeamCherry.Localization;

namespace BasicItemSync.Modules.Hooks;

[HarmonyPatch(typeof(FullQuestBase))]
internal static class FullQuestBaseHook
{
    [HarmonyPatch(nameof(FullQuestBase.BeginQuest))]
    [HarmonyPrefix]
    static void BeginQuest(FullQuestBase __instance)
    {
        if (!QuestManager.IsQuestInList(__instance)) return;
        if (__instance.IsDonateType) return;

        QuestManagerHook.OnQuestStart(__instance);
    }

    [HarmonyPatch(nameof(FullQuestBase.TryEndQuest))]
    [HarmonyPostfix]
    static void TryEndQuest(FullQuestBase __instance, bool __result)
    {
        if (!__result) return;
        QuestManagerHook.OnQuestEnd(__instance);
    }

    [HarmonyPatch(nameof(FullQuestBase.SilentlyComplete))]
    [HarmonyPrefix]
    static void SilentlyComplete(FullQuestBase __instance)
    {
        if (__instance.IsCompleted) return;
        QuestManagerHook.OnQuestEnd(__instance);
    }
}

[HarmonyPatch(typeof(QuestManager))]
internal static class QuestManagerHook
{
    public static void OnQuestStart(BasicQuestBase quest)
    {
        // Can't (shouldn't) sync courier quests
        if (ClientState.WasItemReceived(quest.name)) return;
        if (quest.name.StartsWith("Courier")) return;

        var displayName = Language.GetLocal(quest.displayName);
        NetworkSender.SendQuestState(quest.name, displayName, FlagType.QuestStart);
    }

    public static void OnQuestEnd(BasicQuestBase quest)
    {
        if (ClientState.WasItemReceived(quest.name)) return;

        if (quest is FullQuestBase full)
        {
            var item = full.RewardItem;
            if (item)
            {
                if (item.name == ItemNames.MaskShard) { }
                else if (item.name == ItemNames.SpoolShard) { }
                else if (item.name == ItemNames.ToolPouch) NetworkSender.SendUpgrade("", FlagType.Pouch);
                else if (item.name == ItemNames.CraftingKit) NetworkSender.SendUpgrade("", FlagType.CraftingKit);
                else if (item.name == ItemNames.NeedleUpgrade) NetworkSender.SendUpgrade("", FlagType.Needle);
                else if (item.name == ItemNames.MoneyReward) { }
                else if (item.name == ItemNames.ShardsReward) { }
                else NetworkSender.SendCollectable(item.name, item.GetPopupName(), full.RewardCount, FlagType.Collectable);
            }
        }

        var displayName = Language.GetLocal(quest.displayName);
        NetworkSender.SendQuestState(quest.name, displayName, FlagType.QuestComplete);
    }
}