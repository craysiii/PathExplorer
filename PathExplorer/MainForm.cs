/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 */

using System;
using System.Windows.Forms;
using System.Drawing;

namespace PathExplorer
{

    public partial class MainForm : Form
    {
        private readonly PathExplorer _explorer;

        public MainForm()
        {
            InitializeComponent();

            _explorer = new PathExplorer();                // Retrieve PATH values
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            pathDataGridView.DataSource = _explorer.Paths; // Bind data to grid
            pathDataGridView.RowHeadersVisible = false;    // Hide first column
            pathDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            // Any invalid paths will be singled out here
            foreach (DataGridViewRow row in pathDataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!_explorer.Paths[cell.RowIndex].isValid() && cell.RowIndex != -1)
                        cell.Style.BackColor = Color.Red;
                }
            }  
        }

        private void PathDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (pathDataGridView.IsCurrentCellDirty)       // Only commit if cell actually changed
            {
                pathDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void PathDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("The entered value does not exist on the file system.");
        }

        private void AddFolderButtonClick(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _explorer.AddPath(dialog.SelectedPath);
            }
        }

        private void CommitPathButtonClick(object sender, EventArgs e)
        {
            // Alert user that change is permenant
            var result = MessageBox.Show("Are you sure you want to commit? Changes are permanent!",
                "Commit PATH Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _explorer.CommitPath();
            }
        }

        private void DeleteCurrentCellButtonClick(object sender, EventArgs e)
        {
            if (pathDataGridView.CurrentRow != null)
            {
                _explorer.DeletePath(pathDataGridView.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("No row selected for deletion.", "Delete Current Row",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}