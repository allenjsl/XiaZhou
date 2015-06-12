using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Web.Ashx
{
    //获取供应商酒店信息
    //创建人：lixh 创建时间:2012-03-20
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetSupplierContect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string suppId = EyouSoft.Common.Utils.GetQueryStringValue("suppId");
            string type = EyouSoft.Common.Utils.GetQueryStringValue("type");
            string source = EyouSoft.Common.Utils.GetQueryStringValue("source");
            if (!string.IsNullOrEmpty(suppId) && !string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "1":  //酒店房型 星级
                        if (!string.IsNullOrEmpty(source))
                        {
                            if (source == "1")
                            {
                                EyouSoft.Model.SourceStructure.MSourceHotel HotelModel = new EyouSoft.BLL.SourceStructure.BSource().GetOneHotelModel(suppId);
                                if (HotelModel != null)
                                {
                                    if (HotelModel.Star != null)
                                    {
                                        string star = ((int)HotelModel.Star).ToString();
                                        returnMsg(Newtonsoft.Json.JsonConvert.SerializeObject(HotelModel.HotelRoomList), star);
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(EyouSoft.Common.Utils.GetQueryStringValue("company")))
                                {
                                    //预控房型
                                    EyouSoft.Model.SourceStructure.MSourceSueHotel hotelSue = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByHotelId(suppId, EyouSoft.Common.Utils.GetQueryStringValue("company"));
                                    if (hotelSue != null)
                                    {
                                        string roomTypelist = string.Empty;
                                        roomTypelist = "[";
                                        roomTypelist += "{\"RoomId\":\"" + hotelSue.RoomId + "\",\"userNum\":\"" + (hotelSue.ControlNum - hotelSue.AlreadyNum) + "\",\"TypeName\":\"" + hotelSue.RoomType + "\",\"PriceSK\":\"" + EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(hotelSue.UnitPrice) + "\"},";
                                        roomTypelist = roomTypelist.TrimEnd(',');
                                        roomTypelist += "]";


                                        //酒店星级                                        
                                        EyouSoft.Model.SourceStructure.MSourceHotel HotelModel = new EyouSoft.BLL.SourceStructure.BSource().GetOneHotelModel(hotelSue.SourceId);
                                        if (HotelModel != null)
                                        {
                                            if (HotelModel.Star != null)
                                            {
                                                string star = ((int)HotelModel.Star).ToString();
                                                returnMsg(roomTypelist, star);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "3":
                        if(source=="1")
                        {
                            EyouSoft.Model.SourceStructure.MSourceSpot spotmodel = new EyouSoft.BLL.SourceStructure.BSource().GetSpotModel(suppId);
                            if (spotmodel != null)
                            {

                                if (spotmodel.PriceSystemList != null && spotmodel.PriceSystemList.Count > 0)
                                {
                                    returnMsg(Newtonsoft.Json.JsonConvert.SerializeObject(spotmodel.PriceSystemList), "");
                                }
                                else
                                {
                                    returnMsg("[]", "");
                                }

                            }
                        }
                        else
                        {
                            //预控景点
                            var yk = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelBySightId(suppId, EyouSoft.Common.Utils.GetQueryStringValue("company"));
                            if (yk!=null)
                            {
                                returnMsg("[{\"Id\":\"" + yk.SpotId + "\",\"userNum\":\"" + (yk.ControlNum - yk.AlreadyNum) + "\",\"SpotName\":\"" + yk.SpotName + "\",\"PriceSK\":\"" + EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(yk.UnitPrice) + "\"}]", "");
                            }
                        }
                        break;
                    case "13":
                        if (source == "1")
                        {
                            var m = new EyouSoft.BLL.SourceStructure.BSource().GetOtherModel(suppId);
                            if (m != null)
                            {

                                if (m.OtherTypeList != null && m.OtherTypeList.Count > 0)
                                {
                                    returnMsg(Newtonsoft.Json.JsonConvert.SerializeObject(m.OtherTypeList), "");
                                }
                                else
                                {
                                    returnMsg("[]", "");
                                }

                            }
                        }
                        else
                        {
                            //预控其他
                            var yk = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByOtherId(suppId, EyouSoft.Common.Utils.GetQueryStringValue("company"));
                            if (yk != null)
                            {
                                returnMsg("[{\"OtherId\":\"" + yk.TypeId + "\",\"userNum\":\"" + (yk.ControlNum - yk.AlreadyNum) + "\",\"Name\":\"" + yk.TypeName + "\",\"Price\":\"" + EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(yk.UnitPrice) + "\"}]", "");
                            }
                        }
                        break;
                    case "14":
                        if (source == "1")
                        {
                            EyouSoft.Model.SourceStructure.MSourceShip shipModel = new EyouSoft.BLL.SourceStructure.BSource().GetShipModel(suppId);
                            if (shipModel != null)
                            {
                                string shipNamelist = string.Empty;
                                shipNamelist = "[";
                                if (shipModel.SubShipList != null && shipModel.SubShipList.Count > 0)
                                {
                                    for (int i = 0; i < shipModel.SubShipList.Count; i++)
                                    {
                                        shipNamelist += "{\"id\":\"" + shipModel.SubShipList[i].SubId + "\",\"text\":\"" + shipModel.SubShipList[i].ShipName + "\"},";
                                    }
                                    shipNamelist = shipNamelist.TrimEnd(',');
                                }
                                shipNamelist += "]";
                                returnMsg(shipNamelist, "");
                            }
                        }
                        else
                        {
                            string companyID = EyouSoft.Common.Utils.GetQueryStringValue("company");
                            if (!string.IsNullOrEmpty(companyID))
                            {
                                string shipNamelist = string.Empty;
                                shipNamelist += "[";
                                EyouSoft.Model.SourceStructure.MSourceSueShip shipName = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByShipId(suppId, companyID);
                                if (shipName != null)
                                {
                                    shipNamelist += "{\"id\":\"" + shipName.SubId + "\",\"text\":\"" + shipName.ShipName + "\"},";
                                    shipNamelist = shipNamelist.TrimEnd(',');
                                }
                                shipNamelist += "]";
                                returnMsg(shipNamelist, "");
                            }
                        }
                        break;
                    case "4":
                        if (source == "1")
                        {
                            EyouSoft.Model.SourceStructure.MSourceShip shipModel = new EyouSoft.BLL.SourceStructure.BSource().GetShipModel(suppId);
                            if (shipModel != null)
                            {
                                string shipNamelist = string.Empty;
                                shipNamelist = "[";
                                if (shipModel.SubShipList != null && shipModel.SubShipList.Count > 0)
                                {
                                    for (int i = 0; i < shipModel.SubShipList.Count; i++)
                                    {
                                        shipNamelist += "{\"id\":\"" + shipModel.SubShipList[i].SubId + "\",\"text\":\"" + shipModel.SubShipList[i].ShipName + "\"},";
                                    }
                                    shipNamelist = shipNamelist.TrimEnd(',');
                                }
                                shipNamelist += "]";
                                returnMsg(shipNamelist, "");
                            }
                        }
                        else
                        {
                            string companyID = EyouSoft.Common.Utils.GetQueryStringValue("company");
                            if (!string.IsNullOrEmpty(companyID))
                            {
                                string shipNamelist = string.Empty;
                                shipNamelist += "[";
                                EyouSoft.Model.SourceStructure.MSourceSueShip shipName = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByShipId(suppId, companyID);
                                if (shipName != null)
                                {
                                    shipNamelist += "{\"id\":\"" + shipName.SubId + "\",\"text\":\"" + shipName.ShipName + "\"},";
                                    shipNamelist = shipNamelist.TrimEnd(',');
                                }
                                shipNamelist += "]";
                                returnMsg(shipNamelist, "");
                            }
                        }
                        break;
                    case "2":
                        if (source == "1")
                        {
                            EyouSoft.Model.SourceStructure.MSourceMotorcade carModel = new EyouSoft.BLL.SourceStructure.BSource().GetMotorcadeModel(suppId);
                            if (carModel != null)
                            {
                                string CarModelList = string.Empty;
                                CarModelList += "[";
                                if (carModel.CarList != null && carModel.CarList.Count > 0)
                                {
                                    for (int i = 0; i < carModel.CarList.Count; i++)
                                    {
                                        CarModelList += "{\"id\":\"" + carModel.CarList[i].CarId + "\",\"text\":\"" + carModel.CarList[i].TypeName + "\",\"CarCode\":\"" + carModel.CarList[i].CarNumber + "\",\"driver\":\"" + carModel.CarList[i].Driver + "\",\"driverTel\":\"" + carModel.CarList[i].DriverTel + "\"},";
                                    }
                                    CarModelList = CarModelList.TrimEnd(',');
                                }
                                CarModelList += "]";
                                returnMsg(CarModelList, "");
                            }
                        }
                        else
                        {
                            string companyID = EyouSoft.Common.Utils.GetQueryStringValue("company");
                            if (!string.IsNullOrEmpty(companyID))
                            {
                                string CarModelList = string.Empty;
                                CarModelList += "[";
                                EyouSoft.Model.SourceStructure.MSourceSueCar CarModel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByCarId(suppId, companyID);
                                EyouSoft.Model.SourceStructure.MSourceMotorcade carModel = new EyouSoft.BLL.SourceStructure.BSource().GetMotorcadeModel(CarModel.SourceId);
                                if (CarModelList != null && carModel != null)
                                {
                                    if (carModel.CarList != null && carModel.CarList.Count > 0)
                                    {
                                        CarModelList += "{\"id\":\"" + CarModel.CarId + "\",\"text\":\"" + CarModel.TypeName + "\",\"CarCode\":\"" + carModel.CarList.First().CarNumber + "\",\"driver\":\"" + carModel.CarList.First().Driver + "\",\"driverTel\":\"" + carModel.CarList.First().DriverTel + "\"},";
                                    }
                                    else
                                    {
                                        CarModelList += "{\"id\":\"" + CarModel.CarId + "\",\"text\":\"" + CarModel.TypeName + "\"},";
                                    }
                                }
                                else
                                {
                                    CarModelList += "{\"id\":\"" + CarModel.CarId + "\",\"text\":\"" + CarModel.TypeName + "\"},";
                                }
                                CarModelList = CarModelList.TrimEnd(',');
                                CarModelList += "]";
                                returnMsg(CarModelList, "");
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        public void returnMsg(string typelist, string Star)
        {
            string msg = "{\"tolist\":" + typelist + ",\"star\":\"" + Star + "\"}";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(msg);
            HttpContext.Current.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
