Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraReports.UI
' ...

Namespace DependentEditors
	Partial Public Class XtraReport1
		Inherits XtraReportBase
		Public Sub New()
			InitializeComponent()

			startDate.DataBindings.Add(New XRBinding(Parameters(startDateParameterName), "Text", "{0:dd.MM.yyyy}"))
			endDate.DataBindings.Add(New XRBinding(Parameters(endDateParameterName), "Text", "{0:dd.MM.yyyy}"))
			period.DataBindings.Add(New XRBinding(Parameters(periodParameterName), "Text", String.Empty))
		End Sub
	End Class
End Namespace