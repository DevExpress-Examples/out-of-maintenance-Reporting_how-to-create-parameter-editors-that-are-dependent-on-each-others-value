using DevExpress.XtraReports.UI;
// ...

namespace DependentEditors {
    public partial class XtraReport1 : XtraReportBase {
        public XtraReport1() {
            InitializeComponent();

            startDate.DataBindings.Add(new XRBinding(Parameters[startDateParameterName], 
                "Text", "{0:dd.MM.yyyy}"));
            endDate.DataBindings.Add(new XRBinding(Parameters[endDateParameterName], 
                "Text", "{0:dd.MM.yyyy}"));
            period.DataBindings.Add(new XRBinding(Parameters[periodParameterName], 
                "Text", string.Empty));
        }
    }
}