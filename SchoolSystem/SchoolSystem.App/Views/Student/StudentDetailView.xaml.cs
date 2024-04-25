using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Student;

public partial class StudentDetailView
{
    public StudentDetailView(StudentDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}