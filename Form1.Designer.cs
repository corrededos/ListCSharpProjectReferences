namespace WindowsFormsApplication3
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearchFolderPath = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtReferences = new System.Windows.Forms.TextBox();
            this.txtFilesSearched = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(763, 89);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get References from folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearchFolderPath
            // 
            this.txtSearchFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFolderPath.Location = new System.Drawing.Point(12, 12);
            this.txtSearchFolderPath.Name = "txtSearchFolderPath";
            this.txtSearchFolderPath.Size = new System.Drawing.Size(763, 20);
            this.txtSearchFolderPath.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 133);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtReferences);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtFilesSearched);
            this.splitContainer1.Size = new System.Drawing.Size(763, 410);
            this.splitContainer1.SplitterDistance = 546;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtReferences
            // 
            this.txtReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReferences.Location = new System.Drawing.Point(0, 0);
            this.txtReferences.Multiline = true;
            this.txtReferences.Name = "txtReferences";
            this.txtReferences.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReferences.Size = new System.Drawing.Size(546, 410);
            this.txtReferences.TabIndex = 2;
            // 
            // txtFilesSearched
            // 
            this.txtFilesSearched.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilesSearched.Location = new System.Drawing.Point(0, 0);
            this.txtFilesSearched.Multiline = true;
            this.txtFilesSearched.Name = "txtFilesSearched";
            this.txtFilesSearched.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFilesSearched.Size = new System.Drawing.Size(213, 410);
            this.txtFilesSearched.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 555);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txtSearchFolderPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "List C# project references";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearchFolderPath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtReferences;
        private System.Windows.Forms.TextBox txtFilesSearched;
    }
}

