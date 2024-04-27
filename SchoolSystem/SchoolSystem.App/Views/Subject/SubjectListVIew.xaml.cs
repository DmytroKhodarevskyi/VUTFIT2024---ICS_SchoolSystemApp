using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Subjects;

namespace SchoolSystem.App.Views.Subject;

public partial class SubjectListView
{
    public SubjectListView(SubjectListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}