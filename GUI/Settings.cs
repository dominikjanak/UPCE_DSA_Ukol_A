using System.Windows.Forms;

namespace GUI.Properties {
    
    internal sealed partial class Settings {
        
        public Settings() 
        {
            this.SettingChanging += this.SettingChangingEventHandler;
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) 
        {
            Settings.Default.Save();
        }
    }
}
