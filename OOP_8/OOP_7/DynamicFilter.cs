using OOP_8.Models;
using OOP_8.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_8
{
    internal class DynamicFilter
    {
        private readonly StudentModel _model;
        private readonly IStudentView _view;

        public DynamicFilter(StudentModel model, IStudentView view)
        {
            _model = model;
            _view = view;

            // Подписываемся на события изменения данных
            _view.StudentNameChanged += OnFilterChanged;
            _view.StudentRecordBookNumberChanged += OnFilterChanged;
            _view.StudentGroupNumberChanged += OnFilterChanged;
            _view.StudentInstituteChanged += OnFilterChanged;
            _view.StudentMajorChanged += OnFilterChanged;
            _view.StartDateChanged += OnFilterChanged;
            _view.EndDateChanged += OnFilterChanged;
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            // Используем новые свойства для фильтрации
            string name = _view.FilteredStudentName;
            string recordBookNumber = _view.FilteredStudentRecordBookNumber;
            string groupNumber = _view.FilteredStudentGroupNumber;

            // Получаем значения из новых комбобоксов
            string institute = _view.FilteredStudentInstitute != "Все" ? _view.FilteredStudentInstitute : null;
            string major = _view.FilteredStudentMajor != "Все" ? _view.FilteredStudentMajor : null;

            DateTime? startDate = _view.StartDate;
            DateTime? endDate = _view.EndDate;

            DataTable filteredStudents = _model.ReadStudents(name, recordBookNumber, groupNumber, institute, major, startDate, endDate);

            _view.DisplayStudents(filteredStudents);
        }
    }
}

