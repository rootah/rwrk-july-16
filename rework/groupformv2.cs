using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using lynxs.classes;
using lynxs.forms;
using MongoDB.Bson;

namespace lynxs.controls.v2
{
    public partial class groupformv2 : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly main _mForm;

        public groupformv2(main mForm)
        {
            _mForm = mForm;
            InitializeComponent();
            init();
        }

        private void init()
        {
            groupNoCalc();
            objectIdGen();
            lvl.Select();
        }

        private void objectIdGen()
        {
            var id = ObjectId.GenerateNewId();
            groupid.Text = id.ToString();
        }

        private void groupNoCalc()
        {
            var i = dbActions.groupNo();
            var newno = i + 1;
            num.Text = newno.ToString();
        }
        private void parentResize()
        {
            var parentForm = ParentForm;
            if (parentForm == null) return;
            layoutControl1.BeginUpdate();
            parentForm.ClientSize = new Size(layoutControl1.Root.MinSize.Width, layoutControl1.Root.MinSize.Height);
            layoutControl1.EndUpdate();
        }

        private void layoutControl1_GroupExpandChanged(object sender, DevExpress.XtraLayout.Utils.LayoutGroupEventArgs e)
        {
            parentResize();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            ArrayList wdays = new ArrayList();
            for (int i = 0; i < days.Properties.Items.Count; i++)
            { if (days.Properties.Items[i].CheckState == CheckState.Checked)
                    wdays.Add(days.Properties.Items[i].Value);
            }

            var period = new BsonDocument
            {
                {"pstart", pstart.Text},
                {"pend", pend.Text}
            };

            var grpdoc = new BsonDocument
            {
                {"_id", ObjectId.Parse(groupid.Text)},
                {"groupno", num.Text },
                {"level", lvl.Text },
                {"wdays", new BsonArray(wdays) },
                {"time", time.Time },
                {"period", new BsonDocument(period) }
            };

            dbActions.grpInsert(grpdoc);
            _mForm.groupGridFill();
        }
    }
}
