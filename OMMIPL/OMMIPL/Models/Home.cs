using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OMMIPL.Models;

namespace OMMIPL.Models
{
    public class Home
    {
        public string PK_UserId { get; set; }
        public string Result { get; set; }
        public string DisplayName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string SponsorLoginId { get; set; }
        public string SponsorName { get; set; }
        public string FK_SponsorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConfimPassword { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string AddedBy { get; set; }
        public string UploadQRId { get; set; }
        public List<Home> lstUploadQR { get; set; }


        public DataSet Login()
        {
            SqlParameter[] para = {new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Password",Password)
        };
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
        public DataSet GetSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", SponsorLoginId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetSponsor", para);
            return ds;
        }
        public DataSet Registration()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_SponsorId", FK_SponsorId),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@LastName", LastName),
                                      new SqlParameter("@MobileNo", MobileNo),
                                      new SqlParameter("@Password", Password),
                                      new SqlParameter("@Email", Email),
                                       new SqlParameter("@PinCode", PinCode),
                                       new SqlParameter("@State", State),
                                       new SqlParameter("@City", City),
                                       new SqlParameter("@Address", Address),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }


        public DataSet UploadQR()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MobileNo",MobileNo),
                                       new SqlParameter("@UploadFile", Image),
                                        new SqlParameter("@IsActive", IsActive),
                                         new SqlParameter("@AddedBy", AddedBy),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UploadQR", para);
            return ds;
        }

        public DataSet GetUploadQRDetails()
        {

            SqlParameter[] para = {
                 new SqlParameter("@UploadQRId", UploadQRId),
                                      new SqlParameter("@MobileNo",MobileNo)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUploadQRDetails", para);
            return ds;
        }
        
        public DataSet DeleteUploadQR()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UploadQRId",UploadQRId),
                                         new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteUploadQR", para);
            return ds;
        }


        public DataSet UpdateUploadQR()
        {
            SqlParameter[] para = {
                 new SqlParameter("@UploadQRId",UploadQRId),
                                      new SqlParameter("@MobileNo",MobileNo),
                                       new SqlParameter("@UploadFile", Image),
                                        new SqlParameter("@IsActive", IsActive),
                                         new SqlParameter("@AddedBy", AddedBy),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateUploadQR", para);
            return ds;
        }

        public DataSet ActiveUploadQR()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UploadQRId",UploadQRId),
                                         new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActiveUploadQR", para);
            return ds;
        }

        public DataSet InActiveUploadQR()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UploadQRId",UploadQRId),
                                         new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("InActiveUploadQR", para);
            return ds;
        }




    }
}