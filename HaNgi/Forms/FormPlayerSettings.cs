using Sunny.UI;
using System;
using System.Windows.Forms;

namespace HaNgi
{
    public partial class FormPlayerSettings : UIForm
    {
        public event Action<bool> QueueVisibilityChanged;
        public event Action<bool> InfoVisibilityChanged;
        public event Action<string> ThemeNameChanged;

        public FormPlayerSettings(bool isQueueVisible, bool isInfoVisible, string currentThemeName)
        {
            InitializeComponent();

            UnsubscribeEvents();

            switchQueue.Active = isQueueVisible;
            switchInfo.Active = isInfoVisible;

            switch (currentThemeName)
            {
                case "Sky":
                    radioSky.Checked = true;
                    break;
                case "Sunset":
                    radioSunset.Checked = true;
                    break;
                default:
                    radioHanGi.Checked = true;
                    break;
            }

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switchQueue.ValueChanged += switchQueue_ValueChanged;
            switchInfo.ValueChanged += switchInfo_ValueChanged;
            radioHanGi.ValueChanged += RadioButton_Theme_Changed;
            radioSky.ValueChanged += RadioButton_Theme_Changed;
            radioSunset.ValueChanged += RadioButton_Theme_Changed;
        }

        private void UnsubscribeEvents()
        {
            switchQueue.ValueChanged -= switchQueue_ValueChanged;
            switchInfo.ValueChanged -= switchInfo_ValueChanged;
            radioHanGi.ValueChanged -= RadioButton_Theme_Changed;
            radioSky.ValueChanged -= RadioButton_Theme_Changed;
            radioSunset.ValueChanged -= RadioButton_Theme_Changed;
        }

        private void RadioButton_Theme_Changed(object sender, bool value)
        {
            // Chỉ xử lý khi RadioButton được CHỌN (khi value trở thành true)
            if (value)
            {
                var selectedRadioButton = sender as UIRadioButton;
                if (selectedRadioButton != null)
                {
                    string themeName = "HanGi";
                    if (selectedRadioButton == radioSky)
                    {
                        themeName = "Sky";
                    }
                    else if (selectedRadioButton == radioSunset)
                    {
                        themeName = "Sunset";
                    }

                    ThemeNameChanged?.Invoke(themeName);
                }
            }
        }

        private void switchQueue_ValueChanged(object sender, bool value)
        {
            QueueVisibilityChanged?.Invoke(value);
        }

        private void switchInfo_ValueChanged(object sender, bool value)
        {
            InfoVisibilityChanged?.Invoke(value);
        }

        private void FormPlayerSettings_Load(object sender, EventArgs e)
        {
            
        }
    }
}
