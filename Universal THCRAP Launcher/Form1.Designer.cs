using System.ComponentModel;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.star_button2 = new System.Windows.Forms.Button();
            this.star_button1 = new System.Windows.Forms.Button();
            this.sort_az_button2 = new System.Windows.Forms.Button();
            this.sort_az_button1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.filterByType_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(153, 355);
            this.listBox1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.listBox1, "Choose the run configuration (patch stack).");
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.listBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(11, 89);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox2);
            this.splitContainer1.Size = new System.Drawing.Size(312, 356);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(0, 1);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(152, 355);
            this.listBox2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.listBox2, "Choose the game (executable).");
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.listBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(11, 452);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Close when game starts";
            this.toolTip1.SetToolTip(this.checkBox1, "If checked, the applicated will close itself when starting thcrap.");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(10, 478);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Coded with ♥ at https://github.com/Tudi20/Universal-THCRAP-Launcher ";
            this.toolTip1.SetToolTip(this.label1, "Click to get to the github page.");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // star_button2
            // 
            this.star_button2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star;
            this.star_button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.star_button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.star_button2.Location = new System.Drawing.Point(200, 55);
            this.star_button2.Name = "star_button2";
            this.star_button2.Size = new System.Drawing.Size(25, 25);
            this.star_button2.TabIndex = 6;
            this.toolTip1.SetToolTip(this.star_button2, "Filter for favourites.");
            this.star_button2.UseVisualStyleBackColor = true;
            this.star_button2.Click += new System.EventHandler(this.star_button2_Click);
            // 
            // star_button1
            // 
            this.star_button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star;
            this.star_button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.star_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.star_button1.Location = new System.Drawing.Point(42, 55);
            this.star_button1.Name = "star_button1";
            this.star_button1.Size = new System.Drawing.Size(25, 25);
            this.star_button1.TabIndex = 2;
            this.toolTip1.SetToolTip(this.star_button1, "Filter for favourites.");

            this.star_button1.UseVisualStyleBackColor = true;
            this.star_button1.Click += new System.EventHandler(this.star_button1_Click);
            // 
            // sort_az_button2
            // 
            this.sort_az_button2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Decending;
            this.sort_az_button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sort_az_button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sort_az_button2.Location = new System.Drawing.Point(169, 55);
            this.sort_az_button2.Name = "sort_az_button2";
            this.sort_az_button2.Size = new System.Drawing.Size(25, 25);
            this.sort_az_button2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.sort_az_button2, "Sort in alphabetical order.");
            this.sort_az_button2.UseVisualStyleBackColor = true;
            this.sort_az_button2.Click += new System.EventHandler(this.sort_az_button2_Click);
            // 
            // sort_az_button1
            // 
            this.sort_az_button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Decending;
            this.sort_az_button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sort_az_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sort_az_button1.Location = new System.Drawing.Point(11, 55);
            this.sort_az_button1.Name = "sort_az_button1";
            this.sort_az_button1.Size = new System.Drawing.Size(25, 25);
            this.sort_az_button1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.sort_az_button1, "Sort in alphabetical order.");
            this.sort_az_button1.UseVisualStyleBackColor = true;
            this.sort_az_button1.Click += new System.EventHandler(this.sort_az_button1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Shinmera_Banner_5_mini_size;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(14, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(307, 38);
            this.button1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button1, "Start thcrap with the selected settings.");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // filterByType_button
            // 
            this.filterByType_button.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.GameAndCustom;
            this.filterByType_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.filterByType_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterByType_button.Location = new System.Drawing.Point(231, 55);
            this.filterByType_button.Name = "filterByType_button";
            this.filterByType_button.Size = new System.Drawing.Size(25, 25);
            this.filterByType_button.TabIndex = 7;
            this.toolTip1.SetToolTip(this.filterByType_button, "Filter by Type");
            this.filterByType_button.UseVisualStyleBackColor = true;
            this.filterByType_button.Click += new System.EventHandler(this.filterByType_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 498);
            this.Controls.Add(this.filterByType_button);
            this.Controls.Add(this.star_button2);
            this.Controls.Add(this.star_button1);
            this.Controls.Add(this.sort_az_button2);
            this.Controls.Add(this.sort_az_button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 537);
            this.Name = "Form1";
            this.Text = "Universal THCRAP Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBox1;
        private SplitContainer splitContainer1;
        private ListBox listBox2;
        private CheckBox checkBox1;
        private Button button1;
        private Label label1;
        private ToolTip toolTip1;
        private Button sort_az_button1;
        private Button sort_az_button2;
        private Button star_button1;
        private Button star_button2;
        private Button filterByType_button;
    }
}

