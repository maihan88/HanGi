namespace HaNgi
{
    partial class FormPlayerSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlayerSettings));
            this.uiMarkLabel1 = new Sunny.UI.UIMarkLabel();
            this.switchQueue = new Sunny.UI.UISwitch();
            this.switchInfo = new Sunny.UI.UISwitch();
            this.uiMarkLabel2 = new Sunny.UI.UIMarkLabel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.radioSunset = new Sunny.UI.UIRadioButton();
            this.radioSky = new Sunny.UI.UIRadioButton();
            this.radioHanGi = new Sunny.UI.UIRadioButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiMarkLabel1
            // 
            resources.ApplyResources(this.uiMarkLabel1, "uiMarkLabel1");
            this.uiMarkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiMarkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiMarkLabel1.MarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.uiMarkLabel1.Name = "uiMarkLabel1";
            // 
            // switchQueue
            // 
            this.switchQueue.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.switchQueue.ActiveText = "";
            resources.ApplyResources(this.switchQueue, "switchQueue");
            this.switchQueue.InActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.switchQueue.InActiveText = "";
            this.switchQueue.Name = "switchQueue";
            this.switchQueue.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.switchQueue_ValueChanged);
            // 
            // switchInfo
            // 
            this.switchInfo.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.switchInfo.ActiveText = "";
            resources.ApplyResources(this.switchInfo, "switchInfo");
            this.switchInfo.InActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.switchInfo.InActiveText = "";
            this.switchInfo.Name = "switchInfo";
            this.switchInfo.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.switchInfo_ValueChanged);
            // 
            // uiMarkLabel2
            // 
            resources.ApplyResources(this.uiMarkLabel2, "uiMarkLabel2");
            this.uiMarkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiMarkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiMarkLabel2.MarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.uiMarkLabel2.Name = "uiMarkLabel2";
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.radioSunset);
            this.uiPanel1.Controls.Add(this.radioSky);
            this.uiPanel1.Controls.Add(this.radioHanGi);
            this.uiPanel1.FillColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.uiPanel1, "uiPanel1");
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioSunset
            // 
            resources.ApplyResources(this.radioSunset, "radioSunset");
            this.radioSunset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioSunset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radioSunset.Name = "radioSunset";
            // 
            // radioSky
            // 
            resources.ApplyResources(this.radioSky, "radioSky");
            this.radioSky.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioSky.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radioSky.Name = "radioSky";
            // 
            // radioHanGi
            // 
            resources.ApplyResources(this.radioHanGi, "radioHanGi");
            this.radioHanGi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioHanGi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radioHanGi.Name = "radioHanGi";
            // 
            // uiLabel1
            // 
            resources.ApplyResources(this.uiLabel1, "uiLabel1");
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel1.Name = "uiLabel1";
            // 
            // uiLabel2
            // 
            resources.ApplyResources(this.uiLabel2, "uiLabel2");
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel2.Name = "uiLabel2";
            // 
            // FormPlayerSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiMarkLabel2);
            this.Controls.Add(this.switchInfo);
            this.Controls.Add(this.switchQueue);
            this.Controls.Add(this.uiMarkLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlayerSettings";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.TitleFont = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Load += new System.EventHandler(this.FormPlayerSettings_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UIMarkLabel uiMarkLabel1;
        private Sunny.UI.UISwitch switchQueue;
        private Sunny.UI.UISwitch switchInfo;
        private Sunny.UI.UIMarkLabel uiMarkLabel2;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIRadioButton radioSky;
        private Sunny.UI.UIRadioButton radioHanGi;
        private Sunny.UI.UIRadioButton radioSunset;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
    }
}