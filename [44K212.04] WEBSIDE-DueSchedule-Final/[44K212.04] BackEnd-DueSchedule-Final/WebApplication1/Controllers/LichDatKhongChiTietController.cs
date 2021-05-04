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
    public class LichDatKhongChiTietController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LichDatKhongChiTietController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Tìm kiếm

        #endregion

        #region Select Lịch Đặt
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select MaDat,NgayDat,KhungGio,SoLuongSV from LICHDAT where TrangThaiLV = 1";
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
    }
}
