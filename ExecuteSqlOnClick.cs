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
            SqlOnClickEventArgs eventArgs = new SqlOnClickEventArgs(){
                Page = page,
                SqlScript = sqlScript
            };

            btn.Click += delegate(object sender, EventArgs e)
            {
                btn_Click(sender, eventArgs);
            };
            UtilityFunctions.DoubleSubmitPrevention(page, btn);
        }

        private void btn_Click(object sender, SqlOnClickEventArgs e)
        {
            string _sqlScript = DataUtils.ParseValues(e.Page.oFacadeObject, e.SqlScript);
            DataUtils.ExecuteSql(_sqlScript);
            string message;
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
