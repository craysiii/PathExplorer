/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 */

using System;
using System.Windows.Forms;

namespace PathExplorer
{
    /// <summary>
    ///     Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly PathExplorer _explorer;

        public MainForm()
        {
            InitializeComponent();

            _explorer = new PathExplorer();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            pathDataGridView.DataSource = _explorer.Paths;
            pathDataGridView.RowHeadersVisible = false;
            pathDataGridView.Columns[0].Width = pathDataGridView.Width - 3;
        }

        private void PathDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (pathDataGridView.IsCurrentCellDirty)
            {
                pathDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void PathDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("The entered value does not exist on the file system.");
        }

        private void AddFileButtonClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _explorer.AddPath(dialog.FileName);
            }
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
            _explorer.DeletePath(pathDataGridView.CurrentRow.Index);
        }
    }
}