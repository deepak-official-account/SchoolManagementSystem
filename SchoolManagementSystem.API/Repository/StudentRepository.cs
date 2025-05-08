using Dapper;
using Microsoft.Data.SqlClient;
using SchoolManagementSystem.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SchoolManagementSystem.API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public StudentRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<ResponseDto> AddStudentAsync(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {


                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Name", student.Name);
                parameters.Add("@Surname", student.Surname);
                parameters.Add("@Age", student.Age);
                parameters.Add("@PhoneNumber", student.PhoneNumber);


                int rowsAffected = await connection.ExecuteAsync(sql:"dbo.AddStudent", param: parameters, commandType: CommandType.StoredProcedure);
                if (rowsAffected > 0)
                {
                    return new ResponseDto
                    {
                        message = "Student Added Successfully",
                        data = student,
                        IsSuccess = true
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        message = "Student Not Added",

                        IsSuccess = false
                    };

                }
            }
        }

        public async Task<ResponseDto> DeleteStudentAsync(int rollNo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();                  
                    parameters.Add("@RollNo", rollNo);
                parameters.Add("@IsDeleted", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                     await connection.ExecuteAsync(sql: "dbo.DeleteStudent", param: parameters, commandType: CommandType.StoredProcedure);
                bool isDeleted = parameters.Get<bool>("@IsDeleted");

                    if (isDeleted)
                    {
                        return new ResponseDto { message = "Student Deleted Successfully", IsSuccess = true };
                    }
                    else
                    {
                        return new ResponseDto { message = "Error Occurred While Deleting Student or Student Not Found With Given Roll No", IsSuccess = false };
                    }
                
              
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                return await connection.QueryAsync<Student>(
                    sql: "dbo.GetAllStudents",
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<ResponseDto> UpdateStudentAsync(Student student,int rollNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
             
                    var updateParams = new DynamicParameters();
                    updateParams.Add("@RollNo", rollNo);
                    updateParams.Add("@Name", student.Name);
                    updateParams.Add("@Surname", student.Surname);
                    updateParams.Add("@Age", student.Age);
                    updateParams.Add("@PhoneNumber", student.PhoneNumber);
                    updateParams.Add("@IsUpdated",dbType:DbType.Boolean,direction:ParameterDirection.Output);

                     await connection.ExecuteAsync(sql:"dbo.UpdateStudent",param: updateParams, commandType: CommandType.StoredProcedure);
                    bool isUpdated = updateParams.Get<bool>("@IsUpdated");

                    if (isUpdated)
                    {
                        return new ResponseDto
                        {
                            IsSuccess = true,
                            message = "Student updated successfully",
                            data = student
                        };
                    }
                    else
                    {
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            message = "Error occurred while updating student or No student exists with the given Roll No"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    message = "Exception: " + ex.Message
                };
            }
        }

    }
}
