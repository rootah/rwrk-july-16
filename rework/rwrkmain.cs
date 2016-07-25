using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Internal.Implementations;
using LiteDB;
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
            if (schedcheckButt.Checked)
            {
                schedPanelV2.DockedAsTabbedDocument = true;
                schedPanelV2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
                
            else
            {
                schedPanelV2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
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

        private void gridControl2_Click(object sender, EventArgs e)
        {
             
        }
        public class groupq
        {
            public string num { get; set; }
            public string time { get; set; }
            public string hcnt { get; set; }
            public DateTime startdate { get; set; }
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

                navBarControl2.Groups[0].Caption = @" total groups count: " + coll.Count();
            }
        }

        private void navBarControl2_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            barStaticItem5.Caption = navBarControl2.SelectedLink.Caption;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var no = navBarGroup2.SelectedLink.Caption;
            using (var db = new LiteDatabase("mydb.db"))
            {
                var coll = db.GetCollection<groupq>("groupcoll");
                var results = new BindingList<groupq>(coll.Find(x => x.num == no).ToList());
                gridControl1.DataSource = results;
                foreach (groupq t in results)
                {
                    //var appt = schedulerControl1.Storage.CreateAppointment(AppointmentType.Normal);
                    //appt.Start = t.startdate.Date + t.time.TimeOfDay;
                    //appt.End = appt.Start.AddMinutes(Convert.ToInt32(t.hcnt));
                    //appt.Subject = t.num;
                    //schedulerControl1.Storage.Appointments.Add(appt);
                }
                //MessageBox.Show(text: results.ToString(CultureInfo.InvariantCulture));
            }

            //var apt = schedulerControl1.Storage.CreateAppointment(AppointmentType.Pattern);
            //apt.Start = DateTime.Today.AddHours(9);
            //apt.End = apt.Start.AddMinutes(15);
            //apt.Subject = "My Subject";
            //apt.Location = "My Location";
            //apt.Description = "My Description";

            //apt.RecurrenceInfo.Type = RecurrenceType.Weekly;
            //apt.RecurrenceInfo.Start = apt.Start;
            //apt.RecurrenceInfo.Periodicity = 1;
            //apt.RecurrenceInfo.WeekDays = WeekDays.Friday | WeekDays.Wednesday;
            //apt.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount;
            //apt.RecurrenceInfo.OccurrenceCount = 12;

            //Appointment apt = schedulerControl1.Storage.CreateAppointment(AppointmentType.Normal);
            //apt.Start = DateTime.Today.AddHours(12);
            //apt.Duration = TimeSpan.FromHours(10);
            //apt.Subject = "Subject";
            //apt.Description = "Description";
            //schedulerControl1.Storage.Appointments.Add(apt);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var no = navBarGroup2.SelectedLink.Caption;
            using (var db = new LiteDatabase("mydb.db"))
            {
                var coll = db.GetCollection<groupq>("groupcoll");
                var results = coll.Delete(x => x.num == no);
            }
        }}
}