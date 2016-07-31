using System;
using rework.classes;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;

namespace rework.controls
{
    public partial class groupformv2 : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly RethinkDB R = RethinkDB.R;
        public const string DbName = "ge_initdb";
        public const string TableName = "groups";
        public static Table table = R.Db(DbName).Table(TableName);
        private Connection conn;
        private readonly rwrkmain _mForm;

        public groupformv2(rwrkmain mForm)
        {
            _mForm = mForm;
            InitializeComponent();
            //init();
        }

        public static Connection.Builder DefaultConnectionBuilder()
        {
            return R.Connection()
                .Hostname("127.0.0.1")
                .Port(28015);
        }

        private void okBtn_Click(object sender, System.EventArgs e)
        {
            var myDate = DateTime.ParseExact(startdate.DateTime.ToShortDateString().ToString() + " " + time.Time.TimeOfDay.ToString(),
                    "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            var obj = new Foo { Bar = 1, Baz = 2, Tim = DateTimeOffset.Now };
            Result result = R.Db(@"ge_initdb").Table(@"groups").Insert(obj).Run<Result>(conn);
            
            //using (var db = new LiteDatabase("mydb.db"))
            //{
            //    var col = db.GetCollection("groupcoll");
            //    col.Insert(new BsonDocument()
            //        .Add("_id", new ObjectId(ObjectId.NewObjectId()))
            //        .Add("num", num.Text)
            //        .Add("time", time.Time.ToShortTimeString())
            //        .Add("hcnt", hcnt.Text)
            //        .Add("startdate", myDate));
            //    col.EnsureIndex("num");

            //    // Now, search for document your document
            //    //var customer = col.FindOne(Query.EQ("Name", "john doe"));
            //}
        }

        //    private void init()
        //    {
        //        groupNoCalc();
        //        objectIdGen();
        //        lvl.Select();
        //    }

        //    private void objectIdGen()
        //    {
        //        var id = ObjectId.GenerateNewId();
        //        groupid.Text = id.ToString();
        //    }

        //    private void groupNoCalc()
        //    {
        //        var i = dbActions.groupNo();
        //        var newno = i + 1;
        //        num.Text = newno.ToString();
        //    }
        //    private void parentResize()
        //    {
        //        var parentForm = ParentForm;
        //        if (parentForm == null) return;
        //        layoutControl1.BeginUpdate();
        //        parentForm.ClientSize = new Size(layoutControl1.Root.MinSize.Width, layoutControl1.Root.MinSize.Height);
        //        layoutControl1.EndUpdate();
        //    }

        //    private void layoutControl1_GroupExpandChanged(object sender, DevExpress.XtraLayout.Utils.LayoutGroupEventArgs e)
        //    {
        //        parentResize();
        //    }

        //    private void okBtn_Click(object sender, EventArgs e)
        //    {
        //        ArrayList wdays = new ArrayList();
        //        for (int i = 0; i < days.Properties.Items.Count; i++)
        //        { if (days.Properties.Items[i].CheckState == CheckState.Checked)
        //                wdays.Add(days.Properties.Items[i].Value);
        //        }

        //        var period = new BsonDocument
        //        {
        //            {"pstart", pstart.Text},
        //            {"pend", pend.Text}
        //        };

        //        var grpdoc = new BsonDocument
        //        {
        //            {"_id", ObjectId.Parse(groupid.Text)},
        //            {"groupno", num.Text },
        //            {"level", lvl.Text },
        //            {"wdays", new BsonArray(wdays) },
        //            {"time", time.Time },
        //            {"period", new BsonDocument(period) }
        //        };

        //        dbActions.grpInsert(grpdoc);
        //        _mForm.groupGridFill();
        //    }
    }
}
