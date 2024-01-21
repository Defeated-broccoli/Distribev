namespace Distribev
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "", "", "", "" }, -1);
            button1 = new Button();
            listViewProducts = new ListView();
            ItemName = new ColumnHeader();
            ItemPrice = new ColumnHeader();
            ItemAddress = new ColumnHeader();
            cbWebsites = new ComboBox();
            lPagesVisitedDesc = new Label();
            lProductsCountDesc = new Label();
            lPagesVisited = new Label();
            lProductsCount = new Label();
            openFileDialog1 = new OpenFileDialog();
            bSavePath = new Button();
            tbSavePath = new TextBox();
            tSaveFile = new System.Windows.Forms.Timer(components);
            tScannedCounter = new System.Windows.Forms.Timer(components);
            lScanningTimeDesc = new Label();
            lScanningTime = new Label();
            tScanningTime = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(616, 282);
            button1.Name = "button1";
            button1.Size = new Size(151, 29);
            button1.TabIndex = 0;
            button1.Text = "Load products";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listViewProducts
            // 
            listViewProducts.Columns.AddRange(new ColumnHeader[] { ItemName, ItemPrice, ItemAddress });
            listViewProducts.Items.AddRange(new ListViewItem[] { listViewItem2 });
            listViewProducts.Location = new Point(12, 12);
            listViewProducts.Name = "listViewProducts";
            listViewProducts.Size = new Size(585, 418);
            listViewProducts.TabIndex = 1;
            listViewProducts.UseCompatibleStateImageBehavior = false;
            listViewProducts.View = View.Details;
            // 
            // ItemName
            // 
            ItemName.Text = "Nazwa";
            ItemName.Width = 200;
            // 
            // ItemPrice
            // 
            ItemPrice.Text = "Cena";
            ItemPrice.Width = 150;
            // 
            // ItemAddress
            // 
            ItemAddress.Text = "Link";
            ItemAddress.Width = 200;
            // 
            // cbWebsites
            // 
            cbWebsites.FormattingEnabled = true;
            cbWebsites.Location = new Point(616, 12);
            cbWebsites.Name = "cbWebsites";
            cbWebsites.Size = new Size(151, 28);
            cbWebsites.TabIndex = 2;
            cbWebsites.SelectedIndexChanged += cbWebsites_SelectedIndexChanged;
            // 
            // lPagesVisitedDesc
            // 
            lPagesVisitedDesc.AutoSize = true;
            lPagesVisitedDesc.Location = new Point(616, 375);
            lPagesVisitedDesc.Name = "lPagesVisitedDesc";
            lPagesVisitedDesc.Size = new Size(110, 20);
            lPagesVisitedDesc.TabIndex = 3;
            lPagesVisitedDesc.Text = "Pages Scanned:";
            // 
            // lProductsCountDesc
            // 
            lProductsCountDesc.AutoSize = true;
            lProductsCountDesc.Location = new Point(616, 410);
            lProductsCountDesc.Name = "lProductsCountDesc";
            lProductsCountDesc.Size = new Size(112, 20);
            lProductsCountDesc.TabIndex = 4;
            lProductsCountDesc.Text = "Products found:";
            // 
            // lPagesVisited
            // 
            lPagesVisited.AutoSize = true;
            lPagesVisited.Location = new Point(732, 375);
            lPagesVisited.Name = "lPagesVisited";
            lPagesVisited.Size = new Size(17, 20);
            lPagesVisited.TabIndex = 5;
            lPagesVisited.Text = "0";
            // 
            // lProductsCount
            // 
            lProductsCount.AutoSize = true;
            lProductsCount.Location = new Point(732, 410);
            lProductsCount.Name = "lProductsCount";
            lProductsCount.Size = new Size(17, 20);
            lProductsCount.TabIndex = 6;
            lProductsCount.Text = "0";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // bSavePath
            // 
            bSavePath.Location = new Point(616, 107);
            bSavePath.Name = "bSavePath";
            bSavePath.Size = new Size(151, 29);
            bSavePath.TabIndex = 7;
            bSavePath.Text = "Set save path";
            bSavePath.UseVisualStyleBackColor = true;
            bSavePath.Click += bSavePath_Click;
            // 
            // tbSavePath
            // 
            tbSavePath.Enabled = false;
            tbSavePath.Location = new Point(616, 74);
            tbSavePath.Name = "tbSavePath";
            tbSavePath.Size = new Size(151, 27);
            tbSavePath.TabIndex = 8;
            tbSavePath.TextChanged += tbSavePath_TextChanged;
            // 
            // tSaveFile
            // 
            tSaveFile.Interval = 30000;
            tSaveFile.Tick += tSaveFile_Tick;
            // 
            // tScannedCounter
            // 
            tScannedCounter.Interval = 5000;
            tScannedCounter.Tick += tScannedCounter_Tick;
            // 
            // lScanningTimeDesc
            // 
            lScanningTimeDesc.AutoSize = true;
            lScanningTimeDesc.Location = new Point(616, 340);
            lScanningTimeDesc.Name = "lScanningTimeDesc";
            lScanningTimeDesc.Size = new Size(109, 20);
            lScanningTimeDesc.TabIndex = 9;
            lScanningTimeDesc.Text = "Scanning Time:";
            // 
            // lScanningTime
            // 
            lScanningTime.AutoSize = true;
            lScanningTime.Location = new Point(732, 340);
            lScanningTime.Name = "lScanningTime";
            lScanningTime.Size = new Size(17, 20);
            lScanningTime.TabIndex = 10;
            lScanningTime.Text = "0";
            // 
            // tScanningTime
            // 
            tScanningTime.Interval = 1000;
            tScanningTime.Tick += tScanningTime_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lScanningTime);
            Controls.Add(lScanningTimeDesc);
            Controls.Add(tbSavePath);
            Controls.Add(bSavePath);
            Controls.Add(lProductsCount);
            Controls.Add(lPagesVisited);
            Controls.Add(lProductsCountDesc);
            Controls.Add(lPagesVisitedDesc);
            Controls.Add(cbWebsites);
            Controls.Add(listViewProducts);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ListView listViewProducts;
        private ColumnHeader ItemName;
        private ColumnHeader ItemPrice;
        private ColumnHeader ItemAddress;
        private ComboBox cbWebsites;
        private Label lPagesVisitedDesc;
        private Label lProductsCountDesc;
        private Label lPagesVisited;
        private Label lProductsCount;
        private OpenFileDialog openFileDialog1;
        private Button bSavePath;
        private TextBox tbSavePath;
        private System.Windows.Forms.Timer tSaveFile;
        private System.Windows.Forms.Timer tScannedCounter;
        private Label lScanningTimeDesc;
        private Label lScanningTime;
        private System.Windows.Forms.Timer tScanningTime;
    }
}