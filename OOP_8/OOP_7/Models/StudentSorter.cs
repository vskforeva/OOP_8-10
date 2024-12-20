using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_8.Models
{
    internal class StudentSorter
    {
        private bool _isAscending = true; // Флаг для отслеживания порядка сортировки

        public DataTable SortByRecordBookNumber(DataTable students)
        {
            if (students == null || students.Rows.Count == 0)
                return students;

            // Сортируем по номеру зачетки
            string sortDirection = _isAscending ? "ASC" : "DESC";
            DataView view = new DataView(students);
            view.Sort = $"record_book_number {sortDirection}";

            // Меняем порядок сортировки для следующего нажатия
            _isAscending = !_isAscending;

            return view.ToTable(); // Возвращаем отсортированную таблицу
        }

        public void ResetSort()
        {
            _isAscending = true; // Сбрасываем порядок сортировки
        }
    }
}

