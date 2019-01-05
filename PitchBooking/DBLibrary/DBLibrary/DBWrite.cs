using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class DBWrite
    {
        PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");
                
        public bool CreateLogEntry(string category, string description, int userID, string userName)
        {
            bool logsavevalidated = true;    
            try
               {
                string comment = $"{description} user credentials  = {userName}";
                Log log = new Log();
                log.UserID = userID;
                log.Category = category;
                log.Description = comment;
                log.Date = DateTime.Now;
                SaveLog(log);
               }
            catch
               {
                logsavevalidated = false;
               }
            return logsavevalidated;
        }

        public bool SaveLog(Log log)
        {
            bool savevalidated = true;
            try
                {
                db.Entry(log).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                int saveSucess = db.SaveChanges();
                }
            catch
                {
                    savevalidated = false;
                }
            return savevalidated;
        } 

    }
}
