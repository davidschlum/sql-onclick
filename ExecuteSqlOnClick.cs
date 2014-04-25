using Avectra.netForum.Common;
using Avectra.netForum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OTI.AUGTech2014.Extensions
{
    public class ExecuteSqlOnClick
    {
        public void Initialize(PageClass page, Control control, string sqlScript)
        {
            Button btn = (Button)control;
            //instantiate onClick event args to pass page and sql script to Click event
            SqlOnClickEventArgs eventArgs = new SqlOnClickEventArgs(){
                Page = page,
                SqlScript = sqlScript
            };
            //wire up Click event and use delegate in order to pass custom event args
            btn.Click += delegate(object sender, EventArgs e)
            {
                btn_Click(sender, eventArgs);
            };
            //Make sure the user can't click the button more than once
            UtilityFunctions.DoubleSubmitPrevention(page, btn);
        }

        private void btn_Click(object sender, SqlOnClickEventArgs e)
        {
            //parse any values in the sql script
            string _sqlScript = DataUtils.ParseValues(e.Page.oFacadeObject, e.SqlScript);
            //execute the script that has been parsed
            DataUtils.ExecuteSql(_sqlScript); //any errors get set to Config.LastError
            string message;
            //Check for error and do something for either case
            if (!UtilityFunctions.ER())
            {
                message = "Success";
            }
            else
            {
                message = "Failure";
            }
            Button btn = (Button)sender;
            btn.Text = message;
        }
    }
    public class SqlOnClickEventArgs : EventArgs
    {
        public PageClass Page { get; set; }
        public string SqlScript { get; set; }
    }
}
