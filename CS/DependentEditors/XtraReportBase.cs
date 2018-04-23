using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.Extensions;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
// ...

namespace DependentEditors {
    enum TimePeriod { Custom, Month, ThreeMonths, SixMonths, Year, TwoYears, FiveYears, TenYears }

    public class XtraReportBase : XtraReport {

        #region inner classes

        class DateTimeCorrector {
            readonly int months;
            readonly int years;

            public DateTimeCorrector(int months, int years) {
                this.months = months;
                this.years = years;
            }

            public bool IsEmpty { get { return months == 0 && years == 0; } }

            public DateTime Correct(DateTime time) {
                return time.AddMonths(months).AddYears(years);
            }
        }

        class TimePeriodDesignExtention : ReportDesignExtension {
            public override void AddParameterTypes(IDictionary<Type, string> dictionary) {
                dictionary.Add(typeof(TimePeriod), "TimePeriod");
            }
        }

        class TimePeriodParametersExtention : ParametersRequestExtension {
            bool canUpdateEditors = true;
            static string itemName = string.Empty;

            static EditParameterInfo FindParameterInfo(IList<EditParameterInfo> parametersInfo, string name) {
                itemName = name;
                return new List<EditParameterInfo>(parametersInfo).Find(FindPredicate);
            }

            static bool FindPredicate(EditParameterInfo item) {
                return item.Parameter.Name == itemName;
            }

            static BaseEdit FindEditor(IList<EditParameterInfo> parametersInfo, string name) {
                EditParameterInfo info = FindParameterInfo(parametersInfo, name);
                return info != null ? info.Editor : null;
            }

            protected override void OnBeforeShow(IList<EditParameterInfo> parametersInfo, XtraReport report) {
                EditParameterInfo info = FindParameterInfo(parametersInfo, periodParameterName);
                if(info == null)
                    return;
                info.Editor = CreatePeriodEditor(Enum.GetValues(typeof(TimePeriod)), 
                    new string[] { "(Custom)", "Month", "3 Months", "6 Months", "Year", "2 Years", "5 Years", "10 Years" });
                TimePeriod timePeriod = (TimePeriod)info.Parameter.Value;
                if(timePeriod != TimePeriod.Custom) {
                    DateTimeCorrector corrector = CreateCorrector(timePeriod);
                    DateTime endTime = DateTime.Now;
                    FindParameterInfo(parametersInfo, startDateParameterName).Parameter.Value = (object)corrector.Correct(endTime);
                    FindParameterInfo(parametersInfo, endDateParameterName).Parameter.Value = (object)endTime;
                }
                DateEdit startDateEdit = (DateEdit)FindEditor(parametersInfo, startDateParameterName);
                startDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                DateEdit endDateEdit = (DateEdit)FindEditor(parametersInfo, endDateParameterName);
                endDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            }

            static BaseEdit CreatePeriodEditor(Array values, string[] names) {
                ImageComboBoxEdit result = new ImageComboBoxEdit();
                for(int i = 0; i < values.Length && i < names.Length; i++) {
                    result.Properties.Items.Add(new ImageComboBoxItem(names[i], values.GetValue(i)));
                }
                result.Properties.PopupFormMinSize = 
                    new System.Drawing.Size(result.Properties.PopupFormMinSize.Width, 
                    result.Properties.PopupFormMinSize.Height + 135);
                return result;
            }

            protected override void OnEditorValueChanged(IList<EditParameterInfo> parametersInfo, 
                EditParameterInfo changedInstance, XtraReport report) {
                switch(changedInstance.Parameter.Name) {
                    case periodParameterName:
                        OnPeriodEditValueChanged(parametersInfo);
                        break;
                    case startDateParameterName:
                        OnStartDateDateEditValueChanged(parametersInfo);
                        break;
                    case endDateParameterName:
                        OnEndDateEditValueChanged(parametersInfo);
                        break;
                    default:
                        throw new Exception("Invalid switch's branch.");
                }
            }

            void OnStartDateDateEditValueChanged(IList<EditParameterInfo> parametersInfo) {
                if(!canUpdateEditors)
                    return;
                DateEdit edit = (DateEdit)FindEditor(parametersInfo, startDateParameterName);
                if(edit == null) {
                    return;
                }
                edit.DoValidate();
                SetValue(FindEditor(parametersInfo, periodParameterName), TimePeriod.Custom);
            }

            void OnEndDateEditValueChanged(IList<EditParameterInfo> parametersInfo) {
                if(!canUpdateEditors)
                    return;
                DateEdit edit = (DateEdit)FindEditor(parametersInfo, endDateParameterName);
                if(edit == null) {
                    return;
                }
                edit.DoValidate();
                SetValue(FindEditor(parametersInfo, periodParameterName), TimePeriod.Custom);
            }

            void OnPeriodEditValueChanged(IList<EditParameterInfo> parametersInfo) {
                if(!canUpdateEditors)
                    return;
                BaseEdit edit = FindEditor(parametersInfo, periodParameterName);
                if(edit == null) {
                    return;
                }
                edit.DoValidate();
                DateTimeCorrector corrector = CreateCorrector((TimePeriod)edit.EditValue);
                if(corrector == null || corrector.IsEmpty)
                    return;
                DateTime endTime = DateTime.Now;
                SetValue(FindEditor(parametersInfo, startDateParameterName), corrector.Correct(endTime));
                SetValue(FindEditor(parametersInfo, endDateParameterName), endTime);
            }

            void SetValue(BaseEdit editor, object value) {
                canUpdateEditors = false;
                try {
                    if(editor != null) {
                        editor.EditValue = value;
                        editor.IsModified = true;
                        editor.DoValidate();
                    }
                } finally {
                    canUpdateEditors = true;
                }
            }
        }

        #endregion

        protected const string startDateParameterName = "parameterStartDate";
        protected const string endDateParameterName = "parameterEndDate";
        protected const string periodParameterName = "parameterPeriod";
        readonly static TimePeriod defaultTimePeriod = TimePeriod.ThreeMonths;

        static XtraReportBase() {
            ParametersRequestExtension.RegisterExtension(new TimePeriodParametersExtention(), 
                "DependentEditorsReport");
            ReportDesignExtension.RegisterExtension(new TimePeriodDesignExtention(), 
                "DependentEditorsReport");
        }

        public XtraReportBase() {
            ParametersRequestExtension.AssociateReportWithExtension(this, 
                "DependentEditorsReport");
            ReportDesignExtension.AssociateReportWithExtension(this, 
                "DependentEditorsReport");

            Parameter startDateParameter = CreateParameter(startDateParameterName, typeof(DateTime), 
                "Start Date:", CreateCorrector(defaultTimePeriod).Correct(DateTime.Now));
            Parameter endDateParameter = CreateParameter(endDateParameterName, typeof(DateTime), 
                "End Date:", DateTime.Now);
            Parameter periodParameter = CreateParameter(periodParameterName, typeof(TimePeriod), 
                "Last Time Span:", defaultTimePeriod);
            Parameters.AddRange(new Parameter[] { startDateParameter, endDateParameter, periodParameter });
        }

        static Parameter CreateParameter(string name, Type type, string description, object value) {
            Parameter parameter = new Parameter();
            parameter.Name = name;
            parameter.Type = type;
            parameter.Description = description;
            parameter.Value = value;
            return parameter;
        }

        static DateTimeCorrector CreateCorrector(TimePeriod timePeriod) {
            switch(timePeriod) {
                case TimePeriod.Month:
                    return new DateTimeCorrector(-1, 0);
                case TimePeriod.ThreeMonths:
                    return new DateTimeCorrector(-3, 0);
                case TimePeriod.SixMonths:
                    return new DateTimeCorrector(-6, 0);
                case TimePeriod.Year:
                    return new DateTimeCorrector(0, -1);
                case TimePeriod.TwoYears:
                    return new DateTimeCorrector(0, -2);
                case TimePeriod.FiveYears:
                    return new DateTimeCorrector(0, -5);
                case TimePeriod.TenYears:
                    return new DateTimeCorrector(0, -10);
                default:
                    return new DateTimeCorrector(0, 0);
            }
        }
    }
}