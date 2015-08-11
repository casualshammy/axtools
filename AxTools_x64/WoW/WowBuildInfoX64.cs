﻿using System.Reflection;

namespace AxTools.WoW
{
    [Obfuscation(Exclude = false, Feature = "rename(mode=unicode)")]
    internal static class WowBuildInfoX64
    {

        #region Build info

        internal static readonly byte[] WoWHash =
        {
            0xE0, 0xD1, 0x22, 0x6D, 0x99, 0x89, 0x84, 0x64, 0x01, 0x78, 0x0B, 0xC3, 0x86, 0xC0, 0x9E, 0xE8, 0x51, 0x73, 0x4C, 0x1F, 0x04, 0xDE, 0xB0, 0xC9, 0x3C, 0x08, 0x01, 0xF6, 0x2C, 0x15, 0xBC, 0xE4
        };
        
        #endregion
        
        #region Static infos

        internal static readonly int LastHardwareAction = 0x142E618; // [int] CGGameUI::UpdatePlayerAFK / WRITE
        internal static readonly int TickCount = 0x1421570; // [int]
        internal static readonly int GameState = 0x16887BE; // CGGameUI::LeaveWorld (or Script_IsPlayerInWorld (2) or Script_PlaySound)
        internal static readonly int PlayerName = 0x17D4E40; // ClientServices::GetCharacterName (or Script_UnitName/GetPlayerName)
        internal static readonly int PlayerRealm = 0x17D4FF6; // Гордунни = D0 93 D0 BE D1 80 D0 B4 D1 83 D0 BD D0 BD D0 B8 // Черный Шрам = D0 A7 D0 B5 D1 80 D0 BD D1 8B D0 B9 20 D0 A8 D1 80 D0 B0 D0 BC
        internal static readonly int PlayerZoneID = 0x1688840; // CGGameUI::NewZoneFeedback (16) (or Script_GetRaidRosterInfo (101))
        internal static readonly int PlayerIsLooting = 0x16FC214; // [byte] CGPlayer_C::IsLooting (17) (or Script_SetLootPortrait (32) or Script_GetContainerPurchaseInfo)
        internal static readonly int BlackMarketNumItems = 0x173D328; // [uint]
        internal static readonly int BlackMarketItems = 0x173D330; // [IntPtr]
        internal static readonly int ObjectManager = 0x14CC490; // ClntObjMgrPush (7)
        internal static readonly int PlayerPtr = 0x15ED4E0; // [IntPtr] ClntObjMgrGetActivePlayerObj
        internal static readonly int GlueState = 0x14B5A94; // dword
        internal static readonly int FocusedWidget = 0x142E1C8; // qword
        
        #endregion

        #region Injected methods

        internal static readonly int FrameScript_ExecuteBuffer = 0x3C900;
        internal static readonly int FrameScript_GetLocalizedText = 0x54B5D0;
        internal static readonly int CGGameUI_Target = 0x6E0AD0;
        internal static readonly int CGGameUI_Interact = 0x6E37A0;
        internal static readonly int CGUnit_C_InitializeTrackingState = 0x568270;
        internal const int HookAddr = 0x6F3830;
        internal const int HookLength = 12;
        internal static readonly byte[] HookPattern = {0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74, 0x24, 0x10, 0x57, 0x48};

        #endregion

        #region Object manager

        internal static readonly int ObjectManagerFirstObject = 0x18;
        internal static readonly int ObjectManagerNextObject = 0x68;
        internal static readonly int ObjectType = 0x18;
        internal const int ObjectGUID = 0x50; // declared as const because it used in WoWObjectsInfo

        #endregion

        #region Game object

        internal static readonly int GameObjectOwnerGUIDBase = 0x8;
        internal static readonly int GameObjectOwnerGUIDOffset = 0x30;
        internal static readonly int GameObjectEntryID = 0x24;
        internal static readonly int GameObjectNameBase = 0x498;
        internal static readonly int GameObjectNameOffset = 0xD8;
        internal const int GameObjectIsBobbing = 0x1E0;
        internal const int GameObjectLocation = 0x248;

        #endregion

        #region Player unit

        internal static readonly int UnitDescriptors = 0x8;
        internal static readonly int UnitCastingID = 0x1B98; // Script_UnitCastingInfo //
        internal static readonly int UnitChannelingID = 0x1BB8; // Script_UnitChannelInfo //
        internal static readonly int UnitLocation = 0x1548;
        internal static readonly int UnitRotation = UnitLocation + 0x10;

        internal const int UnitTargetGUID = 0xA0;
        internal const int UnitClass = 0xE5;
        internal const int UnitHealth = 0xF0;
        internal const int UnitPower = 0xF4;
        internal const int UnitHealthMax = 0x10C;
        internal const int UnitPowerMax = 0x110;
        internal const int UnitLevel = 0x158;
        internal const int UnitRace = 0x160;

        #endregion

        #region NPC

        internal static readonly int NpcNameBase = 0x16F0;
        internal static readonly int NpcNameOffset = 0xA0;
        
        internal const int NpcDynamicFlags = 0x28;

        #endregion

    }
}