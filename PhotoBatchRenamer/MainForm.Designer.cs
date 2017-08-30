namespace PhotoBatchRenamer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.labelContainingFolder = new System.Windows.Forms.Label();
            this.textBoxContainingFolder = new System.Windows.Forms.TextBox();
            this.buttonContainingFolderBrowse = new System.Windows.Forms.Button();
            this.labelSourceFilenamePrefix = new System.Windows.Forms.Label();
            this.textBoxSourceFilenamePrefix = new System.Windows.Forms.TextBox();
            this.labelSourceFilenameCounterStart = new System.Windows.Forms.Label();
            this.textBoxSourceFilenameCounterStart = new System.Windows.Forms.TextBox();
            this.labelSourceFilenameCounterFinish = new System.Windows.Forms.Label();
            this.textBoxSourceFilenameCounterFinish = new System.Windows.Forms.TextBox();
            this.labelSourceFilenameSuffix = new System.Windows.Forms.Label();
            this.comboBoxSourceFilenameSuffix = new System.Windows.Forms.ComboBox();
            this.labelDivider1 = new System.Windows.Forms.Label();
            this.textBoxDestinationFilenamePrefix = new System.Windows.Forms.TextBox();
            this.labelDestinationFilenamePrefix = new System.Windows.Forms.Label();
            this.textBoxDestinationFilenameCounterInitialValue = new System.Windows.Forms.TextBox();
            this.labelDestinationFilenameCounterInitialValue = new System.Windows.Forms.Label();
            this.textBoxDestinationFilenameSuffix = new System.Windows.Forms.TextBox();
            this.labelDestinationFilenameSuffix = new System.Windows.Forms.Label();
            this.buttonRenamePhotos = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelContainingFolder
            // 
            this.labelContainingFolder.AutoSize = true;
            this.labelContainingFolder.Location = new System.Drawing.Point(30, 33);
            this.labelContainingFolder.Name = "labelContainingFolder";
            this.labelContainingFolder.Size = new System.Drawing.Size(254, 20);
            this.labelContainingFolder.TabIndex = 0;
            this.labelContainingFolder.Text = "Folder Containing Files to Rename";
            // 
            // textBoxContainingFolder
            // 
            this.textBoxContainingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContainingFolder.Location = new System.Drawing.Point(305, 33);
            this.textBoxContainingFolder.Name = "textBoxContainingFolder";
            this.textBoxContainingFolder.Size = new System.Drawing.Size(874, 26);
            this.textBoxContainingFolder.TabIndex = 1;
            this.textBoxContainingFolder.Tag = "ContainingFolderPath";
            this.textBoxContainingFolder.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // buttonContainingFolderBrowse
            // 
            this.buttonContainingFolderBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonContainingFolderBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContainingFolderBrowse.Location = new System.Drawing.Point(1185, 31);
            this.buttonContainingFolderBrowse.Name = "buttonContainingFolderBrowse";
            this.buttonContainingFolderBrowse.Size = new System.Drawing.Size(52, 37);
            this.buttonContainingFolderBrowse.TabIndex = 2;
            this.buttonContainingFolderBrowse.Tag = "ContainingFolderPath";
            this.buttonContainingFolderBrowse.Text = "...";
            this.buttonContainingFolderBrowse.UseVisualStyleBackColor = true;
            this.buttonContainingFolderBrowse.Click += new System.EventHandler(this.OnContainingFolderBrowseClick);
            // 
            // labelSourceFilenamePrefix
            // 
            this.labelSourceFilenamePrefix.AutoSize = true;
            this.labelSourceFilenamePrefix.Location = new System.Drawing.Point(30, 78);
            this.labelSourceFilenamePrefix.Name = "labelSourceFilenamePrefix";
            this.labelSourceFilenamePrefix.Size = new System.Drawing.Size(172, 20);
            this.labelSourceFilenamePrefix.TabIndex = 3;
            this.labelSourceFilenamePrefix.Text = "Source Filename Prefix";
            // 
            // textBoxSourceFilenamePrefix
            // 
            this.textBoxSourceFilenamePrefix.Location = new System.Drawing.Point(305, 75);
            this.textBoxSourceFilenamePrefix.Name = "textBoxSourceFilenamePrefix";
            this.textBoxSourceFilenamePrefix.Size = new System.Drawing.Size(351, 26);
            this.textBoxSourceFilenamePrefix.TabIndex = 4;
            this.textBoxSourceFilenamePrefix.Tag = "SourceFilenamePrefix";
            this.textBoxSourceFilenamePrefix.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelSourceFilenameCounterStart
            // 
            this.labelSourceFilenameCounterStart.AutoSize = true;
            this.labelSourceFilenameCounterStart.Location = new System.Drawing.Point(30, 120);
            this.labelSourceFilenameCounterStart.Name = "labelSourceFilenameCounterStart";
            this.labelSourceFilenameCounterStart.Size = new System.Drawing.Size(103, 20);
            this.labelSourceFilenameCounterStart.TabIndex = 5;
            this.labelSourceFilenameCounterStart.Text = "Start at value";
            // 
            // textBoxSourceFilenameCounterStart
            // 
            this.textBoxSourceFilenameCounterStart.Location = new System.Drawing.Point(305, 117);
            this.textBoxSourceFilenameCounterStart.Name = "textBoxSourceFilenameCounterStart";
            this.textBoxSourceFilenameCounterStart.Size = new System.Drawing.Size(100, 26);
            this.textBoxSourceFilenameCounterStart.TabIndex = 6;
            this.textBoxSourceFilenameCounterStart.Tag = "SourceFilenameCounterStartValue";
            this.textBoxSourceFilenameCounterStart.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelSourceFilenameCounterFinish
            // 
            this.labelSourceFilenameCounterFinish.AutoSize = true;
            this.labelSourceFilenameCounterFinish.Location = new System.Drawing.Point(492, 120);
            this.labelSourceFilenameCounterFinish.Name = "labelSourceFilenameCounterFinish";
            this.labelSourceFilenameCounterFinish.Size = new System.Drawing.Size(110, 20);
            this.labelSourceFilenameCounterFinish.TabIndex = 7;
            this.labelSourceFilenameCounterFinish.Text = "Finish at value";
            // 
            // textBoxSourceFilenameCounterFinish
            // 
            this.textBoxSourceFilenameCounterFinish.Location = new System.Drawing.Point(626, 117);
            this.textBoxSourceFilenameCounterFinish.Name = "textBoxSourceFilenameCounterFinish";
            this.textBoxSourceFilenameCounterFinish.Size = new System.Drawing.Size(100, 26);
            this.textBoxSourceFilenameCounterFinish.TabIndex = 8;
            this.textBoxSourceFilenameCounterFinish.Tag = "SourceFilenameCounterFinishValue";
            this.textBoxSourceFilenameCounterFinish.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelSourceFilenameSuffix
            // 
            this.labelSourceFilenameSuffix.AutoSize = true;
            this.labelSourceFilenameSuffix.Location = new System.Drawing.Point(822, 120);
            this.labelSourceFilenameSuffix.Name = "labelSourceFilenameSuffix";
            this.labelSourceFilenameSuffix.Size = new System.Drawing.Size(49, 20);
            this.labelSourceFilenameSuffix.TabIndex = 9;
            this.labelSourceFilenameSuffix.Text = "Suffix";
            // 
            // comboBoxSourceFilenameSuffix
            // 
            this.comboBoxSourceFilenameSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceFilenameSuffix.FormattingEnabled = true;
            this.comboBoxSourceFilenameSuffix.Items.AddRange(new object[] {
            ".JPG"});
            this.comboBoxSourceFilenameSuffix.Location = new System.Drawing.Point(892, 117);
            this.comboBoxSourceFilenameSuffix.Name = "comboBoxSourceFilenameSuffix";
            this.comboBoxSourceFilenameSuffix.Size = new System.Drawing.Size(121, 28);
            this.comboBoxSourceFilenameSuffix.TabIndex = 10;
            this.comboBoxSourceFilenameSuffix.Tag = "SourceFilenameExtension";
            this.comboBoxSourceFilenameSuffix.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelDivider1
            // 
            this.labelDivider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider1.Location = new System.Drawing.Point(34, 166);
            this.labelDivider1.Name = "labelDivider1";
            this.labelDivider1.Size = new System.Drawing.Size(1033, 10);
            this.labelDivider1.TabIndex = 11;
            // 
            // textBoxDestinationFilenamePrefix
            // 
            this.textBoxDestinationFilenamePrefix.Location = new System.Drawing.Point(305, 200);
            this.textBoxDestinationFilenamePrefix.Name = "textBoxDestinationFilenamePrefix";
            this.textBoxDestinationFilenamePrefix.Size = new System.Drawing.Size(635, 26);
            this.textBoxDestinationFilenamePrefix.TabIndex = 13;
            this.textBoxDestinationFilenamePrefix.Tag = "DestinationFilenamePrefix";
            this.textBoxDestinationFilenamePrefix.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelDestinationFilenamePrefix
            // 
            this.labelDestinationFilenamePrefix.AutoSize = true;
            this.labelDestinationFilenamePrefix.Location = new System.Drawing.Point(30, 203);
            this.labelDestinationFilenamePrefix.Name = "labelDestinationFilenamePrefix";
            this.labelDestinationFilenamePrefix.Size = new System.Drawing.Size(202, 20);
            this.labelDestinationFilenamePrefix.TabIndex = 12;
            this.labelDestinationFilenamePrefix.Text = "Destination Filename Prefix";
            // 
            // textBoxDestinationFilenameCounterInitialValue
            // 
            this.textBoxDestinationFilenameCounterInitialValue.Location = new System.Drawing.Point(305, 242);
            this.textBoxDestinationFilenameCounterInitialValue.Name = "textBoxDestinationFilenameCounterInitialValue";
            this.textBoxDestinationFilenameCounterInitialValue.Size = new System.Drawing.Size(100, 26);
            this.textBoxDestinationFilenameCounterInitialValue.TabIndex = 15;
            this.textBoxDestinationFilenameCounterInitialValue.Tag = "DestinationFilenameCounterInitialValue";
            this.textBoxDestinationFilenameCounterInitialValue.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelDestinationFilenameCounterInitialValue
            // 
            this.labelDestinationFilenameCounterInitialValue.AutoSize = true;
            this.labelDestinationFilenameCounterInitialValue.Location = new System.Drawing.Point(30, 245);
            this.labelDestinationFilenameCounterInitialValue.Name = "labelDestinationFilenameCounterInitialValue";
            this.labelDestinationFilenameCounterInitialValue.Size = new System.Drawing.Size(182, 20);
            this.labelDestinationFilenameCounterInitialValue.TabIndex = 14;
            this.labelDestinationFilenameCounterInitialValue.Text = "Start numbering at value";
            // 
            // textBoxDestinationFilenameSuffix
            // 
            this.textBoxDestinationFilenameSuffix.Location = new System.Drawing.Point(305, 284);
            this.textBoxDestinationFilenameSuffix.Name = "textBoxDestinationFilenameSuffix";
            this.textBoxDestinationFilenameSuffix.Size = new System.Drawing.Size(635, 26);
            this.textBoxDestinationFilenameSuffix.TabIndex = 17;
            this.textBoxDestinationFilenameSuffix.Tag = "DestinationFilenameSuffix";
            this.textBoxDestinationFilenameSuffix.Leave += new System.EventHandler(this.OnLeaveBatchRenameParameterControl);
            // 
            // labelDestinationFilenameSuffix
            // 
            this.labelDestinationFilenameSuffix.AutoSize = true;
            this.labelDestinationFilenameSuffix.Location = new System.Drawing.Point(30, 287);
            this.labelDestinationFilenameSuffix.Name = "labelDestinationFilenameSuffix";
            this.labelDestinationFilenameSuffix.Size = new System.Drawing.Size(186, 20);
            this.labelDestinationFilenameSuffix.TabIndex = 16;
            this.labelDestinationFilenameSuffix.Text = "Suffix (without extension)";
            // 
            // buttonRenamePhotos
            // 
            this.buttonRenamePhotos.Enabled = false;
            this.buttonRenamePhotos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRenamePhotos.Location = new System.Drawing.Point(648, 337);
            this.buttonRenamePhotos.Name = "buttonRenamePhotos";
            this.buttonRenamePhotos.Size = new System.Drawing.Size(292, 54);
            this.buttonRenamePhotos.TabIndex = 18;
            this.buttonRenamePhotos.Text = "&Rename Photos...";
            this.buttonRenamePhotos.UseVisualStyleBackColor = true;
            this.buttonRenamePhotos.Click += new System.EventHandler(this.OnRenamePhotosClicked);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 643);
            this.Controls.Add(this.buttonRenamePhotos);
            this.Controls.Add(this.textBoxDestinationFilenameSuffix);
            this.Controls.Add(this.labelDestinationFilenameSuffix);
            this.Controls.Add(this.textBoxDestinationFilenameCounterInitialValue);
            this.Controls.Add(this.labelDestinationFilenameCounterInitialValue);
            this.Controls.Add(this.textBoxDestinationFilenamePrefix);
            this.Controls.Add(this.labelDestinationFilenamePrefix);
            this.Controls.Add(this.labelDivider1);
            this.Controls.Add(this.comboBoxSourceFilenameSuffix);
            this.Controls.Add(this.labelSourceFilenameSuffix);
            this.Controls.Add(this.textBoxSourceFilenameCounterFinish);
            this.Controls.Add(this.labelSourceFilenameCounterFinish);
            this.Controls.Add(this.textBoxSourceFilenameCounterStart);
            this.Controls.Add(this.labelSourceFilenameCounterStart);
            this.Controls.Add(this.textBoxSourceFilenamePrefix);
            this.Controls.Add(this.labelSourceFilenamePrefix);
            this.Controls.Add(this.buttonContainingFolderBrowse);
            this.Controls.Add(this.textBoxContainingFolder);
            this.Controls.Add(this.labelContainingFolder);
            this.Name = "MainForm";
            this.Text = "Photo Batch Renamer";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelContainingFolder;
        private System.Windows.Forms.TextBox textBoxContainingFolder;
        private System.Windows.Forms.Button buttonContainingFolderBrowse;
        private System.Windows.Forms.Label labelSourceFilenamePrefix;
        private System.Windows.Forms.TextBox textBoxSourceFilenamePrefix;
        private System.Windows.Forms.Label labelSourceFilenameCounterStart;
        private System.Windows.Forms.TextBox textBoxSourceFilenameCounterStart;
        private System.Windows.Forms.Label labelSourceFilenameCounterFinish;
        private System.Windows.Forms.TextBox textBoxSourceFilenameCounterFinish;
        private System.Windows.Forms.Label labelSourceFilenameSuffix;
        private System.Windows.Forms.ComboBox comboBoxSourceFilenameSuffix;
        private System.Windows.Forms.Label labelDivider1;
        private System.Windows.Forms.TextBox textBoxDestinationFilenamePrefix;
        private System.Windows.Forms.Label labelDestinationFilenamePrefix;
        private System.Windows.Forms.TextBox textBoxDestinationFilenameCounterInitialValue;
        private System.Windows.Forms.Label labelDestinationFilenameCounterInitialValue;
        private System.Windows.Forms.TextBox textBoxDestinationFilenameSuffix;
        private System.Windows.Forms.Label labelDestinationFilenameSuffix;
        private System.Windows.Forms.Button buttonRenamePhotos;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}

