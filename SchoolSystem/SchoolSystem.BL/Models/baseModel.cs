using System;

namespace SchoolSystem.BL.Models
{
    public abstract record baseModel : IModel
    {
        public Guid Id { get; set; }
    }
}
