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
            this.txtSearchQuery = new Sunny.UI.UITextBox();
            this.btnSearch = new Sunny.UI.UISymbolButton();
            this.flpResults = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchQuery.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchQuery.Location = new System.Drawing.Point(13, 14);
            this.txtSearchQuery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchQuery.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSearchQuery.Multiline = true;
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Padding = new System.Windows.Forms.Padding(5);
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
            this.btnSearch.FillColor = System.Drawing.Color.Transparent;
            this.btnSearch.FillColor2 = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSearch.LightColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(320, 12);
            this.btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.Symbol = 61442;
            this.btnSearch.SymbolColor = System.Drawing.Color.Black;
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // flpResults
            // 
            this.flpResults.Location = new System.Drawing.Point(13, 67);
            this.flpResults.Name = "flpResults";
            this.flpResults.Size = new System.Drawing.Size(948, 586);
            this.flpResults.TabIndex = 2;
            // 
            // FormSearch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(973, 665);
            this.Controls.Add(this.flpResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchQuery);
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