﻿using System;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public interface IColumnNames
    {
        string Visible{get;}
        string Identifier{get;}
        string Name{get;}
        string Id{get;}
        string Description{get;}
        string TypicalUses{get;}
        string Sample{get;}
        string Source{get;}
        string Path{get;}
        string Link1Url{get;}
        string Link1Name{get;}
        string Link2Url{get;}
        string Link2Name{get;}
        string Link3Url{get;}
        string Link3Name{get;}
    }

    public sealed class ColumnNames : IColumnNames
    {
        private const string SettingPrefix = "ExcelColumnName:";

        private readonly IAppSettingsProvider appSettingsProvider;

        public string Visible { get { return GetAppSetting(SettingPrefix + "Visible"); }}
        public string Identifier { get { return GetAppSetting(SettingPrefix + "Identifier"); } }
        public string Name { get { return GetAppSetting(SettingPrefix + "Name"); } }
        public string Id { get { return GetAppSetting(SettingPrefix + "Id"); } }
        public string Description { get { return GetAppSetting(SettingPrefix + "Description"); } }
        public string TypicalUses { get { return GetAppSetting(SettingPrefix + "TypicalUses"); } }
        public string Sample { get { return GetAppSetting(SettingPrefix + "Sample"); } }
        public string Source { get { return GetAppSetting(SettingPrefix + "Source"); } }
        public string Path { get { return GetAppSetting(SettingPrefix + "Path"); } }
        public string Link1Url { get { return GetAppSetting(SettingPrefix + "Link1Url"); } }
        public string Link1Name { get { return GetAppSetting(SettingPrefix + "Link1Name"); } }
        public string Link2Url { get { return GetAppSetting(SettingPrefix + "Link2Url"); } }
        public string Link2Name { get { return GetAppSetting(SettingPrefix + "Link2Name"); } }
        public string Link3Url { get { return GetAppSetting(SettingPrefix + "Link3Url"); } }
        public string Link3Name { get { return GetAppSetting(SettingPrefix + "Link3Name"); } }

        public ColumnNames(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        private string GetAppSetting(string name)
        {
            return appSettingsProvider.GetSetting(name);
        }
    }
}
