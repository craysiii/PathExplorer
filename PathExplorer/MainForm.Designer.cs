/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PathExplorer
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView pathDataGridView;
		private System.Windows.Forms.Button addFolderButton;
		private System.Windows.Forms.Button commitPathButton;
		private System.Windows.Forms.Button deleteCurrentCellButton;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.pathDataGridView = new System.Windows.Forms.DataGridView();
            this.addFolderButton = new System.Windows.Forms.Button();
            this.commitPathButton = new System.Windows.Forms.Button();
            this.deleteCurrentCellButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pathDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pathDataGridView
            // 
            this.pathDataGridView.AllowUserToAddRows = false;
            this.pathDataGridView.AllowUserToDeleteRows = false;
            this.pathDataGridView.AllowUserToResizeColumns = false;
            this.pathDataGridView.AllowUserToResizeRows = false;
            this.pathDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pathDataGridView.Location = new System.Drawing.Point(16, 15);
            this.pathDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pathDataGridView.Name = "pathDataGridView";
            this.pathDataGridView.Size = new System.Drawing.Size(663, 406);
            this.pathDataGridView.TabIndex = 0;
            this.pathDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.PathDataGridViewCellValidating);
            // 
            // addFolderButton
            // 
            this.addFolderButton.Location = new System.Drawing.Point(15, 431);
            this.addFolderButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addFolderButton.Name = "addFolderButton";
            this.addFolderButton.Size = new System.Drawing.Size(107, 31);
            this.addFolderButton.TabIndex = 2;
            this.addFolderButton.Text = "Add Folder";
            this.addFolderButton.UseVisualStyleBackColor = true;
            this.addFolderButton.Click += new System.EventHandler(this.AddFolderButtonClick);
            // 
            // commitPathButton
            // 
            this.commitPathButton.Location = new System.Drawing.Point(481, 431);
            this.commitPathButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.commitPathButton.Name = "commitPathButton";
            this.commitPathButton.Size = new System.Drawing.Size(200, 31);
            this.commitPathButton.TabIndex = 3;
            this.commitPathButton.Text = "Commit PATH Changes";
            this.commitPathButton.UseVisualStyleBackColor = true;
            this.commitPathButton.Click += new System.EventHandler(this.CommitPathButtonClick);
            // 
            // deleteCurrentCellButton
            // 
            this.deleteCurrentCellButton.Location = new System.Drawing.Point(231, 431);
            this.deleteCurrentCellButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.deleteCurrentCellButton.Name = "deleteCurrentCellButton";
            this.deleteCurrentCellButton.Size = new System.Drawing.Size(133, 31);
            this.deleteCurrentCellButton.TabIndex = 4;
            this.deleteCurrentCellButton.Text = "Delete Folder";
            this.deleteCurrentCellButton.UseVisualStyleBackColor = true;
            this.deleteCurrentCellButton.Click += new System.EventHandler(this.DeleteCurrentCellButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 471);
            this.Controls.Add(this.deleteCurrentCellButton);
            this.Controls.Add(this.commitPathButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.pathDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Path Explorer";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pathDataGridView)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
