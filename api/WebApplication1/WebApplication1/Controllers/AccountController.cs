using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select AccId, Username, Pass, Rol from dbo.Account";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Account acc)
        {
            string query = @"
                            insert into dbo.Account 
                            (Username,Pass,Rol)
                            values('" + acc.Username + @"'
                            ,'" + acc.Pass + @"'                           
                            ,'" + acc.Rol + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Account acc)
        {
            string query = @"
                   update dbo.Account set 
                   Username = '" + acc.Username + @"'
                   ,Pass = '" + acc.Pass + @"'
                   ,Rol = '" + acc.Rol + @"'      
                   where AccId = " + acc.AccId + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Account where AccId = " + id + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }


        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] Account acc)
        //{
        //    string query = @"SELECT COUNT(*) FROM dbo.Account WHERE Username = @Username AND Pass = @Pass";
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();

        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myCommand.Parameters.AddWithValue("@Username", acc.Username);
        //            myCommand.Parameters.AddWithValue("@Pass", acc.Pass);
        //            int count = (int)myCommand.ExecuteScalar();

        //            if (count > 0)
        //            {
        //                // Đăng nhập thành công
        //                return Ok(new { message = "Đăng nhập thành công" });
        //            }
        //            else
        //            {
        //                // Đăng nhập thất bại
        //                return BadRequest(new { message = "Thông tin đăng nhập không hợp lệ" });
        //            }
        //        }
        //    }
        //}

        ///////////////////////////////////////////////

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Account acc)
        {
            string query = @"SELECT COUNT(*) FROM dbo.Account WHERE Username = @Username AND Pass = @Pass";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", acc.Username);
                    myCommand.Parameters.AddWithValue("@Pass", acc.Pass);
                    int count = (int)myCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        // Lấy thông tin phân quyền từ cơ sở dữ liệu
                        string roleQuery = @"SELECT Rol FROM dbo.Account WHERE Username = @Username";
                        using (SqlCommand roleCommand = new SqlCommand(roleQuery, myCon))
                        {
                            roleCommand.Parameters.AddWithValue("@Username", acc.Username);
                            string role = (string)roleCommand.ExecuteScalar();

                            if (role == "admin")
                            {
                                return Ok(new { role = "admin", message = "Đăng nhập thành công - Màn hình admin" });
                            }
                            else if (role == "user")
                            {
                                return Ok(new { role = "user", message = "Đăng nhập thành công - Màn hình user" });
                            }
                            else
                            {
                                return BadRequest(new { message = "Phân quyền không hợp lệ" });
                            }
                        }
                    }
                    else
                    {
                        // Đăng nhập thất bại
                        return BadRequest(new { role = "user", message = "Thông tin đăng nhập không hợp lệ" });
                    }
                }
            }
        }

        ///////////////////////////



    }
}
