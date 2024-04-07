using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface ISubjectFacade : IFacade<SubjectEntity,SubjectListModel, SubjectDetailedModel>;