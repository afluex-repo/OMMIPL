﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OMMIPL.Models;
using System.Web.Mvc;

namespace OMMIPL.Models
{
    public class User:Comman
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
        public List<SelectListItem> ddlPaymentMode { get; set;}
        
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
    }
}