using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace OMMIPL.Models
{
    public class Game
    {
        public string FK_GameId { get; set; }
        public string FK_ColorId { get; set; }
        public string FK_UserId { get; set; }
        public string FK_NumberId { get; set; }
        public decimal Amount { get; set; }
        public string Duration { get; set; }
        public string GameName { get; set; }
        public string ColorName { get; set; }
        public List<Game> lst { get; set; }
        public string Document { get; set; }
        public string Image { get; set; }
        public string GameStartDateTime { get; set; }
        public DataSet GetAllGames()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetGameDetails");
            return ds;
        }
        public DataSet GetGameDetailsById()
        {
            SqlParameter[] para = { new SqlParameter ("@GameId", FK_GameId )};
            DataSet ds = DBHelper.ExecuteQuery("GetGameById",para);
            return ds;
        }
        public DataSet GameStart()
        {
            SqlParameter[] para = { new SqlParameter("@GameId", FK_GameId),
                new SqlParameter("@FK_ColorId", FK_ColorId),
                new SqlParameter("@GameStartDateTime", GameStartDateTime),
                new SqlParameter("@FK_UserId", FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GameStart", para);
            return ds;
        }
    }
}