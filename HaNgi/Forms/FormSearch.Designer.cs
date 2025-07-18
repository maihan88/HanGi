namespace HaNgi
{
    partial class FormSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.txtSearchQuery = new Sunny.UI.UITextBox();
            this.btnSearch = new Sunny.UI.UISymbolButton();
            this.flpResults = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchQuery.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtSearchQuery.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtSearchQuery.Location = new System.Drawing.Point(13, 14);
            this.txtSearchQuery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchQuery.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSearchQuery.Multiline = true;
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Padding = new System.Windows.Forms.Padding(5);
            this.txtSearchQuery.Radius = 20;
            this.txtSearchQuery.ShowText = false;
            this.txtSearchQuery.Size = new System.Drawing.Size(300, 30);
            this.txtSearchQuery.TabIndex = 0;
            this.txtSearchQuery.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSearchQuery.Watermark = "";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.btnSearch.FillColor2 = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSearch.LightColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(320, 12);
            this.btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Radius = 30;
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.Symbol = 61442;
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // flpResults
            // 
            this.flpResults.BackColor = System.Drawing.Color.Transparent;
            this.flpResults.Location = new System.Drawing.Point(13, 67);
            this.flpResults.Name = "flpResults";
            this.flpResults.Size = new System.Drawing.Size(948, 586);
            this.flpResults.TabIndex = 2;
            // 
            // FormSearch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(973, 665);
            this.Controls.Add(this.flpResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSearch";
            this.Text = "FormSearch";
            this.Initialize += new System.EventHandler(this.FormSearch_Initialize);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox txtSearchQuery;
        private Sunny.UI.UISymbolButton btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flpResults;
    }
}