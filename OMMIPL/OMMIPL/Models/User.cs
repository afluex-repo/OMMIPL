using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OMMIPL.Models;
using System.Web.Mvc;

namespace OMMIPL.Models
{
    public class User : Comman
    {
        public string PK_UserId { get; set; }
        public string DisplayName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public List<Admin> lstReports { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string QRCode { get; set; }
        public string Image { get; set; }
        public string Balance { get; set; }
        public string AddedBy { get; set; }
        public string Result { get; set; }
        public string MainBalance { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public List<Game> lst { get; set; }
        public List<Game> lstColor { get; set; }
        public string PeriodNo { get; set; }
        public string FK_GameId { get; set; }
        public string Duration { get; set; }
        public string FK_ColorId { get; set; }
        public List<SelectListItem> ddlPaymentMode { get; set; }
        public string FK_UserId { get; set; }
        public string GameName { get; set; }
        public string EndTime { get; set; }
        public string FK_PeriodId { get; set; }
        public DataSet GetPaymentModeDetails()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeDetails");
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
        public DataSet SaveEwalletRequest()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Amount",Amount),
                    new SqlParameter("@ImageURL",Image),
                     new SqlParameter("@PaymentMode",PaymentMode),
                     new SqlParameter("@BankName",BankName),
                    new SqlParameter("@BankBranch",BankBranch),
                     new SqlParameter("@DDChequeNo",DDChequeNo),
                        new SqlParameter("@DDChequeDate",DDChequeDate),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveEwalletRequest", para);
            return ds;
        }

        public DataSet GetMainBalance()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("UserMainBalance", para);
            return ds;
        }
        public DataSet GetAllGames()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetGameDetails");
            return ds;
        }
        public DataSet GetGameDetailsById()
        {
            SqlParameter[] para = { new SqlParameter("@FK_GameId", FK_GameId),
                new SqlParameter("@FK_UserId", FK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GenerateGamePeriod", para);
            return ds;
        }
        public DataSet GameStart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_GameId", FK_GameId),
                new SqlParameter("@FK_PeriodId", FK_PeriodId),
                new SqlParameter("@FK_ColorId", FK_ColorId),
                new SqlParameter("@FK_UserId", FK_UserId),
                 new SqlParameter("@Amount", Amount),
            };
            DataSet ds = DBHelper.ExecuteQuery("GameStart", para);
            return ds;
        }
    }
}