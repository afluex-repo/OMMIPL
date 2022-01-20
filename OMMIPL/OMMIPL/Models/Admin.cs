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
        public List<Admin> lstGame { get; set; }
        public List<Admin> lstContact { get; set; }
        public List<Admin> lstReports { get; set; }
        public List<Admin> lstRegistration { get; set; }
        public List<Admin> lstUserledger { get; set; }
        public List<Admin> lstGameReport { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
        public string AccountName { get; set; }
        public string Number { get; set; }
        public string PK_UserId { get; set; }
        public string LoginID { get; set; }
        public string FathersName { get; set; }
        public string AccountNO { get; set; }
        public string IFSCCode { get; set; }
        public string CreatedDate { get; set; }

        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string CurrentDate { get; set; }
        public string paymodename { get; set; }
        public string paymodeid { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ddlstatus { get; set; }
        public string LastName { get; set; }
        public string PK_PeriodId { get; set; }

        public string Fk_ChosenColorId { get; set; }
        public string FK_ResultId { get; set; }
        public string color { get; set; }
        public string PeriodNo { get; set; }
        public string windate { get; set; }
      

        public DataSet GetRegistrationDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginId",LoginID),
               
            };
            DataSet ds = DBHelper.ExecuteQuery("RegistrationList", para);
            return ds;
        }
        public DataSet SaveGame()
        {
            SqlParameter[] para =
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
        public DataSet GetGameReport()
        {
            SqlParameter[] para =
            {
                   new SqlParameter("@LoginID",LoginID),
                   new SqlParameter("@Action",ddlstatus),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GameReport", para);
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
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletDetails", para);
            return ds;
        }

        public DataSet Approv()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RequestID",RequestId),
                 new SqlParameter("@ApprovedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("Approve", para);
            return ds;
        }


        public DataSet Decline()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RequestID",RequestId),
                 new SqlParameter("@Status",Status),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineEwalletRequest", para);
            return ds;
        }


        public DataSet Delete()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RequestID",RequestId),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("Delete", para);
            return ds;
        }

        public DataSet SaveQRMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AccountName",AccountName),
                 new SqlParameter("@Number",Number),
                  new SqlParameter("@UpLoadQR",Image),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveQRMaster", para);
            return ds;
        }
    }
}