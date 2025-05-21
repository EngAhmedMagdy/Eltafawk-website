
using EltafawkPlatform.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EltafawkPlatform.Services
{
    public class CourseService
    {
        private readonly IMongoCollection<CourseModel> _courses;
        private readonly IWebHostEnvironment _env;

        public CourseService(IMongoDatabase database, IWebHostEnvironment env)
        {
            _courses = database.GetCollection<CourseModel>("Courses");
            _env = env;
        }

        public async Task<List<CourseModel>> GetAllCoursesAsync() =>
            await _courses.Find(_ => true).ToListAsync();

        public async Task<CourseModel> GetCourseByIdAsync(string id) =>
            await _courses.Find(c => c.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateCourseAsync(CourseModel model)
        {
            await _courses.InsertOneAsync(model);
        }

        public async Task UpdateCourseAsync(string id, CourseModel model)
        {
            model.Id = ObjectId.Parse(id);
            await _courses.ReplaceOneAsync(c => c.Id == model.Id, model);
        }

        public async Task<bool> DeleteCourseAsync(string id)
        {
            var result = await _courses.DeleteOneAsync(c => c.Id == ObjectId.Parse(id));
            return result.DeletedCount > 0;
        }

        
    }

}
