using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OMMIPL.Models
{
    public class Admin
    {
        public string GameId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Amount { get; set; }
        public string Time { get; set; }
        public string AddedBy { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public List<Admin> lstGame {get;set;}
        public List<Admin> lstContact { get; set; }
        public List<Admin> lstReports { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string RequestID { get; set; }
        
        
        public DataSet SaveGame()
        {
            SqlParameter[] para=
            {
                new SqlParameter("@GameName",Name),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@GameTime",Time),
                  new SqlParameter("@UploadImage",Image1),
                new SqlParameter("@UploadRule",Image2),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveGame", para);
            return ds;
        }

        public DataSet UpdateGame()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@GameId",GameId),
                new SqlParameter("@GameName",Name),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@GameTime",Time),
                 new SqlParameter("@UploadImage",Image1),
                new SqlParameter("@UploadRule",Image2),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateGame", para);
            return ds;
        }
        
        public DataSet GetGameDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@GameId",GameId),
                   new SqlParameter("@GameName",Name)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetGameDetails", para);
            return ds;
        }
        
        public DataSet DeleteGame()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@GameId",GameId),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteGame", para);
            return ds;
        }

        public DataSet GetContactDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetContactDetails", para);
            return ds;
        }
        public DataSet GetEwalletDetails()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletDetails");
            return ds;
        }



    }
}