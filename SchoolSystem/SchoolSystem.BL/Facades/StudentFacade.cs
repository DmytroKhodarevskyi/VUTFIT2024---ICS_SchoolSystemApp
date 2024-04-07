using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;

namespace SchoolSystem.BL.Facades
{
    public class StudentFacade(IUnitOfWorkFactory unitOfWorkFactory, StudentModelMapper mapper)
        : CrudFacade<StudentEntity, StudentListModel, StudentDetailedModel, StudentEntityMapper>(unitOfWorkFactory,
            mapper), IStudentFacade
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = unitOfWorkFactory;
        public async Task<IEnumerable<StudentDetailedModel>> GetStudentByNameSurname(string Name, string Surname)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var dbSet = uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                .Where(x => x.Name == Name && x.Surname == Surname);
            IEnumerable<StudentDetailedModel> students = new List<StudentDetailedModel>();
            foreach (var instance in dbSet)
            {
                var model = mapper.MapToDetailModel(instance);
                students.Append(model);
            }
            return students;
        }

        public async Task<IEnumerable<StudentListModel>> GetStudentsByNameAsync(string name)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var query = uow.GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .Where(student => student.Name.Contains(name));

            return mapper.MapToListModel(query);
        }
        
        public async Task<IEnumerable<StudentListModel>> GetStudentsByNameSubject(string name)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
                .Where(subject => subject.Name == name)
                .SelectMany(subject => subject.StudentSubjects)
                .Select(ss => ss.Student)
                .Include(student => student.StudentSubjects)
                .ThenInclude(ss => ss.Subject)
                .Distinct();

            return mapper.MapToListModel(dbSet);
        }
    
        public async Task<IEnumerable<StudentListModel>> GetStudentsByAbbrSubject(string abbreviation)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
                .Where(subject => subject.Abbreviation == abbreviation)
                .SelectMany(subject => subject.StudentSubjects)
                .Select(ss => ss.Student)
                .Include(student => student.StudentSubjects)
                .ThenInclude(ss => ss.Subject)
                .Distinct();

            return mapper.MapToListModel(dbSet);
        }
    }
}