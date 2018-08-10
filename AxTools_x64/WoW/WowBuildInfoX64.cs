﻿using System.Reflection;

namespace AxTools.WoW
{
    [Obfuscation(Exclude = false, Feature = "rename(mode=unicode)")]
    internal static class WowBuildInfoX64
    {
        #region Comments

        //internal const int PlayerZoneID = 0x1735880; // use CheatEngine
        //internal const int ChatBuffer = 0x1736250;
        //internal const int PlayerPtr = 0x1694B10; // Script_GetCurrentTitle
        //internal const int NameCacheBase = 0x0;
        //internal const int GlueState = 0x155862C; // Search for string "Copy request %d finished"
        //internal const int LastHardwareAction = 0x14BD468;
        //internal const int FocusedWidget = 0x14B1010; // Script_GetCurrentKeyBoardFocus; Frame__HasFocus
        //internal const int BlackMarketNumItems = 0x17E6FC0; // Script_C_BlackMarket.GetItemInfoByIndex
        //internal const int Possible_NotLoadingScreen = 0x11FC734; // go to upper functions, look for strings there
        //internal const int KnownSpellsCount = 0x17937A0; // Script_CastSpellByID
        //internal const int BlackMarketItems = 0x17E6FC8; // Script_C_BlackMarket.GetItemInfoByIndex
        //internal const int KnownSpells = 0x17937A8; // Script_CastSpellByID
        //internal const int MouseoverGUID = 0x17359F0; // Search for string "Current Object Track"
        //internal const int TickCount = 0x14B038C; // Uper function: Script_GetSessionTime
        //internal const int PlayerIsLooting = 0x17AA2CD;
        //internal const int ChatIsOpened = 0x14CA470; // CheatEngine :(
        //internal const int ObjectManager = 0x156EC50;
        //internal const int PlayerName = 0x181F0B0;
        //internal const int UIFrameBase = 0x14BD460; // Script_EnumerateFrames

        /*
		internal static readonly int LastHardwareAction = 0x1446208; // [int] CGGameUI::UpdatePlayerAFK / WRITE
		internal static readonly int TickCount = 0x1439160; // [int]
		internal static readonly int GameState = 0x16A179E; // CGGameUI::LeaveWorld (or Script_IsPlayerInWorld (2) or Script_PlaySound) (it's equal to 2 if in game, 1 if loading from login screen)
		//internal static readonly int PlayerName = 0x17EF7E0; // ClientServices::GetCharacterName (or Script_UnitName/GetPlayerName)
		//internal static readonly int PlayerRealm = 0x17EF996; // Гордунни = D0 93 D0 BE D1 80 D0 B4 D1 83 D0 BD D0 BD D0 B8 // Черный Шрам = D0 A7 D0 B5 D1 80 D0 BD D1 8B D0 B9 20 D0 A8 D1 80 D0 B0 D0 BC
		internal static readonly int PlayerZoneID = 0x16A1820; // CGGameUI::NewZoneFeedback (16) (or Script_GetRaidRosterInfo (101))
		internal static readonly int PlayerIsLooting = 0x1715264; // [byte] CGPlayer_C::IsLooting (17) (or Script_SetLootPortrait (32) or Script_GetContainerPurchaseInfo)
		internal static readonly int ObjectManager = 0x14E4DC0; // ClntObjMgrPush (7)
		internal static readonly int PlayerPtr = 0x1606320; // [IntPtr] ClntObjMgrGetActivePlayerObj
		 *
		 * corpseOwnerGUID = 0x728;
		*/

        // KnownSpells: lookfor "if ( (signed int)sub_750490(v2, v3) >= 0 )" in Script_IsSpellKnown
        // PlayerSpeed: normal speed: 100% = 7f, 110% = 7.7f

        // HOW TO FIND "PlayerGUID"
        // Search for "REALM_SEPARATORS" string after "FriendList.cpp" string. Call before "FriendList.cpp" is GetLocalPlayerGUID()

        // HOW TO FIND "ObjectManager"
        // Search for "object manager" string

        // HOW TO FIND Auras info
        // Look into functions in "Script_UnitAura"

        // HOW TO FIND PlayerIsLooting
        // Look into functions in "Script_GetNumLootItems"

        // HOW TO FIND PlayerZoneID
        // CheatEngine :(

        // GameState: 3 - loading from character screen, 2 - almost loaded, 4 - in game?

        // Bags info:

        #endregion Comments

        #region Build info

        internal static readonly byte[] WoWHash =
        {
            0xEF, 0x6A, 0xCA, 0x11, 0x06, 0x71, 0x6A, 0x4B, 0x76, 0xFA, 0xC6, 0xF8, 0xD6, 0x2F, 0x0F, 0x6E, 0x4C, 0x83, 0x4A, 0x95, 0xA7, 0x14, 0x11, 0x0D, 0xD4, 0x8F, 0x87, 0xC4, 0x67, 0x63, 0xB7, 0x0A
        };

        internal const int WoWRevision = 27219;

        #endregion Build info

        #region Static infos

        internal const int MouseoverGUID = 0x2B505D8;
        internal const int UIFrameBase = 0x270B010;
        internal const int BlackMarketItems = 0x2BDC0E8;
        internal const int BlackMarketNumItems = 0x2BDC0E0;
        internal const int GlueState = 0x2711759;
        internal const int ChatIsOpened = 0x273A164;
        internal const int PlayerZoneID = 0x2B4F8AC;
        internal const int NotLoadingScreen = 0x23F839C;
        internal const int LastHardwareAction = 0x270B018;
        internal const int PlayerIsLooting = 0x2BB4248;
        internal const int FocusedWidget = 0x270B0F8;
        internal const int TickCount = 0x270A60C;
        internal const int GameState = 0x2B505B1;
        internal const int KnownSpells = 0x2B84988;
        internal const int ObjectManager = 0x27F8908;
        internal const int IsChatAFK = 0x2B51490;
        internal const int PlayerGUID = 0x2C6E540;
        internal const int KnownSpellsCount = 0x2B84980;
        internal const int ChatBuffer = 0x2B514A0;
        internal const int NameCacheBase = 0x166A918;

        #endregion Static infos

        #region UIFrame

        internal const int UIFirstFrame = 0xCD0;
        internal const int UINextFrame = 0xCC0;
        internal const int UIFrameVisible = 0xC8;
        internal const int UIFrameVisible1 = 0x13;
        internal const int UIFrameVisible2 = 1;
        internal const int UIFrameName = 0x20;
        internal const int UIEditBoxText = 0x238;

        #endregion UIFrame

        #region Object manager

        internal const int ObjectManagerFirstObject = 0x18;
        internal const int ObjectManagerNextObject = 0x70;
        internal const int ObjectType = 0x20;
        internal const int ObjectGUID = 0x58;

        #endregion Object manager

        #region Game object

        internal static readonly int GameObjectOwnerGUIDBase = 0x10;
        internal static readonly int GameObjectOwnerGUIDOffset = 0x30;
        internal static readonly int GameObjectEntryID = 0x10;
        internal static readonly int GameObjectNameBase = 0x478;
        internal static readonly int GameObjectNameOffset = 0xE0;
        internal const int GameObjectIsBobbing = 0x14C;
        internal const int GameObjectLocation = 0x1b0;

        #endregion Game object

        #region Player unit

        internal static readonly int UnitDescriptors = 0x10;
        internal static readonly int UnitCastingID = 0x1920;
        internal static readonly int UnitChannelingID = 0x1950;
        internal static readonly int UnitLocation = 0x1588;
        internal static readonly int UnitRotation = UnitLocation + 0x10;
        internal static readonly int UnitPitch = UnitRotation + 0x4;

        internal const int UnitTargetGUID = 0x9c;
        internal const int UnitClass = 0xD1;
        internal const int UnitHealth = 0xDC;
        internal const int UnitPower = 0xE4;
        internal const int UnitHealthMax = 0xfc;
        internal const int UnitPowerMax = 0x104;
        internal const int UnitLevel = 0x14C;
        internal const int UnitRace = 0x170;
        internal const int UnitFlags = 0x18C;
        internal const int UnitMountDisplayID = 0x1C0;

        internal const int AuraCount1 = 0x24D8;
        internal const int AuraCount2 = 0x1A58;
        internal const int AuraTable1 = 0x1A58;
        internal const int AuraTable2 = 0x1A60;

        internal const int NameCacheNext = 0x00;
        internal const int NameCacheGuid = 0x20;
        internal const int NameCacheName = 0x31;

        internal const int ActivePlayer_Inventory = 0x1D74;
        internal const int ActivePlayer_Containers = ActivePlayer_Inventory + 19 * 16; // 19 WoWGUIDs of items in active player's inventory
        internal const int ActivePlayer_Backpack = ActivePlayer_Containers + 4 * 16; // 4 active player's containers

        internal const int PlayerSpeedBase = 0x198;
        internal const int PlayerSpeedOffset = 0xA4;

        internal const int PlayerIsLootingOffset0 = 0x1588;
        internal const int PlayerIsLootingOffset1 = 0xA08;

        #endregion Player unit

        #region NPC

        internal static readonly int NpcNameBase = 0x1740;
        internal static readonly int NpcNameOffset = 0xE0;

        internal const int NpcDynamicFlags = 10;

        #endregion NPC

        #region WoWItem

        internal const int WoWItemContainedIn = 0x2c;
        internal const int WoWItemStackCount = 0x5c;
        internal const int WoWItem_WeaponEnchant = 0x88;

        internal const int WoWContainerItems = 0x140;

        #endregion WoWItem

        #region GameChat

        internal const int ChatNextMessage = 0xCB8; // OK
        internal const int ChatFullMessageOffset = 0xE6; // OK
        internal const int ChatSenderGuid = 0x00;
        internal const int ChatSenderName = 0x034;
        internal const int ChatType = 0xCA0;
        internal const int ChatChannelNum = 0xCA4;
        internal const int ChatTimeStamp = 0xCB0;

        #endregion GameChat
    }
}