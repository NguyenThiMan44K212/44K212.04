using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Modes;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichDatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LichDatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Tìm kiếm

        #endregion

        #region Select Lịch Đặt
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select Lichdat.MaDat, LICHDATCHITIET.MaSV, SINHVIEN.HoTenSV, LICHDAT.NgayDat, LICHDAT.KhungGio from dbo.LICHDAT join dbo.LICHDATCHITIET on LICHDAT.MaDat = LICHDATCHITIET.MaDat join SINHVIEN on LICHDATCHITIET.MaSV = SINHVIEN.MaSV";
            DataTable table = new DataTable();
            //Kết nối Connect SQL
            string sqlDataSource = _configuration.GetConnectionString("DatLichAppCon");
            SqlDataReader myReader;
            //tạo bản sao DataSource
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
        #endregion

        //Dùng để insert dữ liệu bằng chuyển dạng Post
        #region Insert Lịch Đặt
        [HttpPost]
        public JsonResult Post(LichDat dep)
        {
            try
            {
                string query = @"declare @hieu bit exec dbo.PROC_dangki '" + dep.MaSV + "','" + dep.NgayDat + "','" + dep.KhungGio + "',@hieu out";
                DataTable table = new DataTable();
                //Kết nối Connect SQL
                string sqlDataSource = _configuration.GetConnectionString("DatLichAppCon");
                SqlDataReader myReader;
                //tạo bản sao DataSource
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

                return new JsonResult("Added Successfuly");
            }
            catch
            {
                return new JsonResult("failed Successfuly");
            }
            
        }
        #endregion

        //Sửa đổi lịch đặt
        #region Update Lịch Đặt
        [HttpPut]

        public JsonResult Put(LichDat dep)
        {
            string query = @"28:16 youtube";
            DataTable table = new DataTable();
            //Kết nối Connect SQL
            string sqlDataSource = _configuration.GetConnectionString("DatLichAppCon");
            SqlDataReader myReader;
            //tạo bản sao DataSource
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
            return new JsonResult("Updated Successfuly");
        }
        #endregion

        //Xóa lịch đặt
        #region Delete Lịch Đặt
        [HttpDelete("{MaDat}/{MaSV}")]


        public JsonResult Delete(string MaDat, string MaSV)
        {
            try
            {
                string query = @"exec fgetxoaldct '" + MaDat + "','" + MaSV + "'";
                DataTable table = new DataTable();
                //Kết nối Connect SQL
                string sqlDataSource = _configuration.GetConnectionString("DatLichAppCon");
                SqlDataReader myReader;
                //tạo bản sao DataSource
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
                return new JsonResult("Deleted Successfuly");
            }
            catch
            {
                return new JsonResult("Deleted Unsuccessfuly");
            }    
        }
        #endregion
    }
}
