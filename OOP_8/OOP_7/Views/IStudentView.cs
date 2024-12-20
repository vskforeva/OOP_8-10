using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_8.Views
{

    public interface IStudentView
    {

        int StudentId { get; set; }
        string StudentName { get; set; }
        string StudentRecordBookNumber { get; set; }
        string StudentGroupNumber { get; set; }
        string StudentInstitute { get; set; }
        string StudentMajor { get; set; }
        DateTime StudentEnrollmentDate { get; set; }

        // Новые свойства для фильтрации
        string FilteredStudentName { get; }
        string FilteredStudentRecordBookNumber { get; }
        string FilteredStudentGroupNumber { get; }
        string FilteredStudentInstitute { get; }
        string FilteredStudentMajor { get; }

        DateTime? StartDate { get; } // Новое свойство для начальной даты
        DateTime? EndDate { get; }   // Новое свойство для конечной даты

        event EventHandler AddStudent;
        event EventHandler UpdateStudent;
        event EventHandler DeleteStudent;
        event EventHandler ViewStudents;

        event EventHandler StudentNameChanged; // Событие для изменения имени студента
        event EventHandler StudentRecordBookNumberChanged; // Событие для изменения номера зачетной книжки
        event EventHandler StudentGroupNumberChanged; // Событие для изменения номера группы
        event EventHandler StudentInstituteChanged; // Событие для изменения института
        event EventHandler StudentMajorChanged; // Событие для изменения направления
        event EventHandler StartDateChanged; // Событие для изменения начальной даты
        event EventHandler EndDateChanged;   // Событие для изменения конечной даты

        void DisplayStudents(DataTable students);
    }
}
