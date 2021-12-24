﻿using OMMIPL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMMIPL.Controllers
{
    public class AdminController : BaseController
    {
        // GET: User
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ContactList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ContactList")]
        public ActionResult ContactList(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetContactDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.Name = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.Address = r["Address"].ToString();
                    lst.Add(Obj);
                }
                model.lstContact = lst;
            }
            return View(model);
        }



        public ActionResult GameMaster(string Id)
        {
            Admin model = new Admin();
            if (Id != null)
            {
                model.GameId = Id;
                DataSet ds11 = model.GetGameDetails();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds11.Tables[0].Rows[0]["GameName"].ToString();
                    model.Amount = ds11.Tables[0].Rows[0]["Amount"].ToString();
                    model.Time = ds11.Tables[0].Rows[0]["GameTime"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("GameMaster")]
        public ActionResult SaveGameMaster(Admin model)
        {
            try
            {
                if(model.GameId==null)
                {
                    model.AddedBy = Session["PK_UserId"].ToString();
                    DataSet ds = model.SaveGame();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Game"] = "Record save successfully";
                        }
                        else
                        {
                            TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    model.AddedBy = Session["PK_UserId"].ToString();
                    DataSet ds = model.UpdateGame();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Game"] = "Record update successfully";
                        }
                        else
                        {
                            TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("GameMaster", "Admin");
        }
        
        public ActionResult GameList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("GameList")]
        public ActionResult GameList(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetGameDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.GameId = r["PK_GameId"].ToString();
                    Obj.Name = r["GameName"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Time = r["GameTime"].ToString();
                    lst.Add(Obj);
                }
                model.lstGame = lst;
            }
            return View(model);
        }


        public ActionResult DeleteGameMaster(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.GameId = Id;
                DataSet ds = model.DeleteGame();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Game"] = "Record Deleted Successfully";
                    }
                    else
                    {
                        TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("GameList", "Admin");
        }
    }
}