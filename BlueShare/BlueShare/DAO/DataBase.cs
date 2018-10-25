using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace BlueShare.DAO
{
    public class DataBase : IDisposable
    {
        protected SQLiteConnection Connection { get; }

        public DataBase()
        {
            //Connection = new SQLiteConnection(DependencyService.Get<ICaminho>().ObterCaminho("database.sqlite"));            
        }                
        
        public virtual void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}