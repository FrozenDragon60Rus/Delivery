namespace Delivery
{
	partial class DeliveryForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DGDelivery = new DataGridView();
			panel1 = new Panel();
			ReportButton = new Button();
			NOrderCount = new NumericUpDown();
			label5 = new Label();
			label4 = new Label();
			label3 = new Label();
			DTPTo = new DateTimePicker();
			CLBRegion = new CheckedListBox();
			ResetButton = new Button();
			label2 = new Label();
			label1 = new Label();
			DTPFrom = new DateTimePicker();
			((System.ComponentModel.ISupportInitialize)DGDelivery).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NOrderCount).BeginInit();
			SuspendLayout();
			// 
			// DGDelivery
			// 
			DGDelivery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			DGDelivery.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DGDelivery.Location = new Point(26, 73);
			DGDelivery.Name = "DGDelivery";
			DGDelivery.ReadOnly = true;
			DGDelivery.Size = new Size(528, 350);
			DGDelivery.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(ReportButton);
			panel1.Controls.Add(NOrderCount);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(DTPTo);
			panel1.Controls.Add(CLBRegion);
			panel1.Controls.Add(ResetButton);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(DTPFrom);
			panel1.Location = new Point(560, 73);
			panel1.Name = "panel1";
			panel1.Size = new Size(228, 350);
			panel1.TabIndex = 3;
			// 
			// ReportButton
			// 
			ReportButton.Location = new Point(133, 244);
			ReportButton.Name = "ReportButton";
			ReportButton.Size = new Size(75, 23);
			ReportButton.TabIndex = 15;
			ReportButton.Text = "Выгрузить";
			ReportButton.UseVisualStyleBackColor = true;
			ReportButton.Click += ReportButton_Click;
			// 
			// NOrderCount
			// 
			NOrderCount.Location = new Point(3, 215);
			NOrderCount.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
			NOrderCount.Name = "NOrderCount";
			NOrderCount.Size = new Size(169, 23);
			NOrderCount.TabIndex = 14;
			NOrderCount.ValueChanged += NOrderCount_ValueChanged;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(3, 197);
			label5.Name = "label5";
			label5.Size = new Size(140, 15);
			label5.TabIndex = 13;
			label5.Text = "Количество обращений";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(3, 163);
			label4.Name = "label4";
			label4.Size = new Size(23, 15);
			label4.TabIndex = 12;
			label4.Text = "По";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(3, 134);
			label3.Name = "label3";
			label3.Size = new Size(15, 15);
			label3.TabIndex = 11;
			label3.Text = "С";
			// 
			// DTPTo
			// 
			DTPTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			DTPTo.Format = DateTimePickerFormat.Custom;
			DTPTo.Location = new Point(32, 157);
			DTPTo.Name = "DTPTo";
			DTPTo.Size = new Size(140, 23);
			DTPTo.TabIndex = 10;
			DTPTo.Value = new DateTime(2024, 10, 27, 0, 0, 0, 0);
			DTPTo.ValueChanged += DTPTo_ValueChanged;
			// 
			// CLBRegion
			// 
			CLBRegion.CheckOnClick = true;
			CLBRegion.FormattingEnabled = true;
			CLBRegion.Location = new Point(3, 22);
			CLBRegion.Name = "CLBRegion";
			CLBRegion.Size = new Size(205, 76);
			CLBRegion.TabIndex = 9;
			CLBRegion.ItemCheck += CLBRegion_ItemCheck;
			// 
			// ResetButton
			// 
			ResetButton.Location = new Point(3, 244);
			ResetButton.Name = "ResetButton";
			ResetButton.Size = new Size(75, 23);
			ResetButton.TabIndex = 8;
			ResetButton.Text = "Сбросить";
			ResetButton.UseVisualStyleBackColor = true;
			ResetButton.Click += ResetButton_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(3, 110);
			label2.Name = "label2";
			label2.Size = new Size(69, 15);
			label2.TabIndex = 6;
			label2.Text = "Дата заказа";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(3, 4);
			label1.Name = "label1";
			label1.Size = new Size(46, 15);
			label1.TabIndex = 5;
			label1.Text = "Регион";
			// 
			// DTPFrom
			// 
			DTPFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			DTPFrom.Format = DateTimePickerFormat.Custom;
			DTPFrom.Location = new Point(32, 128);
			DTPFrom.Name = "DTPFrom";
			DTPFrom.Size = new Size(140, 23);
			DTPFrom.TabIndex = 4;
			DTPFrom.Value = new DateTime(2024, 10, 27, 0, 0, 0, 0);
			DTPFrom.ValueChanged += DTPFrom_ValueChanged;
			// 
			// DeliveryForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(panel1);
			Controls.Add(DGDelivery);
			Name = "DeliveryForm";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)DGDelivery).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)NOrderCount).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView DGDelivery;
		private Panel panel1;
		private DateTimePicker DTPFrom;
		private Label label1;
		private Label label2;
		private Button ResetButton;
		private CheckedListBox CLBRegion;
		private Label label4;
		private Label label3;
		private DateTimePicker DTPTo;
		private NumericUpDown NOrderCount;
		private Label label5;
		private Button ReportButton;
	}
}
