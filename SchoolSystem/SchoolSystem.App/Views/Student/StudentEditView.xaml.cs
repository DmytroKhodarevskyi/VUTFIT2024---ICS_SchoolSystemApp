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