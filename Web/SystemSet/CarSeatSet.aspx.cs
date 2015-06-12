using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 座位设置
    /// 创建人：赵晓慧
    /// 创建时间：2012-08-20
    /// </summary>
    public partial class CarSeatSet : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
            
        }
        private void PageInit()
        {
            string seatnum =Utils.GetQueryStringValue("seatnum");
            int id = 0;
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> seatnumlist = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeSeatNumList();
            if (seatnumlist != null && seatnumlist.Count > 0)
            {
                foreach (var item in seatnumlist)
                {
                    if (item.SeatNum.ToString() == seatnum)
                    {
                        id = item.Id;
                    }
                }
            }
            
            IList<EyouSoft.Model.SysStructure.MSysCarType> list = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeList(id);

            this.hideJsonData.Value = ("{\"data\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}");
        }
    }
}
