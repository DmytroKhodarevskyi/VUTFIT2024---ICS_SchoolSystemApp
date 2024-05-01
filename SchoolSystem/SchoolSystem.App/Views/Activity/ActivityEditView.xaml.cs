using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels.Activity;
using SchoolSystem.App.Converters;


namespace SchoolSystem.App.Views.Activity;

public partial class ActivityEditView
{
    public ActivityEditView(ActivityEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

}