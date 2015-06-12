GO
--��������ǰ����������ݱ��� 
--������Ҫѡ����Ҫ���������
--2013-02-22 ����־
GO
SET XACT_ABORT ON
BEGIN TRAN

DECLARE @CID CHAR(36)
SET @CID=''

--demov1.0��30248301-14e5-4eee-a3f1-fe531fe4088e
--huatian��8460b43c-cacf-49e8-96ff-dceb9d701384
--huanqiu��def66589-9e05-41b1-a3d7-2d42efeef76a

--δ֪����
DELETE FROM tbl_ComAttach WHERE ItemId=''
DELETE FROM tbl_ComAttach WHERE FilePath=''


--�ͻ����������Ϣ
DELETE FROM tbl_ComAttach WHERE ItemType=15 AND ItemId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_CrmBank WHERE CrmId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_CrmJiFen WHERE CrmId IN(SELECT B.CrmId FROM tbl_CrmMember AS B WHERE B.CrmId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_CrmMember WHERE CrmId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComUser WHERE UserId IN(SELECT B.UserId FROM tbl_CrmLinkman AS B WHERE B.[Type]=0 AND B.TypeId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID)) AND CompanyId=@CID
DELETE FROM tbl_CrmLinkman WHERE [Type]=0 AND TypeId IN(SELECT A.CrmId FROM tbl_Crm AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Crm WHERE CompanyId=@CID



--��·�����Ϣ
DELETE FROM tbl_ComAttach WHERE ItemType=1 AND ItemId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=11 AND ItemId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=12 AND ItemId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=13 AND ItemId IN(SELECT B.PlanId FROM tbl_RoutePlan AS B WHERE B.RouteId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_RoutePlanSpot WHERE PlanId IN(SELECT B.PlanId FROM tbl_RoutePlan AS B WHERE B.RouteId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_RoutePlan WHERE RouteId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_RouteServices WHERE RouteId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_RouteStandard WHERE RouteId IN(SELECT A.RouteId FROM tbl_Route AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Route WHERE CompanyId=@CID



--��Ӧ�������Ϣ
DELETE FROM tbl_ComAttach WHERE ItemType=0 AND ItemId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=9 AND ItemId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=10 AND ItemId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=14 AND ItemId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TravelRouteArea WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceTravel WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceDiningCuisine WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceDining WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceCar WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceShop WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceShip WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSubShip WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceOther WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSpotPriceSystem WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceHotelRoom WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceHotel WHERE SourceId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComUser WHERE UserId IN(SELECT B.UserId FROM tbl_CrmLinkman AS B WHERE B.[Type]=1 AND B.TypeId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)) AND CompanyId=@CID
DELETE FROM tbl_CrmLinkman WHERE [Type]=1 AND TypeId IN(SELECT A.SourceId FROM tbl_Source AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Source WHERE CompanyId=@CID



--���������Ϣ
DELETE FROM tbl_SourceGuideCategory WHERE GuideId IN(SELECT A.GuideId FROM tbl_SourceGuide AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceGuide WHERE CompanyId=@CID


--���������Ϣ
DELETE FROM tbl_ComAttach WHERE ItemType=17 AND ItemId IN(SELECT A.QuoteId FROM tbl_Quote AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_QuotePrice WHERE QuoteId IN(SELECT A.QuoteId FROM tbl_Quote AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_QuotePlanSpot WHERE PlanId IN(SELECT B.PlanId FROM tbl_QuotePlan AS B WHERE B.QuoteId IN(SELECT A.QuoteId FROM tbl_Quote AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_QuotePlan WHERE QuoteId IN(SELECT A.QuoteId FROM tbl_Quote AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Quote WHERE CompanyId=@CID

--�ƻ������Ϣ
DELETE FROM tbl_ComAttach WHERE ItemType=18 AND ItemId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ComAttach WHERE ItemType=8 AND ItemId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourPlanSpot WHERE PlanId IN(SELECT B.PlanId FROM tbl_TourPlan AS B WHERE B.TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_TourPlan WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourSupplyPrice WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourPlaner WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourPlanItem WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourSanPinPrice WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourTeamPrice WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourService WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourPlanStatus WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourPlanChange WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourStatusChange WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourOriginalInfo WHERE TourId IN(SELECT A.TourId FROM tbl_Tour AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Tour WHERE CompanyId=@CID
DELETE FROM tbl_ComTourKey WHERE CompanyId=@CID

--���������Ϣ

--���������Ϣ
DELETE FROM tbl_TourOrderTravellerInsurance WHERE TravellerId IN(SELECT B.TravellerId FROM tbl_TourOrderTraveller AS B WHERE B.OrderId IN(SELECT A.OrderId FROM tbl_TourOrder AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_TourOrderTraveller WHERE OrderId IN(SELECT A.OrderId FROM tbl_TourOrder AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourOrderSales WHERE OrderId IN(SELECT A.OrderId FROM tbl_TourOrder AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourOrderChange WHERE OrderId IN(SELECT A.OrderId FROM tbl_TourOrder AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_TourOrder WHERE CompanyId=@CID

--�Ƶ����������Ϣ
DELETE FROM tbl_PlanCostChange WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanGuide WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanHotelRoom WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanHotel WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanAttractions WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanCar WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_FinRegister WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanShipPrice WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanShipOwnCost WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanShip WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanDiningPrice WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_PlanLargeFrequency WHERE PlanId IN(SELECT A.PlanId FROM tbl_Plan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_Plan WHERE CompanyId=@CID

--������������Ϣ
DELETE FROM tbl_FinDebit WHERE CompanyId=@CID
DELETE FROM tbl_FinOtherInFee WHERE CompanyId=@CID
DELETE FROM tbl_FinOtherOutFee WHERE CompanyId=@CID
DELETE FROM tbl_FinDisburseApplyOverrun WHERE DisburseId IN(SELECT A.DisburseId FROM tbl_FinDisburseApply AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_FinDisburseApply WHERE CompanyId=@CID
DELETE FROM tbl_FinAssets WHERE CompanyId=@CID
DELETE FROM tbl_FinProfitDistribute WHERE CompanyId=@CID
DELETE FROM tbl_FinKingDeeProofDetail WHERE FId IN(SELECT A.FId FROM tbl_FinKingDeeProof AS A WHERE A.FCompanyId=@CID)
DELETE FROM tbl_FinKingDeeProof WHERE FCompanyId=@CID
DELETE FROM tbl_FinBill WHERE CompanyId=@CID
DELETE FROM tbl_FinKingDeeSubject WHERE CompanyId=@CID
DELETE FROM tbl_FinKingDeeChk WHERE CompanyId=@CID
DELETE FROM tbl_FinDaiShou WHERE CompanyId=@CID

--���������Ϣ
--֪ͨ����
DELETE FROM tbl_ComAttach WHERE ItemType=2 AND ItemId IN(SELECT A.NoticeId FROM tbl_GovNotice AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovNoticeReceiver WHERE NoticeId IN(SELECT A.NoticeId FROM tbl_GovNotice AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovNoticeBrowse WHERE NoticeId IN(SELECT A.NoticeId FROM tbl_GovNotice AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovNotice WHERE CompanyId=@CID
--����
DELETE FROM tbl_GovMeetingStaff WHERE MeetingId IN(SELECT A.MeetingId FROM tbl_GovMeeting AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovMeeting WHERE CompanyId=@CID
--����
DELETE FROM tbl_GovAttendanceOvertime WHERE AttendanceId IN(SELECT A.AttendanceId FROM tbl_GovAttendance AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovAttendance WHERE CompanyId=@CID
--�����ƶ�
DELETE FROM tbl_ComAttach WHERE ItemType=2 AND ItemId IN(SELECT A.RegId FROM tbl_GovRegulation AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovRegApplyDept WHERE RegId IN(SELECT A.RegId FROM tbl_GovRegulation AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovRegulation WHERE CompanyId=@CID
--�������
DELETE FROM tbl_ComAttach WHERE ItemType=5 AND ItemId IN(SELECT A.OpinionId FROM tbl_GovOpinion AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovOpinionUser WHERE OpinionId IN(SELECT A.OpinionId FROM tbl_GovOpinion AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovOpinion WHERE CompanyId=@CID
--��ְ
DELETE FROM tbl_GovPersonnelApprove WHERE Id IN(SELECT B.Id FROM tbl_GovFilePersonnel AS B WHERE B.FileId IN(SELECT A.Id FROM tbl_GovFile AS A WHERE A.CompanyId=@CID))
DELETE FROM tbl_GovFilePersonnel WHERE FileId IN(SELECT A.Id FROM tbl_GovFile AS A WHERE A.CompanyId=@CID)
--��˾��ͬ
DELETE FROM tbl_GovContract WHERE CompanyId=@CID
--��ѵ����
DELETE FROM tbl_ComAttach WHERE ItemType=7 AND ItemId IN(SELECT A.TrainId FROM tbl_GovTrain AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovTrain WHERE CompanyId=@CID
--��Ʒ����
DELETE FROM tbl_GovGoodUse WHERE GoodId IN(SELECT A.GoodId FROM tbl_GovGood AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovGood WHERE CompanyId=@CID
--�ļ�����
DELETE FROM tbl_ComAttach WHERE ItemType=16 AND ItemId IN(SELECT A.DocumentsId FROM tbl_GovDocuments AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovDocumentsApprove WHERE DocumentsId IN(SELECT A.DocumentsId FROM tbl_GovDocuments AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_GovDocuments WHERE CompanyId=@CID
--���µ���
DELETE FROM tbl_GovFileEducation WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFilePosition WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFileCurriculum WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFileDept WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFilehome WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFileContract WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovPersonnelApprove WHERE Id IN(SELECT Id FROM tbl_GovFilePersonnel WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID))
DELETE FROM tbl_GovFilePersonnel WHERE FileId IN(SELECT ID FROM tbl_GovFile WHERE CompanyId=@CID)
DELETE FROM tbl_GovFile WHERE CompanyId=@CID
--���������Ϣ

--�������������Ϣ
DELETE FROM tbl_IndWorkReportCheck WHERE WorkId IN(SELECT A.Id FROM tbl_IndWorkReport AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_IndWorkReport WHERE CompanyId=@CID
DELETE FROM tbl_IndFileManage WHERE CompanyId=@CID
DELETE FROM tbl_IndMemo WHERE CompanyId=@CID
DELETE FROM tbl_IndWorkPlanCC WHERE WorkPlanId IN(SELECT A.WorkPlanId FROM tbl_IndWorkPlan AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_IndWorkPlan WHERE CompanyId=@CID
DELETE FROM tbl_IndWorkPubScope WHERE PubId IN(SELECT A.Id FROM tbl_IndWorkPub AS A WHErE A.CompanyId=@CID)
DELETE FROM tbl_IndWorkPubRep WHERE PubId IN(SELECT A.Id FROM tbl_IndWorkPub AS A WHErE A.CompanyId=@CID)
DELETE FROM tbl_IndWorkPub WHErE CompanyId=@CID

--��ͬ���������Ϣ
DELETE FROM tbl_ContractNumCollar WHERE ContractId IN(SELECT A.ContractId FROM tbl_ContractNum AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_ContractNum WHERE CompanyId=@CID

--��ԴԤ�������Ϣ
DELETE FROM tbl_SourceSueHotelChange WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueCar AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueHotelChange WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueHotel AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueHotelChange WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueShip AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueTour WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueCar AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueTour WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueHotel AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueTour WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueShip AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueOperator WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueCar AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueOperator WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueHotel AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueOperator WHERE SueId IN(SELECT A.Id FROM tbl_SourceSueShip AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_SourceSueCar WHERE CompanyId=@CID
DELETE FROM tbl_SourceSueHotel WHERE CompanyId=@CID
DELETE FROM tbl_SourceSueShip WHERE CompanyId=@CID

--�������������Ϣ
DELETE FROM tbl_CrmComplaint WHERE CompanyId=@CID
DELETE FROM tbl_CrmVisitDetail WHERE VisitId IN(SELECT A.VisitId FROM tbl_CrmVisit AS A WHERE A.CompanyId=@CID)
DELETE FROM tbl_CrmVisit WHERE CompanyId=@CID

--��־�����Ϣ
DELETE FROM tbl_SysLogLogin WHERE CompanyId=@CID
DELETE FROM tbl_SysLogHandle WHERE CompanyId=@CID
--�쳣��־
DELETE FROM tbl_SysLogException

--��˾����
DELETE FROM tbl_ComDepartment WHERE CompanyId=@CID
--�ͻ��ȼ�
DELETE FROM tbl_ComLev WHERE CompanyId=@CID
--��˾����
DELETE FROM tbl_ComSetting WHERE CompanyId=@CID
--��·����
DELETE FROM tbl_ComAreaPlaner WHERE AreaId IN(SELECT AreaId FROM tbl_ComArea WHERE CompanyId=@CID)
DELETE FROM tbl_ComArea WHERE CompanyId=@CID
--��˾��ɫ
DELETE FROM tbl_ComRole WHERE CompanyId=@CID
--��˾֧����ʽ
DELETE FROM tbl_ComPayment WHERE CompanyId=@CID
--�ź�����
DELETE FROM tbl_TourNoOptionCode WHERE CompanyId=@CID
--����ģ��
DELETE FROM tbl_ComProject WHERE CompanyId=@CID
--���۵ȼ�
DELETE FROM tbl_ComStand WHERE CompanyId=@CID
--��˾�����˺�
DELETE FROM tbl_ComAccount  WHERE CompanyId=@CID
--�û���Ϣ
DELETE FROM tbl_ComUser WHERE CompanyId=@CID
--ϵͳ����
DELETE FROM tbl_SysDomain WHERE SysId IN(SELECT SysId FROM tbl_Sys WHERE SysId IN(SELECT SysId FROM tbl_Company WHERE Id=@CID))
--ϵͳ�˵� 
DELETE FROM tbl_SysMenu WHERE SysId IN(SELECT SysId FROM tbl_Sys WHERE SysId IN(SELECT SysId FROM tbl_Company WHERE Id=@CID))


--ϵͳ��Ϣ
DELETE FROM tbl_Sys WHERE SysId IN(SELECT SysId FROM tbl_Company WHERE Id=@CID)
--��˾��Ϣ
DELETE FROM tbl_Company WHERE Id=@CID

--webmaster
DELETE FROM tbl_SysWebmaster

--��������
DELETE FROM tbl_SmsBankCharge
DELETE FROM tbl_SmsUnitPrice
DELETE FROM tbl_SmsAccount
DELETE FROM tbl_SmsPlanLose114
DELETE FROM tbl_SmsPlan114
DELETE FROM tbl_SmsPlan

DROP TABLE tbl_SmsBankCharge
DROP TABLE tbl_SmsUnitPrice
DROP TABLE tbl_SmsAccount
DROP TABLE tbl_SmsPlanLose114
DROP TABLE tbl_SmsPlan114
DROP TABLE tbl_SmsPlan

DROP PROC proc_Sys_Create

COMMIT TRAN
SET XACT_ABORT OFF
GO
