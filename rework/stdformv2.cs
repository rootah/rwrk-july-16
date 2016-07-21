using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using lynxs.classes;
using MongoDB.Bson;

namespace lynxs.controls.v2
{
    public partial class stdformv2 : DevExpress.XtraEditors.XtraUserControl
    {
        public stdformv2()
        {
            InitializeComponent();

        }

        public void controlInit()
        {
            idlabel.Text = ObjectId.GenerateNewId().ToString();
            parentResize();
            fakeCheck();
            groupFill();
        }

        private async void groupFill()
        {
            @group.Properties.Items.Clear();
            group.Properties.Items.AddRange(await dbActions.groupComboFill());
        }
        private void fakeCheck()
        {
            var parent = (Form)Parent;
            if (parent == null) return;
            if (parent.Text == @"* editing") return;
            if (parent.Text == @"+ creating")
            {
                if (Properties.Settings.Default.fakegen == true)
                    fakeGen();
            }
        }
        public void fakeGen()
        {
            fname.Text = Faker.Name.First();
            lname.Text = Faker.Name.Last();
            phonemain.Text = Faker.Phone.Number();
            phone.Text = Faker.Phone.Number();
            
        }

        public async void stdEditFormFill(string stdid)
        {
            var stdoc = await dbActions.stdDetail(stdid);
            
            // filling ...
            idlabel.Text = stdid;
            //infoheader.Text = (string)detail["fullname"];
            fname.Text = (string) stdoc["fname"];
            lname.Text = (string) stdoc["lname"];

            try
            {
                underagecheck.Checked = (bool)stdoc["underage"];
            }
            catch (Exception)
            {

                underagecheck.Checked = false;
            }

            try
            {
                individualcheck.Checked = (bool)stdoc["individual"];
            }
            catch (Exception)
            {

                individualcheck.Checked = false;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            layoutControlGroup3.Visibility = underagecheck.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        private void parentResize()
        {
            var parentForm = (Form)Parent;
            if (parentForm == null) return;
            mainlayoutcontrol.BeginUpdate();
            parentForm.ClientSize = new Size(mainlayoutcontrol.Root.MinSize.Width, mainlayoutcontrol.Root.MinSize.Height);
            mainlayoutcontrol.EndUpdate();
        }

        private void stdIns()
        {
            var stdcontacts = new BsonDocument
            {
                {"phonemain", phonemain.Text},
                {"phoneadd", phone.Text}
            };

            var stdoc = new BsonDocument
            {
                {"fname", fname.Text},
                {"lname", lname.Text },
                {"fullname", lname.Text + " " + fname.Text },
                {"groupno", @group.Text },
                {"underage", underagecheck.Checked },
                {"individual", individualcheck.Checked },
                {"cost", cost.Text },
                {"contacts", stdcontacts }
            };

            dbActions.stdInsert(stdoc);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            var parentForm = ParentForm;
            if (parentForm == null) return;
            switch (parentForm.Text)
            {
                case @"* editing":
                    return;
                case @"+ creating":
                    stdIns();
                    break;
            }
        }
    }
}
