/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
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
		
		// Path class that will be used to validate file and folder locations.
		public class Path
		{
			private string value;
			
			[DisplayName("Path Variable")]
			public string Value
			{
				get
				{
					return this.value;
				}
				set
				{
					if ( File.Exists(value) ||  Directory.Exists(value) )
					{
						this.value = value;
					}
					
					else
					{
						MessageBox.Show(value);
						throw new FileNotFoundException(@value + "does not exist.");
					}
				}
			}
			
			public Path(string value)
			{
				this.Value = value;
			}
			
		}
		
		// PathExplorer class that will be used for handling getting and setting the path in the registry.
		public class PathExplorer
		{
			private readonly string PathKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
			BindingList<Path> paths = new BindingList<Path>();
			
			public PathExplorer()
			{
				string temp = (string)Registry.GetValue(PathKey, "Path", null);
				var list = temp.Split(';');
				foreach( string item in list )
				{
					this.addPath(item);
				}
				
			}
			
			public BindingList<Path> Paths
			{
				get
				{
					return this.paths;
				}
			}
			
			public void addPath(string value)
			{
				var path = new Path(value);
				this.paths.Add(path);
				
			}
			
			public void deletePath(int index)
			{
				this.paths.RemoveAt(index);
			}
			
			public void commitPath()
			{
				string absolutePath = "";
				foreach (Path path in this.paths)
				{
					absolutePath += (path.Value + ';');
				}
				
				Registry.SetValue(PathKey, "Path", absolutePath.Substring(0, absolutePath.Length -1));
			}
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
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
