﻿namespace AxTools.Classes.WoW
{
    internal static class WowBuildInfo
    {
        #region Build info

        internal static readonly int WowBuild = 17128;
        internal static readonly int WowBuildAddress = 0xB12B44;

        #endregion
        
        #region Static infos

        internal static readonly int LastHardwareAction = 0xB30824; // CGGameUI::UpdatePlayerAFK / WRITE
        internal static readonly int IsInGame = 0xCDC84E; // CGGameUI::LeaveWorld (or Script_IsPlayerInWorld (2))
        internal static readonly int IsBattlegroundFinished = 0xD39E28; // Script_GetBattlefieldWinner (2)
        internal static readonly int PlayerName = 0xE3CB40; // ClientServices::GetCharacterName (or Script_UnitName)
        internal static readonly int PlayerRealm = 0xE3CCD6;
        internal static readonly int PlayerZoneText = 0xCDC844; // Script_GetZoneText (4)
        internal static readonly int PlayerZoneID = 0xCDC8EC; // CGGameUI::NewZoneFeedback (16) (or Script_GetRaidRosterInfo (101))
        internal static readonly int PlayerIsLooting = 0xD49D40; // CGPlayer_C::IsLooting (17) (or Script_SetLootPortrait (32))

        #endregion

        #region Injected methods

        internal static readonly int ClntObjMgrGetActivePlayerObjAddress = 0x2D17; // ClntObjMgrGetActivePlayerObj
        internal static readonly int LuaDoStringAddress = 0x54EE6; // FrameScript_ExecuteBuffer
        internal static readonly int LuaGetLocalizedTextAddress = 0x3DD663; // FrameScript_GetLocalizedText
        //internal static readonly int ClickToMove = 0x3E240B; // CGUnit_C::InitializeTrackingState
        internal static readonly int SelectTarget = 0x84CBBE; // CGGameUI::Target
        internal static readonly int Interact = 0x84E102; // CGGameUI::Interact

        #endregion

        #region Black Market

        internal static readonly int BlackMarketNumItems = 0xD88B20;
        internal static readonly int BlackMarketItems = 0xD88B24;

        #endregion

        #region Object manager

        internal static readonly int ObjectManager = 0xC30974; // ClntObjMgrPush (7)
        internal static readonly int ObjectManagerFirstObject = 0xCC;
        internal static readonly int ObjectManagerNextObject = 0x34;

        internal static readonly int ObjectType = 0xC;
        internal static readonly int LocalGUID = 0xE0;
        internal static readonly int ObjectGUID = 0x28;

        #endregion

        #region Game object

        internal static readonly int GameObjectOwnerGUIDBase = 0x4;
        internal static readonly int GameObjectOwnerGUIDOffset = 0x20;
        internal static readonly int GameObjectAnimation = 0xC4;
        internal static readonly int GameObjectEntryID = 0x14;
        internal static readonly int GameObjectNameBase = 0x1B8;
        internal static readonly int GameObjectNameOffset = 0xB0;
        internal static readonly int GameObjectLocationX = 0xF0;
        internal static readonly int GameObjectLocationY = GameObjectLocationX + 0x4;
        internal static readonly int GameObjectLocationZ = GameObjectLocationX + 0x8;

        #endregion

        #region Player unit

        internal static readonly int UnitDescriptors = 0x4;
        internal static readonly int UnitDescriptorsBig = 0xDC; // GetDisplayClassName
        internal static readonly int UnitClass = 81; // GetDisplayClassName
        internal static readonly int UnitLevel = 0xD0; // use CE
        internal static readonly int UnitHealth = 0x78;
        internal static readonly int UnitHealthMax = 0x90;
        internal static readonly int UnitFactionTemplate = 0xD8;
        internal static readonly int UnitTargetGUID = 0x50;
        internal static readonly int UnitCastingID = 0xC60; // Script_UnitCastingInfo //
        internal static readonly int UnitChannelingID = 0xC78; // Script_UnitChannelInfo //
        internal static readonly int UnitLocationX = 0x7F8;
        internal static readonly int UnitLocationY = UnitLocationX + 0x4;
        internal static readonly int UnitLocationZ = UnitLocationX + 0x8;
        internal static readonly int UnitRotation = UnitLocationX + 0x10;
        internal static readonly int UnitNameCachePointer = 0xC03D58 + 0x8;
        internal static readonly int UnitNameMaskOffset = 0x024;
        internal static readonly int UnitNameBaseOffset = 0x18;
        internal static readonly int UnitNameStringOffset = 0x21;

        #endregion

        #region NPC

        internal static readonly int NpcNameBase = 0x974;
        internal static readonly int NpcNameOffset = 0x6C;

        #endregion
    }
}