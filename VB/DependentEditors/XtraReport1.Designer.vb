Imports Microsoft.VisualBasic
Imports System
Namespace DependentEditors
	Partial Public Class XtraReport1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
			Me.xrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
			Me.xrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
			Me.xrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
			Me.period = New DevExpress.XtraReports.UI.XRLabel()
			Me.endDate = New DevExpress.XtraReports.UI.XRLabel()
			Me.startDate = New DevExpress.XtraReports.UI.XRLabel()
			Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
			Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
			Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
			Me.xrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' Detail
			' 
			Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrLabel4, Me.xrLabel3, Me.xrLabel2, Me.period, Me.endDate, Me.startDate})
			Me.Detail.HeightF = 72.99998F
			Me.Detail.Name = "Detail"
			Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' xrLabel4
			' 
			Me.xrLabel4.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.xrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(187.5F, 50F)
			Me.xrLabel4.Name = "xrLabel4"
			Me.xrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.xrLabel4.SizeF = New System.Drawing.SizeF(128.125F, 22.99998F)
			Me.xrLabel4.StylePriority.UseFont = False
			Me.xrLabel4.StylePriority.UseTextAlignment = False
			Me.xrLabel4.Text = "Last Time Span:"
			Me.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
			' 
			' xrLabel3
			' 
			Me.xrLabel3.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.xrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(187.5F, 25F)
			Me.xrLabel3.Name = "xrLabel3"
			Me.xrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.xrLabel3.SizeF = New System.Drawing.SizeF(128.125F, 23F)
			Me.xrLabel3.StylePriority.UseFont = False
			Me.xrLabel3.StylePriority.UseTextAlignment = False
			Me.xrLabel3.Text = "End Date:"
			Me.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
			' 
			' xrLabel2
			' 
			Me.xrLabel2.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.xrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(187.5F, 0F)
			Me.xrLabel2.Name = "xrLabel2"
			Me.xrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.xrLabel2.SizeF = New System.Drawing.SizeF(128.1251F, 23F)
			Me.xrLabel2.StylePriority.UseFont = False
			Me.xrLabel2.StylePriority.UseTextAlignment = False
			Me.xrLabel2.Text = "Start Date:"
			Me.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
			' 
			' period
			' 
			Me.period.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.period.LocationFloat = New DevExpress.Utils.PointFloat(325F, 50F)
			Me.period.Name = "period"
			Me.period.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.period.SizeF = New System.Drawing.SizeF(189.5833F, 22.99998F)
			Me.period.StylePriority.UseFont = False
			Me.period.StylePriority.UseTextAlignment = False
			Me.period.Text = "period"
			Me.period.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
			' 
			' endDate
			' 
			Me.endDate.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.endDate.LocationFloat = New DevExpress.Utils.PointFloat(325F, 25F)
			Me.endDate.Name = "endDate"
			Me.endDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.endDate.SizeF = New System.Drawing.SizeF(189.5833F, 23F)
			Me.endDate.StylePriority.UseFont = False
			Me.endDate.StylePriority.UseTextAlignment = False
			Me.endDate.Text = "endDate"
			Me.endDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
			' 
			' startDate
			' 
			Me.startDate.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.startDate.LocationFloat = New DevExpress.Utils.PointFloat(325F, 0F)
			Me.startDate.Name = "startDate"
			Me.startDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.startDate.SizeF = New System.Drawing.SizeF(189.5833F, 23F)
			Me.startDate.StylePriority.UseFont = False
			Me.startDate.StylePriority.UseTextAlignment = False
			Me.startDate.Text = "startDate"
			Me.startDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
			' 
			' TopMargin
			' 
			Me.TopMargin.Name = "TopMargin"
			Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' BottomMargin
			' 
			Me.BottomMargin.Name = "BottomMargin"
			Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' ReportHeader
			' 
			Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrLabel1})
			Me.ReportHeader.HeightF = 31.04165F
			Me.ReportHeader.Name = "ReportHeader"
			' 
			' xrLabel1
			' 
			Me.xrLabel1.Font = New System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.xrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(125F, 0F)
			Me.xrLabel1.Name = "xrLabel1"
			Me.xrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
			Me.xrLabel1.SizeF = New System.Drawing.SizeF(382.0833F, 31.04165F)
			Me.xrLabel1.StylePriority.UseFont = False
			Me.xrLabel1.StylePriority.UseTextAlignment = False
			Me.xrLabel1.Text = "Dependent parameters demo (change the Last Time Span value)"
			Me.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
			' 
			' XtraReport1
			' 
			Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() { Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader})
			Me.Extensions.Add("ParametersRequestExtension", "DependentEditorsReport")
			Me.Extensions.Add("DataSerializationExtension", "DependentEditorsReport")
			Me.Extensions.Add("DataEditorExtension", "DependentEditorsReport")
			Me.Extensions.Add("ParameterEditorExtension", "DependentEditorsReport")
			Me.RequestParameters = False
			Me.Version = "10.2"
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private Detail As DevExpress.XtraReports.UI.DetailBand
		Private TopMargin As DevExpress.XtraReports.UI.TopMarginBand
		Private BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
		Private startDate As DevExpress.XtraReports.UI.XRLabel
		Private period As DevExpress.XtraReports.UI.XRLabel
		Private endDate As DevExpress.XtraReports.UI.XRLabel
		Private ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
		Private xrLabel1 As DevExpress.XtraReports.UI.XRLabel
		Private xrLabel4 As DevExpress.XtraReports.UI.XRLabel
		Private xrLabel3 As DevExpress.XtraReports.UI.XRLabel
		Private xrLabel2 As DevExpress.XtraReports.UI.XRLabel
	End Class
End Namespace
