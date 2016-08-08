using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using LiteDB;
using rework.controls;

// todo: prevent view menu closing on item press

namespace rework
{
    public partial class rwrkmain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public rwrkmain()
        {
            InitializeComponent();
        }

        private void newGroupBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void newStdBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            
            newstd.ShowDialog(this);
        }

        private void barCheckItem4_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedCategory.Visible = schedcheckButt.Checked;
            if (schedcheckButt.Checked)
            {
                schedPanel.DockedAsTabbedDocument = true;
                schedPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
                
            else
            {
                schedPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

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

        private void stdtlscheckButt_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            stddtlPanel.Visible = stdtlscheckButt.Checked;
        }

        public class groupq
        {
            public string num { get; set; }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new LiteDatabase("mydb.db"))
            {
                var coll = db.GetCollection<groupq>("groupcoll");
                var results = new BindingList<groupq>(coll.Find(Query.All()).ToList());
                navBarControl2.Groups[0].ItemLinks.Clear();
                foreach (groupq t in results)
                {
                    var indexitem = navBarControl2.Items.Add();
                    indexitem.Caption = t.num;
                    navBarControl2.Groups[0].ItemLinks.Add(indexitem);
                }
            }
        }

        private void navBarControl2_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            barStaticItem5.Caption = navBarControl2.SelectedLink.Caption;
        }

        private void schedPanel_ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            schedcheckButt.Checked = false;
        }

        private void schedViewCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedCategory.Visible = schedcheckButt.Checked;
            if (schedViewCheck.Checked)
            {
                schedPanel.DockedAsTabbedDocument = true;
                schedPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }

            else
            {
                schedPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }

            if (schedViewCheck.Checked)
                schedCategory.Visible = true;
            else schedCategory.Visible = false;
        }
    }
}