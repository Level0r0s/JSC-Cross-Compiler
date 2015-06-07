using TestADBBattery;
using TestADBBattery.Experimental;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestADBBattery
{
    public partial class ApplicationControl : UserControl
    //public partial class ApplicationControl : WebUserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void chart1_Click(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/13350036/c-sharp-charts-add-multiple-series-from-datatable
            // http://msdn.microsoft.com/en-us/library/dd456766(v=vs.110).aspx
            //this.book1Sheet1BindingSourceBindingSource.AddNew();
            this.chart1.DataBind();

        }

        private void book1Sheet1BindingSourceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripContainer.get_ContentPanel()]

        }

        private void ApplicationControl_SizeChanged(object sender, System.EventArgs e)
        {
            // 41:26920ms { Name = , Siblin
            // no. not in the browser. why?


            // dod docked controls get the event? do we?
            //Console.WriteLine(
            //    new { this.ParentForm.Name } +
            //    " ApplicationControl_SizeChanged");
        }



        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            this.Invalidate();

            this.Refresh();


            // can we add a new datarow?
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150606/battery

            // Error	5	The type or namespace name 'Book1Sheeht1Row' does not exist in the namespace 'TestADBBattery.Data' (are you missing an assembly reference?)	X:\jsc.svn\examples\javascript\forms\Test\TestADBBattery\TestADBBattery\ApplicationControl.cs	73	30	TestADBBattery

            var item0 = this.book1Sheet1BindingSourceBindingSource[0];

            var n = new global::TestADBBattery.Data.Book1Sheet1Row
            {

                level = 1,
                currentnow = 1
            };

            // how do we add a new row???


            //var datarow = (System.Data.DataRowView)

            //public static implicit operator Book1Sheet1Row(DataRow value);
            //public static implicit operator Book1Sheet1Row(DataRowView value);

            //var ds = book1Sheet1BindingSourceBindingSource.DataSource;

            //book1Sheet1BindingSourceBindingSource.Add(
            //    n
            //);



            var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
            var args = "shell dumpsys battery";

            var psi = new System.Diagnostics.ProcessStartInfo(
                cmd,
                args
                )
            {
                // dont want no flashing black boxes..
                CreateNoWindow = true,

                UseShellExecute = false,


                // The Process object must have the UseShellExecute property set to false in order to redirect IO streams.
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var p = System.Diagnostics.Process.Start(psi);

            // StandardOut has not been redirected or the process hasn't started yet.
            var text = p.StandardOutput.ReadToEnd();
            var err = p.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(err))
            {
                this.BackColor = Color.Red;
                return;
            }

            //            ************** Exception Text **************
            //System.FormatException: Input string was not in a correct format.
            //   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
            //   at System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo info)
            //   at System.Int32.Parse(String s)

            //  AC powered: false
            //  USB powered: false
            //  Wireless powered: false

            //  status: 3

            //  health: 2

            //  present: true

            //  level: 97

            //  scale: 100

            //  voltage: 4261

            //  temperature: 332

            //  technology: Li-ion

            //  LED Charging: true

            //  LED Low Battery: true

            //  current now: -272

            //  Adaptive Fast Charging Settings: true

            //SUPPORT_LOG_BATTERY_USAGE: true

            //  isTablet: false

            //  mBatteryMaxTemp: 440

            //  mBatteryMaxCurrent: 2620

            //  mBatteryAsocEfs: 99

            //  mBatteryAsocNow: 103

            var _level = int.Parse(text.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r"));
            var _currentnow = int.Parse(text.SkipUntilIfAny("current now: ").TakeUntilOrEmpty("\r"));

            var nn = (System.Data.DataRowView)book1Sheet1BindingSourceBindingSource.AddNew();

            // did it show up in the chart?
            nn.Row["iteration"] = book1Sheet1BindingSourceBindingSource.Count;
            //nn.Row["level"] = 7;
            //nn.Row["currentnow"] = 100;

            nn.Row["level"] = _level;
            nn.Row["currentnow"] = _currentnow;

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Desktop\TaskbarProgress.cs
            // not working for RemoteApp ? 
            ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowProgress(_level);
            //Console.Title = new { _level }.ToString();

            this.FindForm().Text = new { _level }.ToString();

            // error: device not found

            // resynch
            this.chart1.DataBind();

            this.BackColor = Color.White;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // display an awesome countdown

            var sw = Stopwatch.StartNew();

            new { }.With(
                async delegate
                {
                    toolStripProgressBar1.Maximum = timer1.Interval;

                    do
                    {
                        if (IsDisposed)
                            return;

                        var v = timer1.Interval - sw.ElapsedMilliseconds;



                        toolStripProgressBar1.Value = (int)v;

                        await Task.Delay(1000 / 15);
                    }
                    while (sw.ElapsedMilliseconds < timer1.Interval);

                }
            );


            toolStripButton1.PerformClick();
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            this.toolStripButton2.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Process.Start("cmd",
                @"/K  x:\util\android-sdk-windows\platform-tools\adb.exe connect 192.168.1.126:5550");

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
              Process.Start("cmd",
                @"/K  x:\util\android-sdk-windows\platform-tools\adb.exe logcat");

        }

    }
}
