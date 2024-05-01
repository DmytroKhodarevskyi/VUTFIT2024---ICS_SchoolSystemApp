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
        public async Task<IEnumerable<StudentListModel>> GetStudentByNameSurname(string Name, string Surname)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var dbSet = uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                .Where(x => x.Name == Name && x.Surname == Surname);
            var students = new List<StudentListModel>(); // Directly instantiate as List<StudentDetailedModel>
            foreach (var instance in dbSet)
            {
                var model = mapper.MapToListModel(instance);
                students.Add(model); // Use Add method to add the model to the list
            }
            return students;
        }

        public async Task<IEnumerable<StudentListModel>> GetStudentsByNameAsync(string name)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var query = uow.GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .Where(student => student.Name == name);
            var students = new List<StudentListModel>(); // Directly instantiate as List<StudentDetailedModel>
            foreach (var instance in query)
            {
                var model = mapper.MapToListModel(instance);
                students.Add(model); // Use Add method to add the model to the list
            }
            return students;
        }
        
        public async Task<StudentListModel> GetStudentByNameAsync(string name)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var query = uow.GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .Where(student => student.Name == name);
            var students = new List<StudentListModel>(); // Directly instantiate as List<StudentDetailedModel>
            foreach (var instance in query)
            {
                var model = mapper.MapToListModel(instance);
                students.Add(model); // Use Add method to add the model to the list
            }
            return students.FirstOrDefault();
        }
        
        public async Task<IEnumerable<StudentListModel>> GetStudentsByNameSubject(string name)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var ids = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
                .Where(subject => subject.Name == name)
                .SelectMany(subject => subject.StudentSubjects)
                .Select(ss => ss.StudentId);

            var dbSet = new List<StudentEntity>();
            foreach (var id in ids)
            {
                var students = uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                    .Where(s => s.Id == id);
                await students.ForEachAsync(s => dbSet.Add(s));
            }
            var studentsModels = new List<StudentListModel>(); // Directly instantiate as List<StudentDetailedModel>
            foreach (var instance in dbSet)
            {
                var model = mapper.MapToListModel(instance);
                studentsModels.Add(model); // Use Add method to add the model to the list
            }
            return studentsModels;
        }
    
        public async Task<IEnumerable<StudentListModel>> GetStudentsByAbbrSubject(string abbreviation)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var ids = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
                .Where(subject => subject.Abbreviation == abbreviation)
                .SelectMany(subject => subject.StudentSubjects)
                .Select(ss => ss.StudentId);

            var dbSet = new List<StudentEntity>();
            foreach (var id in ids)
            {
                var students = uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                    .Where(s => s.Id == id);
                await students.ForEachAsync(s => dbSet.Add(s));
            }
            var studentsModels = new List<StudentListModel>(); // Directly instantiate as List<StudentDetailedModel>
            foreach (var instance in dbSet)
            {
                var model = mapper.MapToListModel(instance);
                studentsModels.Add(model); // Use Add method to add the model to the list
            }
            return studentsModels;
        }
    }
}