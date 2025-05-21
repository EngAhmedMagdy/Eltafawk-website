using EltafawkPlatform.Models;
using MongoDB.Driver;

namespace EltafawkPlatform.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<StudentModel> _students;

        public StudentService(IMongoDatabase database)
        {
            _students = database.GetCollection<StudentModel>("Students");
        }

        public async Task CreateAsync(StudentModel student)
        {
            await _students.InsertOneAsync(student);
        }

        // يمكنك إضافة دوال أخرى مثل GetAll, Update, Delete ...
    }
}
