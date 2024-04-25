using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Students;

namespace SchoolSystem.App.Views.Student;

public partial class StudentEditView
{
    public StudentEditView(StudentEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}