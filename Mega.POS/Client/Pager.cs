using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Client
{
    public partial class Pager : UserControl
    {
        public Pager()
        {
            InitializeComponent();
        }

        private void InitializeControls()
        {
            var enabled = Data != null && Data.Count() > 0;
            Visible = enabled;
        }

        public int CurrentPage = 1;
        private int PageIndex;
        private int PageSize = DataHelper.GetUDCDataGridViewPageSize(ApplicationHelper.GetPosDataContext());
        public long ItemsCount { get; set; }
        public DataGridView SDataGridView { get; set; }
        private IQueryable<Object> data;
        private int Pages = 14;

        public IQueryable<Object> Data
        {
            set
            {
                CurrentPage = 1;
                PageIndex = 0;
                data = value;
            }

            get { return data; }
        }

        public int PageCount
        {
            get { return (int)Math.Ceiling((double)ItemsCount / PageSize); }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!IsValid) return;
            CurrentPage = int.Parse(((Label)sender).Text);
            Populate(false);
            Cursor = Cursors.Default;
        }

        protected bool IsValid
        {
            get { return Data != null && SDataGridView != null; }
        }

        public void Populate(bool reset = true)
        {
            InitializeControls();

            long count;
            SDataGridView.DataSource = Data.GetPage(CurrentPage, PageSize, out count);
            ItemsCount = count;

            NextButton.Enabled = PageIndex == 0 ? PageCount > Pages : PageCount > (PageIndex * Pages) + Pages;
            PreviuosButton.Enabled = PageIndex != 0;

            PopulatPagesLabels(reset);
            SetCurrentPageLabel();
        }

        private void PopulatPagesLabels(bool reset = true)
        {
            for (var i = 1; i <= Pages; i++)
            {
                var label = LabelsPanel.Controls.OfType<Label>().Where(l => l.Name == "Label" + i).First();

                if ((PageIndex * Pages) + i <= PageCount)
                {
                    label.Text = ((PageIndex * Pages) + i).ToString();
                    label.Visible = true;
                }
                else
                {
                    label.Visible = false;
                }
            }

            if (reset)
            {
                CurrentPage = (PageIndex * Pages) + 1;
            }
        }

        private void SetCurrentPageLabel()
        {
            foreach (var control in LabelsPanel.Controls.OfType<Label>())
            {
                (control).ForeColor = CurrentPage.ToString().Equals((control).Text) ? Color.Blue : Color.Black;
            }
        }

        private void PreviuosButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!IsValid) return;
            PageIndex--;
            PopulatPagesLabels();
            Populate();
            Cursor = Cursors.Default;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!IsValid) return;
            PageIndex++;
            PopulatPagesLabels();
            Populate();
            Cursor = Cursors.Default;
        }

        private void Pager_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }
    }
}
