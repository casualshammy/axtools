﻿using AxTools.WoW.Helpers;
using AxTools.WoW.Internals;
using AxTools.WoW.PluginSystem;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkCrafter
{
    public class InkCrafter : IPlugin3
    {
        #region Info

        public string Name => nameof(InkCrafter);

        public Version Version => new Version(1, 0);

        public string Author => "CasualShammy";

        public string Description => "Crafts all inks";

        private Image trayIcon;

        public Image TrayIcon => trayIcon ?? (trayIcon = new Bitmap($"{this.GetPluginSourceFolder()}\\inv_inscription_ink_starlight.jpg"));

        public string WowIcon => "Interface\\\\Icons\\\\inv_inscription_ink_starlight";

        public bool ConfigAvailable => true;

        public string[] Dependencies => null;

        public bool DontCloseOnWowShutdown => false;

        #endregion Info

        #region Events

        public void OnConfig()
        {
            if (SettingsInstance == null)
            {
                SettingsInstance = this.LoadSettingsJSON<InkCrafterSettings>();
            }
            InkCrafterConfig.Open(SettingsInstance);
            this.SaveSettingsJSON(SettingsInstance);
        }

        public void OnStart(GameInterface game)
        {
            this.game = game;
            SettingsInstance = this.LoadSettingsJSON<InkCrafterSettings>();
            startupTask = Task.Run(() =>
            {
                this.ShowNotify("Please wait while plugin set up inks lists...", false, false);
                game.SendToChat($"/run {tableNames} = {{}};");
                game.SendToChat($"/run {tableAvailable} = {{}};");
                game.SendToChat($"/run {tableIndexes} = {{}};");
                game.SendToChat($"/run {tableRemain} = {{}};");
                foreach (string ink in inks)
                {
                    game.SendToChat($"/run {tableNames}[\"{ink}\"] = true;");
                    game.SendToChat($"/run {tableRemain}[\"{ink}\"] = {(ink == "Чернила разжигателя войны" ? SettingsInstance.WarbindersInkCount : 0)};");
                }
                this.ShowNotify("Completed, starting to craft", false, false);
                (timer = this.CreateTimer(1000, game, OnPulse)).Start();
            });
        }

        public void OnPulse()
        {
            WoWPlayerMe me = game.GetGameObjects();
            if (me != null && me.CastingSpellID == 0 && me.ChannelSpellID == 0)
            {
                game.SendToChat($"/run {tableSkills}={{}}");
                string prepare = $"/run local s=C_TradeSkillUI.GetFilteredRecipeIDs();local t={{}};if(#s>0) then for i=1,#s do C_TradeSkillUI.GetRecipeInfo(s[i], t);{tableSkills}[t.name]={{t.recipeID,t.numAvailable}}; end end";
                game.SendToChat(prepare);
                string craft = string.Format("/run for n,i in pairs({0}) do if({1}[n] and i[2]>{2}[n])then C_TradeSkillUI.CraftRecipe(i[1],i[2]-{2}[n]);return; end end", tableSkills, tableNames, tableRemain);
                game.SendToChat(craft);
                //game.SendToChat(string.Format("/run local o=GetNumTradeSkills();if(o>0)then for i=1,o do local n,_,a=GetTradeSkillInfo(i);if({0}[n])then {1}[n]=a;{2}[n]=i;end end end", tableNames, tableAvailable, tableIndexes));
                //game.SendToChat(string.Format("/run for i in pairs({0})do if({1}[i])then local a={1}[i]-{2}[i];if(a>0)then DoTradeSkill({3}[i],a);return;end end end", tableNames, tableAvailable, tableRemain, tableIndexes));
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        public void OnStop()
        {
            startupTask.Wait();
            startupTask.Dispose();
            timer.Dispose();
        }

        #endregion Events

        private SafeTimer timer;
        internal InkCrafterSettings SettingsInstance;
        private readonly string tableNames = $"_G[\"{Utilities.GetRandomString(6, true)}\"]";
        private readonly string tableAvailable = $"_G[\"{Utilities.GetRandomString(6, true)}\"]";
        private readonly string tableIndexes = $"_G[\"{Utilities.GetRandomString(6, true)}\"]";
        private readonly string tableRemain = $"_G[\"{Utilities.GetRandomString(6, true)}\"]";
        private readonly string tableSkills = Utilities.GetRandomString(6, true);
        private Task startupTask;
        private GameInterface game;

        private readonly string[] inks =
        {
            "Чернила звездного света",
            "Чернила снов",
            "Чернила Преисподней",
            "Мрачно-коричневые чернила",
            //"Чернила снегопада",
            "Чернила моря",
            //"Чернила черного огня",
            "Астральные чернила",
            //"Небесные чернила",
            "Мерцающие чернила",
            "Огненные чернила",
            "Астрономические чернила",
            //"Королевские чернила",
            "Чернила Нефритового Пламени",
            //"Чернила утренней звезды",
            "Чернила царя зверей",
            //"Чернила охотника",
            "Полуночные чернила",
            "Чернила лунного сияния",
            "Бежевые чернила",
            "Великолепная шкура",
            "Толстая борейская кожа",
            "Чернила разжигателя войны",
            "Алые чернила",
            "Ультрамариновые чернила",
        };
    }
}