
namespace SS_SOFTWARE_S.N_JEWELLERS
{
    partial class FRM_STOCK_IN_OUT_REPORT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_STOCK_IN_OUT_REPORT));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStockInOutReport = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.txtToDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.txtFromDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label1.Location = new System.Drawing.Point(42, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Stock IN/OUT Report";
            // 
            // btnResize
            // 
            this.btnResize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResize.Image = ((System.Drawing.Image)(resources.GetObject("btnResize.Image")));
            this.btnResize.Location = new System.Drawing.Point(722, 13);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(24, 24);
            this.btnResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnResize.TabIndex = 25;
            this.btnResize.TabStop = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(692, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 24;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStockInOutReport);
            this.panel1.Controls.Add(this.txtToDate);
            this.panel1.Controls.Add(this.txtFromDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(46, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 85);
            this.panel1.TabIndex = 1;
            // 
            // btnStockInOutReport
            // 
            this.btnStockInOutReport.AllowToggling = false;
            this.btnStockInOutReport.AnimationSpeed = 100;
            this.btnStockInOutReport.AutoGenerateColors = false;
            this.btnStockInOutReport.BackColor = System.Drawing.Color.Transparent;
            this.btnStockInOutReport.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnStockInOutReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStockInOutReport.BackgroundImage")));
            this.btnStockInOutReport.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnStockInOutReport.ButtonText = "IN/OUT Report";
            this.btnStockInOutReport.ButtonTextMarginLeft = 0;
            this.btnStockInOutReport.ColorContrastOnClick = 45;
            this.btnStockInOutReport.ColorContrastOnHover = 45;
            this.btnStockInOutReport.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnStockInOutReport.CustomizableEdges = borderEdges1;
            this.btnStockInOutReport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStockInOutReport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.btnStockInOutReport.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.btnStockInOutReport.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnStockInOutReport.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Idle;
            this.btnStockInOutReport.Font = new System.Drawing.Font("Gilroy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockInOutReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnStockInOutReport.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockInOutReport.IconMarginLeft = 0;
            this.btnStockInOutReport.IconPadding = 12;
            this.btnStockInOutReport.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockInOutReport.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnStockInOutReport.IdleBorderRadius = 5;
            this.btnStockInOutReport.IdleBorderThickness = 1;
            this.btnStockInOutReport.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnStockInOutReport.IdleIconLeftImage = null;
            this.btnStockInOutReport.IdleIconRightImage = null;
            this.btnStockInOutReport.IndicateFocus = false;
            this.btnStockInOutReport.Location = new System.Drawing.Point(529, 20);
            this.btnStockInOutReport.Name = "btnStockInOutReport";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(201)))), ((int)(((byte)(120)))));
            stateProperties1.BorderRadius = 5;
            stateProperties1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(201)))), ((int)(((byte)(120)))));
            stateProperties1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnStockInOutReport.onHoverState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(86)))), ((int)(((byte)(6)))));
            stateProperties2.BorderRadius = 5;
            stateProperties2.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(86)))), ((int)(((byte)(6)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.btnStockInOutReport.OnPressedState = stateProperties2;
            this.btnStockInOutReport.Size = new System.Drawing.Size(114, 45);
            this.btnStockInOutReport.TabIndex = 3;
            this.btnStockInOutReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStockInOutReport.TextMarginLeft = 0;
            this.btnStockInOutReport.UseDefaultRadiusAndThickness = true;
            this.btnStockInOutReport.Click += new System.EventHandler(this.btnStockInOutReport_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtToDate.BorderRadius = 1;
            this.txtToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtToDate.CalendarTitleBackColor = System.Drawing.Color.LightBlue;
            this.txtToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtToDate.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.txtToDate.Color = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtToDate.CustomFormat = "dd-MM-yyyy";
            this.txtToDate.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thick;
            this.txtToDate.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.txtToDate.DisabledColor = System.Drawing.Color.Gray;
            this.txtToDate.DisplayWeekNumbers = false;
            this.txtToDate.DPHeight = 0;
            this.txtToDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.txtToDate.FillDatePicker = false;
            this.txtToDate.Font = new System.Drawing.Font("Gilroy", 10F, System.Drawing.FontStyle.Bold);
            this.txtToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtToDate.Icon = ((System.Drawing.Image)(resources.GetObject("txtToDate.Icon")));
            this.txtToDate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtToDate.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.txtToDate.Location = new System.Drawing.Point(273, 36);
            this.txtToDate.MinimumSize = new System.Drawing.Size(140, 26);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(234, 26);
            this.txtToDate.TabIndex = 2;
            this.txtToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_Only);
            // 
            // txtFromDate
            // 
            this.txtFromDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtFromDate.BorderRadius = 1;
            this.txtFromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtFromDate.CalendarTitleBackColor = System.Drawing.Color.LightBlue;
            this.txtFromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFromDate.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.txtFromDate.Color = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFromDate.CustomFormat = "dd-MM-yyyy";
            this.txtFromDate.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thick;
            this.txtFromDate.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.txtFromDate.DisabledColor = System.Drawing.Color.Gray;
            this.txtFromDate.DisplayWeekNumbers = false;
            this.txtFromDate.DPHeight = 0;
            this.txtFromDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.txtFromDate.FillDatePicker = false;
            this.txtFromDate.Font = new System.Drawing.Font("Gilroy", 10F, System.Drawing.FontStyle.Bold);
            this.txtFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFromDate.Icon = ((System.Drawing.Image)(resources.GetObject("txtFromDate.Icon")));
            this.txtFromDate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFromDate.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.txtFromDate.Location = new System.Drawing.Point(16, 36);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(140, 26);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(234, 26);
            this.txtFromDate.TabIndex = 1;
            this.txtFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_Only);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label3.Location = new System.Drawing.Point(269, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 56;
            this.label3.Text = "To Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 54;
            this.label2.Text = "From Date :";
            // 
            // FRM_STOCK_IN_OUT_REPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(760, 190);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FRM_STOCK_IN_OUT_REPORT";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SS SOFTWARE";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FRM_STOCK_IN_OUT_REPORT_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnResize;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuDatePicker txtToDate;
        private Bunifu.UI.WinForms.BunifuDatePicker txtFromDate;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnStockInOutReport;
    }
}