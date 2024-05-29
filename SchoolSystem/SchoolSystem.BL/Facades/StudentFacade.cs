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

        protected override ICollection<string> IncludesNavigationPathDetail =>
            new[]
            {
                $"{nameof(StudentEntity.Subjects)}.{nameof(SubjectEntity.Students)}"
            };

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
        
        public async Task<IEnumerable<StudentListModel>> GetStudentsBySubjectAsync(string subjectAbbr)
        {
            await using var uow = _unitOfWorkFactory.Create();

            var query = uow.GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .Include($"{nameof(StudentEntity.Subjects)}")
                .Where(student => student!.Subjects.Any(subject => subject.Abbreviation == subjectAbbr));
            var students = new List<StudentListModel>(); 
            foreach (var instance in query)
            {
                var model = mapper.MapToListModel(instance);
                students.Add(model); 
            }

            return students;
        }
        
        public async Task<string> GetStudentNameByIdAsync(Guid? id)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var student = await uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                .FirstOrDefaultAsync(s => s.Id == id);
            return student?.Name ?? string.Empty;
        }
        
        public async Task<string> GetStudentSurnameByIdAsync(Guid? id)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var student = await uow.GetRepository<StudentEntity, StudentEntityMapper>().Get()
                .FirstOrDefaultAsync(s => s.Id == id);
            return student?.Surname ?? string.Empty;
        }
        
        public async Task AddSubjectToStudentAsync(Guid studentId, Guid subjectId)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var student = await uow
                .GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .SingleOrDefaultAsync(e => e.Id == studentId);
            var subject = await uow
                .GetRepository<SubjectEntity, SubjectEntityMapper>()
                .Get()
                .Include($"{nameof(SubjectEntity.Students)}.{nameof(StudentEntity.Subjects)}")
                .SingleOrDefaultAsync(e => e.Id == subjectId);
            
            if (student == null || subject == null)
            {
                throw new InvalidOperationException("Student or Subject not found.");
            }
            
            student.Subjects.Add(subject);
            await uow.CommitAsync();
        }

        public async Task RemoveSubjectFromStudentAsync(Guid studentId, string subjectAbbr)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var student = await uow
                .GetRepository<StudentEntity, StudentEntityMapper>()
                .Get()
                .SingleOrDefaultAsync(e => e.Id == studentId);
            var subject = await uow
                .GetRepository<SubjectEntity, SubjectEntityMapper>()
                .Get()
                .Include($"{nameof(SubjectEntity.Students)}.{nameof(StudentEntity.Subjects)}")
                .SingleOrDefaultAsync(e => e.Abbreviation == subjectAbbr);
            
            if (student == null || subject == null)
            {
                throw new InvalidOperationException("Student or Subject not found.");
            }
            
            student.Subjects.Remove(subject);
            await uow.CommitAsync();
        }

    }
}