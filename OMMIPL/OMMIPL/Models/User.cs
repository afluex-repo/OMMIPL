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
        public List<User> lstReports { get; set; }
        public List<User> lstLedget { get; set; }

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
        public string IFSC { get; set; }
        public string AccountNo { get; set; }
        public string HolderName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Name { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string DebitAmount { get; set; }
        public string CreditAmount { get; set; }
        public string oldPassword { get; set; }
        public string NewPassword { get; set; }
        public string CfPassword { get; set; }
        public List<SelectListItem> ddlPaymentMode { get; set; }
        public string Narration { get; set; }
        public List<Game> lst { get; set; }
        public List<Game> lstColor { get; set; }
        public List<Game> lstPrediction { get; set; }
        public List<Game> lstResult { get; set; }
        public string PeriodNo { get; set; }
        public string FK_GameId { get; set; }
        public string Duration { get; set; }
        public string FK_ColorId { get; set; }
        public string FK_UserId { get; set; }
        public string GameName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string FK_PeriodId { get; set; }
        public string Message { get; set; }
        public string ColorName { get; set; }
        public TimeSpan time { get; set; }
        public string duration { get; set; }
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
        public DataSet WithdrawlRequest()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("WithdrawlRequest", para);
            return ds;
        }

        public DataSet UpdateAccountDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId),
                new SqlParameter("@BankHolderName",HolderName),
                 new SqlParameter("@accountno",AccountNo),
                 new SqlParameter("@bankname",BankName),
                 new SqlParameter("@ifsc",IFSC),
                 new SqlParameter("@branchname",BankBranch),
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveAccountDetails", para);
            return ds;
        }

        public DataSet GetEwalletLedger()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId),
                new SqlParameter("@todate",ToDate),
                 new SqlParameter("@fromdate",FromDate),

            };
            DataSet ds = DBHelper.ExecuteQuery("EWalletLedger", para);
            return ds;
        }

        public DataSet ChangePsssword()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",PK_UserId),
                  new SqlParameter("@oldpassword",oldPassword),
                new SqlParameter("@Password",NewPassword)
            };
            DataSet ds = DBHelper.ExecuteQuery("Changepassword", para);
            return ds;
        }
        public DataSet GetAllGames()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetGameDetails");
            return ds;
        }
        public DataSet GenrateGamePeriod()
        {
            DataSet ds = DBHelper.ExecuteQuery("GenerateGamePeriod");
            return ds;
        }
        public DataSet GenerateGameResponse()
        {
            DataSet ds = DBHelper.ExecuteQuery("GenerateGameResponse");
            return ds;
        }
        public DataSet GetGameDetailsById()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@FK_GameId",FK_GameId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GameDetailsById", para);
            return ds;
        }
        public DataSet GetUserGamePrediction()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GameReportForUser", para);
            return ds;
        }
        public DataSet GetGamePeriod()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetGamePeriod");
            return ds;
        }
        public DataSet GetGameResponse()
        {
            SqlParameter[] para = { new SqlParameter("@FK_PeriodId", FK_PeriodId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetGameResponse", para);
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
            DataSet ds = DBHelper.ExecuteQuery("SaveUserGameRequest", para);
            return ds;
        }
    }
}