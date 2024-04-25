using SchoolSystem.App.ViewModels.Students;

namespace SchoolSystem.App.Views.Student;

public partial class StudentDetailView
{
    public StudentDetailView(StudentDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}