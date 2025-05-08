using SchoolManagementSystem.API.Models;

namespace SchoolManagementSystem.API.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        
        Task<ResponseDto> AddStudentAsync(Student student);
        Task<ResponseDto> UpdateStudentAsync(Student student,int rollNo);
        Task<ResponseDto> DeleteStudentAsync(int rollNo);
    }
}
