using BasicItemSync.Modules.Network.Client;
using GlobalSettings;
using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using System.Linq;

namespace BasicItemSync.Modules.Hooks;

[HarmonyPatch(typeof(CollectableItemPickup))]
internal class CollectableItemPickupHook
{
    public static readonly Dictionary<string, FlagType> CollectableTypes = new()
    {
        { "Ant Trapper Item", FlagType.QuestProgress },
        { "Architect Key", FlagType.Progression },
        { "Beastfly Remains", FlagType.QuestProgress },
        { "Belltown House Key", FlagType.Progression },
        { "Blue Goop Jar", FlagType.QuestProgress },
        { "Broken SilkShot", FlagType.Tool },
        { "Broodmother Remains", FlagType.QuestProgress },
        { "Clover Heart", FlagType.Progression },
        { "Cog Heart Pieces", FlagType.Progression },
        { "Common Spine", FlagType.QuestProgress },
        { "Conchfly Remains", FlagType.QuestProgress },
        { "Coral Chunk", FlagType.QuestProgress },
        { "Coral Heart", FlagType.Progression },
        { "Coral Ingredient", FlagType.QuestProgress },
        { "Courier Supplies", FlagType.DoNotSync },
        { "Courier Supplies Gourmand", FlagType.DoNotSync },
        { "Courier Supplies Mask Maker", FlagType.DoNotSync },
        { "Courier Supplies Slave", FlagType.DoNotSync },
        { "Craw Summons", FlagType.Progression },
        { "Crawbell", FlagType.Collectable },
        { "Crest Socket Unlocker", FlagType.Collectable },
        { "Crow Feather", FlagType.QuestProgress },
        { "Crowman Memento", FlagType.Collectable },
        { "Dock Demo Key", FlagType.DoNotSync },
        { "Dock Key", FlagType.Progression },
        { "Dresses", FlagType.DoNotSync },
        { "Enemy Morsel Seared", FlagType.QuestProgress },
        { "Enemy Morsel Shredded", FlagType.QuestProgress },
        { "Enemy Morsel Speared", FlagType.QuestProgress },
        { "Extractor Machine Pins", FlagType.QuestProgress },
        { "Farsight", FlagType.Collectable },
        { "Fine Pin", FlagType.QuestProgress },
        { "Fixer Idol", FlagType.QuestComplete },
        { "Flower Heart", FlagType.Progression },
        { "Great Shard", FlagType.Collectable },
        { "Grey Memento", FlagType.Collectable },
        { "Growstone", FlagType.QuestComplete },
        { "Hunter Heart", FlagType.Collectable },
        { "Hunter Memento", FlagType.Collectable },
        { "Materium", FlagType.Collectable },
        { "Memento Garmond", FlagType.Collectable },
        { "Memento Seth", FlagType.Collectable },
        { "Memento Surface", FlagType.Collectable },
        { "Mossberry", FlagType.QuestProgress },
        { "Mossberry Stew", FlagType.QuestProgress },
        { "Pale_Oil", FlagType.Collectable },
        { "Pickled Roach Egg", FlagType.QuestProgress },
        { "Pilgrim Rag", FlagType.QuestProgress },
        { "Plasmium", FlagType.QuestProgress },
        { "Plasmium Blood", FlagType.QuestProgress },
        { "Plasmium Gland", FlagType.QuestComplete },
        { "Pristine Core", FlagType.Progression },
        { "Quill", FlagType.Collectable },
        { "R Ancient Egg", FlagType.Collectable },
        { "R Bone Record", FlagType.Collectable },
        { "R Librarian Melody Cylinder", FlagType.Progression },
        { "R Psalm Cylinder", FlagType.Collectable },
        { "R Seal Chit", FlagType.Collectable },
        { "R Weaver Record", FlagType.Collectable },
        { "R Weaver Totem", FlagType.Collectable },
        { "Roach Corpse Item", FlagType.QuestProgress },
        { "Rock Roller Item", FlagType.QuestProgress },
        { "Rosary_Set_Frayed", FlagType.Collectable },
        { "Rosary_Set_Huge_White", FlagType.Collectable },
        { "Rosary_Set_Large", FlagType.Collectable },
        { "Rosary_Set_Medium", FlagType.Collectable },
        { "Rosary_Set_Small", FlagType.Collectable },
        { "Shard Pouch", FlagType.Collectable },
        { "Shell Flower", FlagType.QuestProgress },
        { "Shining Cog", FlagType.QuestProgress },
        { "Silk Grub", FlagType.Collectable },
        { "Silver Bellclapper", FlagType.QuestProgress },
        { "Simple Key", FlagType.Collectable },
        { "Skull King Fragment", FlagType.QuestProgress },
        { "Slab Key", FlagType.Progression },
        { "Snare Soul Bell Hermit", FlagType.Progression },
        { "Snare Soul Churchkeeper", FlagType.Progression },
        { "Snare Soul Swamp Bug", FlagType.Progression },
        { "Song Pilgrim Cloak", FlagType.QuestProgress },
        { "Sprintmaster Memento", FlagType.Collectable },
        { "Tool Metal", FlagType.Collectable },
        { "Vintage Nectar", FlagType.QuestProgress },
        { "Ward Boss Key", FlagType.Progression },
        { "Ward Key", FlagType.Progression },
        { "White Flower", FlagType.Progression },
        { "Wood Witch Item", FlagType.Progression },

        { "Ancient Egg Abyss Middle",               FlagType.Collectable },
        { "Bone Record Bone_East_14",               FlagType.Collectable },
        { "Bone Record Greymoor_flooded_corridor",  FlagType.Collectable },
        { "Bone Record Understore_Map_Room",        FlagType.Collectable },
        { "Bone Record Wisp Top",                   FlagType.Collectable },
        { "Librarian Melody Cylinder",              FlagType.Collectable },
        { "Psalm Cylinder Grindle",                 FlagType.Collectable },
        { "Psalm Cylinder Hang",                    FlagType.Collectable },
        { "Psalm Cylinder Librarian",               FlagType.Collectable },
        { "Psalm Cylinder Library Roof",            FlagType.Collectable },
        { "Psalm Cylinder Ward",                    FlagType.Collectable },
        { "Seal Chit Aspid_01",                     FlagType.Collectable },
        { "Seal Chit City Merchant",                FlagType.Collectable },
        { "Seal Chit Silk Siphon",                  FlagType.Collectable },
        { "Seal Chit Ward Corpse",                  FlagType.Collectable },
        { "Weaver Record Conductor",                FlagType.Collectable },
        { "Weaver Record Sprint_Challenge",         FlagType.Collectable },
        { "Weaver Record Weave_08",                 FlagType.Collectable },
        { "Weaver Totem Bonetown_upper_room",       FlagType.Collectable },
        { "Weaver Totem Slab_Bottom",               FlagType.Collectable },
        { "Weaver Totem Witch",                     FlagType.Collectable },
    };
    [HarmonyPatch(nameof(CollectableItemPickup.DoPickupAction))]
    [HarmonyPostfix]
    public static void DoPickupAction(CollectableItemPickup __instance, ref bool __result)
    {
        if (!__result || !__instance.Item) return;
        if (!OnGetItem(__instance.Item)) return;

        var boolName = __instance.playerDataBool;
        PlayerDataHook.BoolUpdated(boolName, true);

        if (__instance.persistent)
        {
            PersistentBoolItemHook.UpdateValue(__instance.persistent);
        }
    }

    public static bool OnGetItem(SavedItem savedItem)
    {
        var item = savedItem;
        var key = item.name;
        var displayName = savedItem.GetPopupName();

        if (ClientState.WasItemReceived(key)) return false;
        if (!savedItem.CanGetMore()) return false;

        if (!CollectableTypes.TryGetValue(key, out var flagType))
        {
            if (item is CollectableRelic)
            {
                flagType = FlagType.Collectable;
            }
            else if (item is Quest quest)
            {
                SavedItem target = quest.Targets.Count > 0 ? quest.Targets[0].Counter : quest;

                flagType = FlagType.QuestProgress;
                displayName = target.GetPopupName();
            }
            else
            {
                Log.LogWarning($"Unknown item {key}");
                return false;
            }
        }

        if (flagType == FlagType.DoNotSync) return false;

        NetworkSender.SendCollectable(key, displayName, 1, flagType);
        return true;
    }
}

[HarmonyPatch(typeof(SavedItemGet))]
internal class SavedItemGetHook
{
    [HarmonyPatch(nameof(SavedItemGet.OnEnter))]
    [HarmonyPostfix]
    public static void OnEnter(SavedItemGet __instance)
    {
        var item = __instance.Item.Value as SavedItem;
        if (!item) return;

        CollectableItemPickupHook.OnGetItem(item);
    }
}

[HarmonyPatch(typeof(CollectableItemCollect))]
internal class CollectableItemCollectHook
{
    [HarmonyPatch(nameof(CollectableItemCollect.DoAction))]
    [HarmonyPostfix]
    public static void DoAction(CollectableItemCollect __instance, CollectableItem item)
    {
        CollectableItemPickupHook.OnGetItem(item);
    }
}


[HarmonyPatch(typeof(CorpseItems))]
internal class CorpseItemsHook
{
    [HarmonyPatch(nameof(CorpseItems.DoPickupItems))]
    [HarmonyPrefix]
    static void DoPickupItems(CorpseItems __instance)
    {
        List<string> items = [];

        foreach (var item in __instance.pickupItems)
        {
            Log.LogDebug($"[CLI: DPI {item.Item.GetType().Name}] {item.Item}");
            if (item.Item is CollectableItem cI)
            {
                if (cI.IsAtMax()) continue;
            }
            items.Add(item.Item.name);
        }

        foreach (var item in items)
        {
            Log.LogDebug($"[CLI: DPI3] {item}");
            NetworkSender.SendCollectable(item, "", 1, FlagType.QuestProgress);
        }
    }
}

