﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AzureMeteorDetect.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DefaultEndpointsProtocol=https;AccountName=meteorshots;AccountKey=M+rGNU1Ija+Zrs0" +
            "9fVL8FiVj+HVWkx1ji4MvRcSC0Yaa/G+A+MOdN3rAWWCMu8pLBBrFfxM8K4d68FBbsTOmYw==;Endpoi" +
            "ntSuffix=core.windows.net")]
        public string BlobConnectionString {
            get {
                return ((string)(this["BlobConnectionString"]));
            }
            set {
                this["BlobConnectionString"] = value;
            }
        }
    }
}