using OOP_8.Models;
using OOP_8.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_8.Presenters
{
    public class StudentPresenter
    {
        private readonly IStudentView view;
        private readonly StudentModel model;
        


        public StudentPresenter(IStudentView view, StudentModel model)
        {
            this.view = view;
            this.model = model;
            

            view.AddStudent += OnAddStudent;
            view.UpdateStudent += OnUpdateStudent;
            view.DeleteStudent += OnDeleteStudent;
            view.ViewStudents += OnViewStudents;

        }

        private void OnAddStudent(object sender, EventArgs e)
        {
            // Логика добавления студента
            model.CreateStudent(view.StudentName, view.StudentRecordBookNumber,
                view.StudentGroupNumber, view.StudentInstitute,
                view.StudentMajor, view.StudentEnrollmentDate);

            // После добавления студента обновляем список
            OnViewStudents(sender, e);
        }

        private void OnUpdateStudent(object sender, EventArgs e)
        {
            model.UpdateStudent(view.StudentId, view.StudentName, 
                view.StudentRecordBookNumber, view.StudentGroupNumber,
                view.StudentInstitute, view.StudentMajor, 
                view.StudentEnrollmentDate);
            OnViewStudents(sender, e);
        }

        private void OnDeleteStudent(object sender, EventArgs e)
        {
            model.DeleteStudent(view.StudentId);
            OnViewStudents(sender, e);
        }

        private void OnViewStudents(object sender, EventArgs e)
        {
            view.DisplayStudents(model.ReadStudents());
        }
    }
}
