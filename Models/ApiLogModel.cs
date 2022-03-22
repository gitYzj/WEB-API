using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WEB_API.Tools;

namespace WEB_API.Models
{
    /// <summary>
    /// 日志数据库版本
    /// </summary> 
    public class ApiLogModel
    {
        private static string GetApiDbConnection = ConfigurationManager.ConnectionStrings["ApiSystem"].ConnectionString;
        public ErrorType cType { get; set; }
        public string cUserName { get; set; }
        public string cReturnData { get; set; }
        public string cInputData { get; set; }
        public string cMesg { get; set; }
        public Type cClassType { get; set; }
        public DateTime dDate
        {
            get { return DateTime.Now; }
        }

        public ApiLogModel(Type cClassType, ErrorType cType, string cUserName, string cReturnData, string cInputData, string cMesg)
        {
            this.cType = cType;
            this.cUserName = cUserName;
            this.cReturnData = cReturnData;
            this.cInputData = cInputData;
            this.cMesg = cMesg;
        }

        public async void SQLLog()
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(GetApiDbConnection))
                {
                    conn.Execute("insert into (cClassType,cType,cUserName,cReturnData,cInputData,cMesg,dDate) values(@cClassType,@cType,@cUserName,@cReturnData,@cInputData,@cMesg,@dDate);", this);
                }
            }
            catch (Exception e)
            {
                Log4Helper.Info(cClassType, $"{DateTime.Now.ToString("O")}: cUserName{cUserName},cReturnData {cReturnData}, cInputData{cInputData}, cMesg{cMesg}");
                Log4Helper.Error(this.GetType(), $"{DateTime.Now.ToString("O")}: 日志写入错误 {e.ToString()} ");
            }
        }
    }

    public enum ErrorType
    {
        日志 = 1,
        警告 = 2,
        异常 = 3, 
    }
}