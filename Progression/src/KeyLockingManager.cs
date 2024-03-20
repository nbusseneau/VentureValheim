using System.Collections.Generic;
using System.Linq;
using BepInEx;

namespace VentureValheim.Progression
{
    public partial class KeyManager
    {
        public const string BOSS_KEY_MEADOW = "defeated_eikthyr";
        public const string BOSS_KEY_BLACKFOREST = "defeated_gdking";
        public const string BOSS_KEY_SWAMP = "defeated_bonemass";
        public const string BOSS_KEY_MOUNTAIN = "defeated_dragon";
        public const string BOSS_KEY_PLAIN = "defeated_goblinking";
        public const string BOSS_KEY_MISTLAND = "defeated_queen";

        public const string HILDIR_KEY_CRYPT = "hildir1";
        public const string HILDIR_KEY_CAVE = "hildir2";
        public const string HILDIR_KEY_TOWER = "hildir3";

        public const int TOTAL_BOSSES = 6;
        public readonly Dictionary<string, int> BossKeyOrderList = new Dictionary<string, int>
        {
            { "", 0 },
            { BOSS_KEY_MEADOW, 1 },
            { BOSS_KEY_BLACKFOREST, 2 },
            { BOSS_KEY_SWAMP, 3 },
            { BOSS_KEY_MOUNTAIN, 4 },
            { BOSS_KEY_PLAIN, 5 },
            { BOSS_KEY_MISTLAND, 6 }
        };

        public readonly Dictionary<string, string> GuardianKeysList = new Dictionary<string, string>
        {
            { "GP_Eikthyr", BOSS_KEY_MEADOW },
            { "GP_TheElder", BOSS_KEY_BLACKFOREST },
            { "GP_Bonemass", BOSS_KEY_SWAMP },
            { "GP_Moder", BOSS_KEY_MOUNTAIN },
            { "GP_Yagluth", BOSS_KEY_PLAIN },
            { "GP_Queen", BOSS_KEY_MISTLAND }
        };

        public readonly Dictionary<string, string> BossItemKeysList = new Dictionary<string, string>
        {
            { "HardAntler", BOSS_KEY_MEADOW },
            { "CryptKey", BOSS_KEY_BLACKFOREST },
            { "Wishbone", BOSS_KEY_SWAMP },
            { "DragonTear", BOSS_KEY_MOUNTAIN },
            { "YagluthDrop", BOSS_KEY_PLAIN },
            { "DvergrKey", BOSS_KEY_PLAIN },
            { "QueenDrop", BOSS_KEY_MISTLAND }
        };

        public readonly Dictionary<string, string> MaterialKeysList = new Dictionary<string, string>
        {
            // Black Forest
            { "FineWood", BOSS_KEY_MEADOW },
            { "Tin", BOSS_KEY_MEADOW },
            { "Copper", BOSS_KEY_MEADOW },
            { "Bronze", BOSS_KEY_MEADOW },
            { "BronzeNails", BOSS_KEY_MEADOW },
            { "TrollHide", BOSS_KEY_MEADOW },
            // Swamp
            { "Iron", BOSS_KEY_BLACKFOREST },
            { "IronNails", BOSS_KEY_BLACKFOREST },
            { "Chain", BOSS_KEY_BLACKFOREST },
            { "ElderBark", BOSS_KEY_BLACKFOREST },
            { "Root", BOSS_KEY_BLACKFOREST },
            { "SharpeningStone", BOSS_KEY_BLACKFOREST },
            // Mountain
            { "Silver", BOSS_KEY_SWAMP },
            { "WolfHairBundle", BOSS_KEY_SWAMP },
            { "WolfPelt", BOSS_KEY_SWAMP },
            { "WolfClaw", BOSS_KEY_SWAMP },
            { "WolfFang", BOSS_KEY_SWAMP },
            { "JuteRed", BOSS_KEY_SWAMP },
            { "Obsidian", BOSS_KEY_SWAMP },
            // Plains
            { "BlackMetal", BOSS_KEY_MOUNTAIN },
            { "Tar", BOSS_KEY_MOUNTAIN },
            { "Needle", BOSS_KEY_MOUNTAIN },
            { "LinenThread", BOSS_KEY_MOUNTAIN },
            { "LoxPelt", BOSS_KEY_MOUNTAIN },
            // Mistlands
            { "BlackMarble", BOSS_KEY_PLAIN },
            { "BlackCore", BOSS_KEY_PLAIN },
            { "Carapace", BOSS_KEY_PLAIN },
            { "Eitr", BOSS_KEY_PLAIN },
            { "ScaleHide", BOSS_KEY_PLAIN },
            { "Wisp", BOSS_KEY_PLAIN },
            { "YggdrasilWood", BOSS_KEY_PLAIN },
            { "JuteBlue", BOSS_KEY_PLAIN }
        };

        public readonly Dictionary<string, string> FoodKeysList = new Dictionary<string, string>
        {
            // Black Forest
            { "Blueberries", BOSS_KEY_MEADOW },
            { "MushroomYellow", BOSS_KEY_MEADOW },
            { "Carrot", BOSS_KEY_MEADOW },
            { "Thistle", BOSS_KEY_MEADOW },
            { "Entrails", BOSS_KEY_MEADOW }, // soft lock due to draugr villages
            // Swamp
            { "Turnip", BOSS_KEY_BLACKFOREST },
            { "Bloodbag", BOSS_KEY_BLACKFOREST },
            { "Ooze", BOSS_KEY_BLACKFOREST },
            { "SerpentMeat", BOSS_KEY_BLACKFOREST },
            { "SerpentMeatCooked", BOSS_KEY_BLACKFOREST },
            // Mountain
            { "FreezeGland", BOSS_KEY_SWAMP },
            { "WolfMeat", BOSS_KEY_SWAMP },
            { "Onion", BOSS_KEY_SWAMP },
            // Plains
            { "Barley", BOSS_KEY_MOUNTAIN },
            { "LoxMeat", BOSS_KEY_MOUNTAIN },
            { "BarleyFlour", BOSS_KEY_MOUNTAIN },
            { "Cloudberry", BOSS_KEY_MOUNTAIN },
            { "ChickenMeat", BOSS_KEY_MOUNTAIN },
            { "ChickenEgg", BOSS_KEY_MOUNTAIN },
            { "BreadDough", BOSS_KEY_MOUNTAIN },
            // Mistlands
            { "GiantBloodSack", BOSS_KEY_PLAIN },
            { "BugMeat", BOSS_KEY_PLAIN },
            { "RoyalJelly", BOSS_KEY_PLAIN },
            { "HareMeat", BOSS_KEY_PLAIN },
            { "Sap", BOSS_KEY_PLAIN },
            { "MushroomJotunPuffs", BOSS_KEY_PLAIN }
        };

        private static int _cachedPublicBossKeys = -1;
        private static int _cachedPrivateBossKeys = -1;

        public int GetPublicBossKeysCount()
        {
            return _cachedPublicBossKeys;
        }

        public int GetPrivateBossKeysCount()
        {
            return _cachedPrivateBossKeys;
        }

        /// <summary>
        /// Returns whether the Player contains the necessary key for taming the specified creature,
        /// or true if the configuration does not exist for the creature.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        private bool HasTamingKey(string creature)
        {
            if (creature.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (TamingKeysList.ContainsKey(creature))
            {
                return HasKey(TamingKeysList[creature]);
            }

            return true;
        }

        /// <summary>
        /// Returns whether the Player contains the necessary key for summoning the specified creature,
        /// or true if the configuration does not exist for the creature.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        private bool HasSummoningKey(string creature)
        {
            if (creature.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (SummoningKeysList.ContainsKey(creature))
            {
                if (HasKey(SummoningKeysList[creature]))
                {
                    if (ProgressionConfiguration.Instance.GetUnlockBossSummonsOverTime())
                    {
                        return SummoningTimeReached(SummoningKeysList[creature], ProgressionAPI.GetGameDay());
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// True if the in-game days passed allows for the creature to be summoned.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool SummoningTimeReached(string key, int gameDay)
        {
            if (BossKeyOrderList.ContainsKey(key))
            {
                int requiredDay = BossKeyOrderList[key] * ProgressionConfiguration.Instance.GetUnlockBossSummonsTime();
                return gameDay >= requiredDay;
            }

            return true;
        }

        /// <summary>
        /// Returns whether the Player contains the necessary key for accepting a boss power.
        /// Supports single boss power name (e.g. <c>"GP_Eikthyr"</c>) and comma-separated string of boss power names
        /// (e.g. <c>"GP_Eikthyr,GP_TheElder"</c>).
        /// </summary>
        /// <param name="guardianPower"></param>
        /// <returns></returns>
        protected bool HasGuardianKey(string guardianPower)
        {
            if (guardianPower.IsNullOrWhiteSpace())
            {
                return false;
            }

            // In most cases, guardianPower will be a single power, e.g. "GP_Eikthyr", in which case we check if the key
            // associated to that power is unlocked. For modding compatibility, we support guardianPower being used for
            // multiple powers at onec via a comma-separated string, e.g. "GP_Eikthyr,GP_TheElder", in which case we
            // check if all of the keys associated with all powers are unlocked.
            var guardianPowers = guardianPower.Split(',');
            var allPowersHaveKnownKeys = guardianPowers.All(GuardianKeysList.ContainsKey);
            if (allPowersHaveKnownKeys)
            {
                var allKeysAreUnlocked = guardianPowers.All(gp => HasKey(GuardianKeysList[gp]));
                return allKeysAreUnlocked;
            }

            return false; // If there are other mods that add powers will need to revisit this
        }

        /// <summary>
        /// Returns whether the Player contains the necessary key for handling the item.
        /// </summary>
        /// <param name="item">Prefab name of the item<</param>
        /// <param name="checkBossItems"></param>
        /// <param name="checkMaterials"></param>
        /// <param name="checkFood"></param>
        /// <returns></returns>
        private bool HasItemKey(string item, bool checkBossItems, bool checkMaterials, bool checkFood)
        {
            if (item.IsNullOrWhiteSpace())
            {
                return false;
            }

            if ((checkBossItems && BossItemKeysList.ContainsKey(item) && !HasKey(BossItemKeysList[item])) ||
               (checkMaterials && MaterialKeysList.ContainsKey(item) && !HasKey(MaterialKeysList[item])) ||
               (checkFood && FoodKeysList.ContainsKey(item) && !HasKey(FoodKeysList[item])))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if an action is blocked based on prefab categories and keys.
        /// Checks the passed item and the item recipe.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quality"></param>
        /// <param name="checkBossItems"></param>
        /// <param name="checkMaterials"></param>
        /// <param name="checkFood"></param>
        /// <returns></returns>
        private bool IsActionBlocked(ItemDrop.ItemData item, int quality, bool checkBossItems, bool checkMaterials, bool checkFood)
        {
            if (item?.m_dropPrefab == null || !Instance.HasItemKey(Utils.GetPrefabName(item.m_dropPrefab), checkBossItems, checkMaterials, checkFood))
            {
                return true;
            }

            var recipe = ObjectDB.instance.GetRecipe(item);
            return IsActionBlocked(recipe, quality, checkBossItems, checkMaterials, checkFood);
        }

        /// <summary>
        /// Checks if an action is blocked based on prefab categories and keys.
        /// Checks the passed recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="quality"></param>
        /// <param name="checkBossItems"></param>
        /// <param name="checkMaterials"></param>
        /// <param name="checkFood"></param>
        /// <returns></returns>
        private bool IsActionBlocked(Recipe recipe, int quality, bool checkBossItems, bool checkMaterials, bool checkFood)
        {
            if (recipe != null)
            {
                for (int lcv1 = 0; lcv1 < recipe.m_resources.Length; lcv1++)
                {
                    if (recipe.m_resources[lcv1].m_resItem == null)
                    {
                        return false;
                    }

                    // Loop through current quality level and all previous.
                    // Blocked should account for both base requirements and
                    // all valid upgrades on an item.
                    for (int lcv2 = 1; lcv2 <= quality; lcv2++)
                    {
                        if (recipe.m_resources[lcv1].GetAmount(lcv2) > 0)
                        {
                            if (!Instance.HasItemKey(Utils.GetPrefabName(recipe.m_resources[lcv1].m_resItem.gameObject), checkBossItems, checkMaterials, checkFood))
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Applies the burning effect and displays the blocked action message.
        /// </summary>
        /// <param name="player"></param>
        private void ApplyBlockedActionEffects(Player player)
        {
            if (player != null)
            {
                if (ProgressionConfiguration.Instance.GetUseBlockedActionEffect())
                {
                    player.GetSEMan()?.AddStatusEffect(Character.s_statusEffectBurning, resetTime: false);
                }

                if (ProgressionConfiguration.Instance.GetUseBlockedActionMessage())
                {
                    player.Message(MessageHud.MessageType.Center, ProgressionConfiguration.Instance.GetBlockedActionMessage());
                }
            }
        }
    }
}
