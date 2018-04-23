Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
' ...

Namespace DependentEditors
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			AddHandler Load, AddressOf Form1_Load

			Dim report As New XtraReport1()
			report.CreateDocument(False)
			Dim reportPrintTool As New ReportPrintTool(report)
			reportPrintTool.AutoShowParametersPanel = True
			reportPrintTool.ShowPreviewDialog()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
			Close()
		End Sub
	End Class
End Namespace