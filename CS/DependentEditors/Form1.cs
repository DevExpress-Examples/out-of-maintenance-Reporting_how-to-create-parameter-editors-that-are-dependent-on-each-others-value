using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
// ...

namespace DependentEditors {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            Load += Form1_Load;

            XtraReport1 report = new XtraReport1();
            report.CreateDocument(false);
            ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            reportPrintTool.AutoShowParametersPanel = true;
            reportPrintTool.ShowPreviewDialog();
        }

        void Form1_Load(object sender, System.EventArgs e) {
            Close();
        }
    }
}