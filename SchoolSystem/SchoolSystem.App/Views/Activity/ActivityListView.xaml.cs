using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Activity;

namespace SchoolSystem.App.Views.Activity;

public partial class ActivityListView
{
    public ActivityListView(ActivityListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}