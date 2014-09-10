/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;


namespace PathExplorer
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private PathExplorer explorer;

		public MainForm()
		{
			InitializeComponent();
			
			explorer = new PathExplorer();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			this.pathDataGridView.DataSource = explorer.Paths;
			this.pathDataGridView.RowHeadersVisible = false;
			this.pathDataGridView.Columns[0].Width = this.pathDataGridView.Width - 3;
		}
		
		void PathDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if ( pathDataGridView.IsCurrentCellDirty )
			{
				pathDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}
		
		void PathDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			MessageBox.Show("The entered value does not exist on the file system.");
		}

		void AddFileButtonClick(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			DialogResult result = dialog.ShowDialog();
			
			if ( result == DialogResult.OK )
			{
				explorer.addPath(dialog.FileName);
			}
		}
		
		void AddFolderButtonClick(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog();
			DialogResult result = dialog.ShowDialog();
			
			if ( result == DialogResult.OK )
			{
				explorer.addPath(dialog.SelectedPath);
			}
		}
		
		void CommitPathButtonClick(object sender, EventArgs e)
		{
			// Alert user that change is permenant
			DialogResult result = MessageBox.Show("Are you sure you want to commit? Changes are permanent!", "Commit PATH Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			
			if (result == DialogResult.Yes)
			{
				explorer.commitPath();
			}
		}
		
		void DeleteCurrentCellButtonClick(object sender, EventArgs e)
		{
			explorer.deletePath(pathDataGridView.CurrentRow.Index);
		}
	}
}
