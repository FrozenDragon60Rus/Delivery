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
			CLBRegion = new CheckedListBox();
			ResetButton = new Button();
			label2 = new Label();
			label1 = new Label();
			DTPFrom = new DateTimePicker();
			((System.ComponentModel.ISupportInitialize)DGDelivery).BeginInit();
			panel1.SuspendLayout();
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
			ResetButton.Location = new Point(68, 186);
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
			DTPFrom.CustomFormat = "yyyy-MM-dd hh:mm:ss";
			DTPFrom.Format = DateTimePickerFormat.Custom;
			DTPFrom.Location = new Point(3, 128);
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
	}
}
