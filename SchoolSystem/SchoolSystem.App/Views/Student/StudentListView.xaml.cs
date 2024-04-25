using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Students;

namespace SchoolSystem.App.Views.Student;

public partial class StudentListView
{
    public StudentListView(StudentListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}