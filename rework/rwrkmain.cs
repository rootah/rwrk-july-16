using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using rework.controls;

namespace rework
{
    public partial class rwrkmain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public rwrkmain()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var grp = new groupformv2(this);
            var newgroup = new XtraForm
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,

            };
            newgroup.Controls.Add(grp);
            newgroup.Controls[0].Dock = DockStyle.Fill;
            newgroup.ClientSize = new Size(grp.layoutControl1.Root.MinSize.Width, grp.layoutControl1.Root.MinSize.Height);
            newgroup.StartPosition = FormStartPosition.CenterParent;

            newgroup.ShowDialog(this);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new stdformv2();
            var newstd = new XtraForm
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false

            };
            newstd.Controls.Add(frm);
            newstd.Controls[0].Dock = DockStyle.Fill;
            newstd.ClientSize = new Size(frm.mainlayoutcontrol.Root.MinSize.Width, frm.mainlayoutcontrol.Root.MinSize.Height);
            newstd.StartPosition = FormStartPosition.CenterParent;
            newstd.Text = @"+ creating";

            //frm.controlInit();
            newstd.ShowDialog(this);
        }

        private void barCheckItem4_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageCategory1.Visible = schedcheckButt.Checked;
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            //barStaticItem5.Caption = ribbonControl1.SelectedPage.Text == @"display" ? @"display" : @"...";
            //checkPaneState();
        }

        //private void checkPaneState()
        //{
        //    groupsPanel.Visible = groupscheckButt.Checked;
        //    stdsPanel.Visible = stdscheckButt.Checked;
        //    ribbonPageCategory1.Visible = schedcheckButt.Checked;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            groupscheckButt.Checked = true;
            stdscheckButt.Checked = true;
        }

        private void groupscheckButt_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupsPanel.Visible = groupscheckButt.Checked;
        }

        private void stdscheckButt_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            stdsPanel.Visible = stdscheckButt.Checked;
        }

        private void backstageViewButtonItem1_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            Close();
        }

        private void gdtlcheckButt_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gdetailPanel.Visible = gdtlcheckButt.Checked;
        }

        private void stddtlPanel_Click(object sender, EventArgs e)
        {

        }

        private void stdtlscheckButt_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            stddtlPanel.Visible = stdtlscheckButt.Checked;
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
    }
}