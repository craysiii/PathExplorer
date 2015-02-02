using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace PathExplorer
{
    public partial class MainForm : Form
    {
        private readonly PathExplorer _explorer;

        public MainForm()
        {
            InitializeComponent();

            // Check if running as Admin, if not inform that changes can't be saved
            var isAdmin = (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                .IsInRole(WindowsBuiltInRole.Administrator);
            if (!isAdmin)
            {
                MessageBox.Show(
                    "All changes will not be saved, you must run this application as Administrator in order to make changes.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                commitPathButton.Enabled = false;
                deleteCurrentCellButton.Enabled = false;
                addFolderButton.Enabled = false;
                 base.Text += " (Non-Administrator)";
            }

            _explorer = new PathExplorer(); // Retrieve PATH values
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            pathDataGridView.DataSource = _explorer.Paths; // Bind data to grid
            pathDataGridView.RowHeadersVisible = false; // Hide first column
            pathDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Any invalid paths will be singled out here
            foreach (DataGridViewRow row in pathDataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!Path.IsValid(_explorer.Paths[cell.RowIndex].Value) && cell.RowIndex != -1)
                        cell.Style.BackColor = Color.Red;
                }
            }
        }

        private void PathDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (pathDataGridView.IsCurrentCellDirty) // Only commit if cell actually changed
            {
                pathDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (!Path.IsValid(e.FormattedValue.ToString()))
                {
                    pathDataGridView.CurrentCell.Style.BackColor = Color.Red;
                }
                else
                    pathDataGridView.CurrentCell.Style.BackColor = Color.White;
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