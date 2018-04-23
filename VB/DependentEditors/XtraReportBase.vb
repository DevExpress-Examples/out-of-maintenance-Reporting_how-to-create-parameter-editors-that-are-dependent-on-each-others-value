Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
' ...

Namespace DependentEditors
	Friend Enum TimePeriod
		Custom
		Month
		ThreeMonths
		SixMonths
		Year
		TwoYears
		FiveYears
		TenYears
	End Enum

	Public Class XtraReportBase
		Inherits XtraReport

		#Region "inner classes"

		Private Class DateTimeCorrector
			Private ReadOnly months As Integer
			Private ReadOnly years As Integer

			Public Sub New(ByVal months As Integer, ByVal years As Integer)
				Me.months = months
				Me.years = years
			End Sub

			Public ReadOnly Property IsEmpty() As Boolean
				Get
					Return months = 0 AndAlso years = 0
				End Get
			End Property

			Public Function Correct(ByVal time As DateTime) As DateTime
				Return time.AddMonths(months).AddYears(years)
			End Function
		End Class

		Private Class TimePeriodDesignExtention
			Inherits ReportDesignExtension
			Public Overrides Sub AddParameterTypes(ByVal dictionary As IDictionary(Of Type, String))
				dictionary.Add(GetType(TimePeriod), "TimePeriod")
			End Sub
		End Class

		Private Class TimePeriodParametersExtention
			Inherits ParametersRequestExtension
			Private canUpdateEditors As Boolean = True
			Private Shared itemName As String = String.Empty

			Private Shared Function FindParameterInfo(ByVal parametersInfo As IList(Of EditParameterInfo), ByVal name As String) As EditParameterInfo
				itemName = name
				Return New List(Of EditParameterInfo)(parametersInfo).Find(AddressOf FindPredicate)
			End Function

			Private Shared Function FindPredicate(ByVal item As EditParameterInfo) As Boolean
				Return item.Parameter.Name = itemName
			End Function

			Private Shared Function FindEditor(ByVal parametersInfo As IList(Of EditParameterInfo), ByVal name As String) As BaseEdit
				Dim info As EditParameterInfo = FindParameterInfo(parametersInfo, name)
				If info IsNot Nothing Then
					Return info.Editor
				Else
					Return Nothing
				End If
			End Function

			Protected Overrides Sub OnBeforeShow(ByVal parametersInfo As IList(Of EditParameterInfo), ByVal report As XtraReport)
				Dim info As EditParameterInfo = FindParameterInfo(parametersInfo, periodParameterName)
				If info Is Nothing Then
					Return
				End If
				info.Editor = CreatePeriodEditor(System.Enum.GetValues(GetType(TimePeriod)), New String() { "(Custom)", "Month", "3 Months", "6 Months", "Year", "2 Years", "5 Years", "10 Years" })
				Dim timePeriod As TimePeriod = CType(info.Parameter.Value, TimePeriod)
				If timePeriod <> TimePeriod.Custom Then
					Dim corrector As DateTimeCorrector = CreateCorrector(timePeriod)
					Dim endTime As DateTime = DateTime.Now
					FindParameterInfo(parametersInfo, startDateParameterName).Parameter.Value = CObj(corrector.Correct(endTime))
					FindParameterInfo(parametersInfo, endDateParameterName).Parameter.Value = CObj(endTime)
				End If
				Dim startDateEdit As DateEdit = CType(FindEditor(parametersInfo, startDateParameterName), DateEdit)
				startDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
				Dim endDateEdit As DateEdit = CType(FindEditor(parametersInfo, endDateParameterName), DateEdit)
				endDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
			End Sub

			Private Shared Function CreatePeriodEditor(ByVal values As Array, ByVal names() As String) As BaseEdit
				Dim result As New ImageComboBoxEdit()
				Dim i As Integer = 0
				Do While i < values.Length AndAlso i < names.Length
					result.Properties.Items.Add(New ImageComboBoxItem(names(i), values.GetValue(i)))
					i += 1
				Loop
				result.Properties.PopupFormMinSize = New System.Drawing.Size(result.Properties.PopupFormMinSize.Width, result.Properties.PopupFormMinSize.Height + 135)
				Return result
			End Function

			Protected Overrides Sub OnEditorValueChanged(ByVal parametersInfo As IList(Of EditParameterInfo), ByVal changedInstance As EditParameterInfo, ByVal report As XtraReport)
				Select Case changedInstance.Parameter.Name
					Case periodParameterName
						OnPeriodEditValueChanged(parametersInfo)
					Case startDateParameterName
						OnStartDateDateEditValueChanged(parametersInfo)
					Case endDateParameterName
						OnEndDateEditValueChanged(parametersInfo)
					Case Else
						Throw New Exception("Invalid switch's branch.")
				End Select
			End Sub

			Private Sub OnStartDateDateEditValueChanged(ByVal parametersInfo As IList(Of EditParameterInfo))
				If (Not canUpdateEditors) Then
					Return
				End If
				Dim edit As DateEdit = CType(FindEditor(parametersInfo, startDateParameterName), DateEdit)
				If edit Is Nothing Then
					Return
				End If
				edit.DoValidate()
				SetValue(FindEditor(parametersInfo, periodParameterName), TimePeriod.Custom)
			End Sub

			Private Sub OnEndDateEditValueChanged(ByVal parametersInfo As IList(Of EditParameterInfo))
				If (Not canUpdateEditors) Then
					Return
				End If
				Dim edit As DateEdit = CType(FindEditor(parametersInfo, endDateParameterName), DateEdit)
				If edit Is Nothing Then
					Return
				End If
				edit.DoValidate()
				SetValue(FindEditor(parametersInfo, periodParameterName), TimePeriod.Custom)
			End Sub

			Private Sub OnPeriodEditValueChanged(ByVal parametersInfo As IList(Of EditParameterInfo))
				If (Not canUpdateEditors) Then
					Return
				End If
				Dim edit As BaseEdit = FindEditor(parametersInfo, periodParameterName)
				If edit Is Nothing Then
					Return
				End If
				edit.DoValidate()
				Dim corrector As DateTimeCorrector = CreateCorrector(CType(edit.EditValue, TimePeriod))
				If corrector Is Nothing OrElse corrector.IsEmpty Then
					Return
				End If
				Dim endTime As DateTime = DateTime.Now
				SetValue(FindEditor(parametersInfo, startDateParameterName), corrector.Correct(endTime))
				SetValue(FindEditor(parametersInfo, endDateParameterName), endTime)
			End Sub

			Private Sub SetValue(ByVal editor As BaseEdit, ByVal value As Object)
				canUpdateEditors = False
				Try
					If editor IsNot Nothing Then
						editor.EditValue = value
						editor.IsModified = True
						editor.DoValidate()
					End If
				Finally
					canUpdateEditors = True
				End Try
			End Sub
		End Class

		#End Region

		Protected Const startDateParameterName As String = "parameterStartDate"
		Protected Const endDateParameterName As String = "parameterEndDate"
		Protected Const periodParameterName As String = "parameterPeriod"
		Private ReadOnly Shared defaultTimePeriod As TimePeriod = TimePeriod.ThreeMonths

		Shared Sub New()
			ParametersRequestExtension.RegisterExtension(New TimePeriodParametersExtention(), "DependentEditorsReport")
			ReportDesignExtension.RegisterExtension(New TimePeriodDesignExtention(), "DependentEditorsReport")
		End Sub

		Public Sub New()
			ParametersRequestExtension.AssociateReportWithExtension(Me, "DependentEditorsReport")
			ReportDesignExtension.AssociateReportWithExtension(Me, "DependentEditorsReport")

			Dim startDateParameter As Parameter = CreateCustomParameter(startDateParameterName, GetType(DateTime), "Start Date:", CreateCorrector(defaultTimePeriod).Correct(DateTime.Now))
			Dim endDateParameter As Parameter = CreateCustomParameter(endDateParameterName, GetType(DateTime), "End Date:", DateTime.Now)
			Dim periodParameter As Parameter = CreateCustomParameter(periodParameterName, GetType(TimePeriod), "Last Time Span:", defaultTimePeriod)
			Parameters.AddRange(New Parameter() { startDateParameter, endDateParameter, periodParameter })
		End Sub

		Private Shared Function CreateCustomParameter(ByVal name As String, ByVal type As Type, ByVal description As String, ByVal value As Object) As Parameter
			Dim parameter As New Parameter()
			parameter.Name = name
			parameter.Type = type
			parameter.Description = description
			parameter.Value = value
			Return parameter
		End Function

		Private Shared Function CreateCorrector(ByVal timePeriod As TimePeriod) As DateTimeCorrector
			Select Case timePeriod
				Case TimePeriod.Month
					Return New DateTimeCorrector(-1, 0)
				Case TimePeriod.ThreeMonths
					Return New DateTimeCorrector(-3, 0)
				Case TimePeriod.SixMonths
					Return New DateTimeCorrector(-6, 0)
				Case TimePeriod.Year
					Return New DateTimeCorrector(0, -1)
				Case TimePeriod.TwoYears
					Return New DateTimeCorrector(0, -2)
				Case TimePeriod.FiveYears
					Return New DateTimeCorrector(0, -5)
				Case TimePeriod.TenYears
					Return New DateTimeCorrector(0, -10)
				Case Else
					Return New DateTimeCorrector(0, 0)
			End Select
		End Function
	End Class
End Namespace