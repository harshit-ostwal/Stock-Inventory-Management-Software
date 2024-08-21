
namespace SS_SOFTWARE_S.N_JEWELLERS
{
    partial class FRM_PURCHASE_WISE_REPORT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PURCHASE_WISE_REPORT));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.btnResize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgwPurchase = new System.Windows.Forms.DataGridView();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtPurchaseInvoiceNo = new System.Windows.Forms.TextBox();
            this.txtToDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.txtFromDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPurchaseWiseReport = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnResize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // btnResize
            // 
            this.btnResize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResize.Image = ((System.Drawing.Image)(resources.GetObject("btnResize.Image")));
            this.btnResize.Location = new System.Drawing.Point(741, 12);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(24, 24);
            this.btnResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnResize.TabIndex = 29;
            this.btnResize.TabStop = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(711, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 28;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label1.Location = new System.Drawing.Point(42, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Purchase Wise Report";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgwPurchase);
            this.panel1.Controls.Add(this.txtSupplierName);
            this.panel1.Controls.Add(this.txtPurchaseInvoiceNo);
            this.panel1.Controls.Add(this.txtToDate);
            this.panel1.Controls.Add(this.txtFromDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnPurchaseWiseReport);
            this.panel1.Location = new System.Drawing.Point(46, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 173);
            this.panel1.TabIndex = 1;
            // 
            // dgwPurchase
            // 
            this.dgwPurchase.AllowUserToAddRows = false;
            this.dgwPurchase.AllowUserToDeleteRows = false;
            this.dgwPurchase.AllowUserToResizeColumns = false;
            this.dgwPurchase.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gilroy", 10F, System.Drawing.FontStyle.Bold);
            this.dgwPurchase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwPurchase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwPurchase.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgwPurchase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dgwPurchase.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgwPurchase.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Gilroy", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgwPurchase.ColumnHeadersHeight = 30;
            this.dgwPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Gilroy", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwPurchase.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgwPurchase.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.dgwPurchase.Location = new System.Drawing.Point(21, 74);
            this.dgwPurchase.MultiSelect = false;
            this.dgwPurchase.Name = "dgwPurchase";
            this.dgwPurchase.ReadOnly = true;
            this.dgwPurchase.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Gilroy", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwPurchase.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgwPurchase.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Gilroy", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.dgwPurchase.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgwPurchase.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Gilroy", 10F, System.Drawing.FontStyle.Bold);
            this.dgwPurchase.RowTemplate.Height = 30;
            this.dgwPurchase.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwPurchase.Size = new System.Drawing.Size(491, 86);
            this.dgwPurchase.TabIndex = 27;
            this.dgwPurchase.Visible = false;
            this.dgwPurchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwPurchase_KeyDown);
            this.dgwPurchase.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgwPurchase_MouseDoubleClick);
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtSupplierName.Font = new System.Drawing.Font("Gilroy", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierName.ForeColor = System.Drawing.Color.White;
            this.txtSupplierName.Location = new System.Drawing.Point(278, 43);
            this.txtSupplierName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(234, 26);
            this.txtSupplierName.TabIndex = 2;
            this.txtSupplierName.TextChanged += new System.EventHandler(this.txtSupplierName_TextChanged);
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierName_KeyDown);
            // 
            // txtPurchaseInvoiceNo
            // 
            this.txtPurchaseInvoiceNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPurchaseInvoiceNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtPurchaseInvoiceNo.Font = new System.Drawing.Font("Gilroy", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseInvoiceNo.ForeColor = System.Drawing.Color.White;
            this.txtPurchaseInvoiceNo.Location = new System.Drawing.Point(21, 43);
            this.txtPurchaseInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPurchaseInvoiceNo.Name = "txtPurchaseInvoiceNo";
            this.txtPurchaseInvoiceNo.Size = new System.Drawing.Size(234, 26);
            this.txtPurchaseInvoiceNo.TabIndex = 1;
            this.txtPurchaseInvoiceNo.TextChanged += new System.EventHandler(this.txtPurchaseInvoiceNo_TextChanged);
            this.txtPurchaseInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseInvoiceNo_KeyDown);
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
            this.txtToDate.Location = new System.Drawing.Point(278, 113);
            this.txtToDate.MinimumSize = new System.Drawing.Size(140, 26);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(234, 26);
            this.txtToDate.TabIndex = 164;
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
            this.txtFromDate.Location = new System.Drawing.Point(21, 113);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(140, 26);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(234, 26);
            this.txtFromDate.TabIndex = 163;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label4.Location = new System.Drawing.Point(274, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 162;
            this.label4.Text = "To Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label5.Location = new System.Drawing.Point(17, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 161;
            this.label5.Text = "From Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label3.Location = new System.Drawing.Point(274, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 159;
            this.label3.Text = "Supplier Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gilroy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label2.Location = new System.Drawing.Point(18, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 157;
            this.label2.Text = "Purchase Invoice No";
            // 
            // btnPurchaseWiseReport
            // 
            this.btnPurchaseWiseReport.AllowToggling = false;
            this.btnPurchaseWiseReport.AnimationSpeed = 100;
            this.btnPurchaseWiseReport.AutoGenerateColors = false;
            this.btnPurchaseWiseReport.BackColor = System.Drawing.Color.Transparent;
            this.btnPurchaseWiseReport.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnPurchaseWiseReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPurchaseWiseReport.BackgroundImage")));
            this.btnPurchaseWiseReport.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnPurchaseWiseReport.ButtonText = "Purchase Wise Report";
            this.btnPurchaseWiseReport.ButtonTextMarginLeft = 0;
            this.btnPurchaseWiseReport.ColorContrastOnClick = 45;
            this.btnPurchaseWiseReport.ColorContrastOnHover = 45;
            this.btnPurchaseWiseReport.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnPurchaseWiseReport.CustomizableEdges = borderEdges1;
            this.btnPurchaseWiseReport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPurchaseWiseReport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.btnPurchaseWiseReport.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.btnPurchaseWiseReport.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnPurchaseWiseReport.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Idle;
            this.btnPurchaseWiseReport.Font = new System.Drawing.Font("Gilroy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseWiseReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnPurchaseWiseReport.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnPurchaseWiseReport.IconMarginLeft = 0;
            this.btnPurchaseWiseReport.IconPadding = 12;
            this.btnPurchaseWiseReport.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnPurchaseWiseReport.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnPurchaseWiseReport.IdleBorderRadius = 5;
            this.btnPurchaseWiseReport.IdleBorderThickness = 1;
            this.btnPurchaseWiseReport.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnPurchaseWiseReport.IdleIconLeftImage = null;
            this.btnPurchaseWiseReport.IdleIconRightImage = null;
            this.btnPurchaseWiseReport.IndicateFocus = false;
            this.btnPurchaseWiseReport.Location = new System.Drawing.Point(524, 63);
            this.btnPurchaseWiseReport.Name = "btnPurchaseWiseReport";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(201)))), ((int)(((byte)(120)))));
            stateProperties1.BorderRadius = 5;
            stateProperties1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(201)))), ((int)(((byte)(120)))));
            stateProperties1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnPurchaseWiseReport.onHoverState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(86)))), ((int)(((byte)(6)))));
            stateProperties2.BorderRadius = 5;
            stateProperties2.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(86)))), ((int)(((byte)(6)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.btnPurchaseWiseReport.OnPressedState = stateProperties2;
            this.btnPurchaseWiseReport.Size = new System.Drawing.Size(154, 45);
            this.btnPurchaseWiseReport.TabIndex = 156;
            this.btnPurchaseWiseReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPurchaseWiseReport.TextMarginLeft = 0;
            this.btnPurchaseWiseReport.UseDefaultRadiusAndThickness = true;
            this.btnPurchaseWiseReport.Click += new System.EventHandler(this.btnPurchaseWiseReport_Click);
            // 
            // FRM_PURCHASE_WISE_REPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(777, 264);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FRM_PURCHASE_WISE_REPORT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SS SOFTWARE";
            this.Load += new System.EventHandler(this.FRM_PURCHASE_WISE_REPORT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FRM_PURCHASE_WISE_REPORT_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.btnResize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPurchase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnResize;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtPurchaseInvoiceNo;
        private System.Windows.Forms.DataGridView dgwPurchase;
        private Bunifu.UI.WinForms.BunifuDatePicker txtToDate;
        private Bunifu.UI.WinForms.BunifuDatePicker txtFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnPurchaseWiseReport;
    }
}