GO

GO
ALTER TABLE dbo.tbl_YL_HangQi ADD
	SeoTitle nvarchar(250) NULL,
	SeoKeyword nvarchar(250) NULL,
	SeoDescription nvarchar(250) NULL
GO

/* =============================================
 Author:		汪奇志
 Create date: 2014-03-26
 Description:	YL-航期
 =============================================*/
ALTER VIEW [dbo].[view_YL_HangQi]
AS
SELECT     HangQiId, CompanyId, MingCheng, BianHao, ISNULL
                          ((SELECT     MIN(CASE WHEN J.JiaGe1 <> 0 THEN J.JiaGe1 WHEN J.JiaGe2 <> 0 THEN J.JiaGe2 WHEN J.JiaGe3 <> 0 THEN J.JiaGe3 WHEN J.JiaGe4 <> 0 THEN J.JiaGe4
                                                     END) AS Expr1
                              FROM         dbo.tbl_YL_HangQiJiaGe AS J
                              WHERE     (HangQiId = A.HangQiId) AND (RiQiId IN
                                                        (SELECT     RiQiId
                                                          FROM          dbo.tbl_YL_HangQiRiQi AS R
                                                          WHERE      (HangQiId = A.HangQiId) AND (DATEDIFF(MONTH, RiQi, A.YouXiaoRiQi) = 0) AND (DATEDIFF(DAY, RiQi, GETDATE()) <= 0))) AND 
                                                    (BinKeLeiXingId IN
                                                        (SELECT     XinXiId
                                                          FROM          dbo.tbl_YL_JiChuXinXi
                                                          WHERE      (T1 = 1)))), QiShiJiaGe) AS QiShiJiaGe, QiShiJiaGe AS QiShiJiaGe1, QiShiJiaGeShuoMing, GysId, GongSiId, XiLieId, ChuanZhiId, 
                      HangXianId, ChuFaGangKouId, DiDaGangKouId, ChanPinTeSe, YouHuiXinXi, TianShu1, TianShu2, FeiYongShuoMing, QianZhengQianZhu, YuDingXuZhi, 
                      YouQingTiShi, HangXianXingZhi, KeDiKouJinFen, JiFenDuiHuanBiLi, JiFenLeiJiBiLi, IssueTime, LatestTime, OperatorId, IsDelete, IsYouXiao, YouXiaoRiQiId, 
                      YouXiaoRiQi, LeiXing, TuJingChengShi, FaPiaoKuaiDiJinE, GongLue, PaiXuId,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_GongSi AS A1
                            WHERE      (GongSiId = A.GongSiId)) AS GongSiName,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_XiLie AS A1
                            WHERE      (XiLieId = A.XiLieId)) AS XiLieName,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_ChuanZhi AS A1
                            WHERE      (ChuanZhiId = A.ChuanZhiId)) AS ChuanZhiName,
                          (SELECT     Name
                            FROM          dbo.tbl_Source AS A1
                            WHERE      (SourceId = A.GysId)) AS GysName,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_YL_HangQiDingDan AS A1
                            WHERE      (HangQiId = A.HangQiId)) AS XiaoLiang, XiaoLiang AS XiaoLiang1, HaoPing,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_JiChuXinXi AS A1
                            WHERE      (XinXiId = A.HangXianId)) AS HangXianMingCheng,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_JiChuXinXi AS A1
                            WHERE      (XinXiId = A.ChuFaGangKouId)) AS ChuFaGangKouMingCheng,
                          (SELECT     MingCheng
                            FROM          dbo.tbl_YL_JiChuXinXi AS A1
                            WHERE      (XinXiId = A.DiDaGangKouId)) AS DiDaGangKouMingCheng, ISNULL
                          ((SELECT     RenShu
                              FROM         dbo.tbl_YL_HangQiRiQi AS A1
                              WHERE     (RiQiId = A.YouXiaoRiQiId)), 0) AS YuKongRenShu,
                          (SELECT     ISNULL(SUM(RenShu), 0) AS Expr1
                            FROM          dbo.tbl_YL_HangQiDingDan AS A1
                            WHERE      (IsDelete = '0') AND (HangQiId = A.HangQiId) AND (DingDanStatus IN (0, 1, 2, 4)) AND (IsTuanGou = '0')) AS YouXiaoDingDanRenShu, SeoTitle, 
                      SeoKeyword, SeoDescription
FROM         dbo.tbl_YL_HangQi AS A

GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	单项业务列表视图
-- =============================================
ALTER VIEW [dbo].[view_SingleService]
AS
SELECT A.TourId
	,A.TourStatus
	,B.OrderId 
	,B.OrderCode
	,B.CompanyId
	,B.BuyCompanyName
	,B.BuyCompanyId
	,B.SellerId
	,B.SellerName
	,B.DeptId
	,B.ContactName
	,B.ContactTel
	,B.Adults                
	,B.Operator
	,B.OperatorId
	,B.IssueTime
	,A.LDate
	,A.TourType
	,A.IsDelete
	,(SELECT A1.ServiceType FROM tbl_TourTeamPrice AS A1 WHERE A1.TourId = A.TourId FOR xml raw, root('Root')) AS PlanProject
	,(SELECT A1.Planer FROM tbl_TourPlaner AS A1 WHERE A1.tourId = A.tourid FOR xml raw, root('Root')) AS TourPlaner
	,A.CancelReson
	,B.ContractId AS HeTongId
	,B.ContractCode AS HeTongCode
FROM tbl_Tour AS A INNER JOIN tbl_TourOrder AS B 
ON A.TourId = B.TourId
WHERE A.IsDelete='0' AND A.TourType=6

GO
--以上日志已更新 汪奇志 10 15 2014  3:58PM
GO

-- =============================================
-- Author:		<曹胡生>
-- Create date: <2011-09-14>
-- Description:	<个人业绩统计视图>
-- 修改记录:
-- 1、加条件TourOrder.Status=4 曹胡生 2012-06-1
-- 2.2014-01-02 汪奇志 散拼团队利润=团队结算收入-计调支出
-- 3.增加TourId，TourType,OrderId,BuyCompanyId
-- =============================================
ALTER VIEW [dbo].[view_SellerYeJi]
AS
SELECT dbo.tbl_ComDepartment.DepartName,
       D.SellerName,
       D.CompanyId,
       D.SellerId,
       D.DeptId,
       D.TourCode,
       D.OrderCode,
       D.RouteName,
       D.BuyCompanyName,
       D.LDate,
       D.PeopleNum,
       D.InCome,
       D.Pay,
       D.Operator,
       D.ReviewTime,
       D.OrderNum,
       D.GrossProfit
       ,D.TourId
       ,D.TourType
       ,D.OrderId
       ,D.BuyCompanyId
FROM   dbo.tbl_ComDepartment
       INNER JOIN (
                SELECT dbo.tbl_ComUser.ContactName AS SellerName,
                       dbo.tbl_ComUser.CompanyId,
                       C.SellerId,
                       C.DeptId,
                       C.TourCode,
                       C.OrderCode,
                       C.RouteName,
                       C.BuyCompanyName,
                       C.LDate,
                       C.PeopleNum,
                       C.InCome,
                       C.Pay,
                       C.Operator,
                       C.ReviewTime,
                       C.OrderNum,
                       C.GrossProfit
                       ,C.TourId
                       ,C.TourType
                       ,C.OrderId
                       ,C.BuyCompanyId
                FROM   dbo.tbl_ComUser
                       INNER JOIN (
                                SELECT A.SellerId,
                                       A.DeptId,
                                       B.TourCode,
                                       A.OrderCode,
                                       B.RouteName,
                                       A.BuyCompanyName,
                                       B.LDate,
                                       A.Adults + A.Childs AS PeopleNum,
                                       A.ConfirmMoney AS InCome,
                                       A.ConfirmSettlementMoney AS Pay,
                                       A.Operator,
                                       (
                                           SELECT TOP(1) IssueTime
                                           FROM   dbo.tbl_TourStatusChange
                                           WHERE  (TourId = B.TourId)
                                                  AND (TourStatus = 11)
                                       )  AS ReviewTime,
                                       1  AS OrderNum,
                                       A.ConfirmMoney - A.ConfirmSettlementMoney AS 
                                       GrossProfit
                                       ,B.TourId
                                       ,B.TourType
                                       ,A.OrderId
                                       ,A.BuyCompanyId
                                FROM   dbo.tbl_TourOrder AS A
                                       LEFT OUTER JOIN dbo.tbl_Tour AS B
                                            ON  A.TourId = B.TourId
                                WHERE  (A.IsDelete = '0')
                                       AND (A.[Status] = 4)
                                       AND (A.ConfirmMoneyStatus = '1')
                                       AND (B.TourStatus = 11)
                                       AND (A.TourType IN (1, 3, 5))
                                UNION ALL
                                SELECT SellerId,
                                       DeptId,
                                       TourCode,
                                       '' AS OrderCode,
                                       RouteName,
                                       '' AS BuyCompanyName,
                                       LDate,
                                       0  AS PeopleNum,
                                       --TourIncome + TourOtherIncome AS InCome,
                                       TourSettlement AS InCome,
                                       TourPay AS Pay,
                                       '' AS Operator,
                                       (
                                           SELECT TOP(1) IssueTime
                                           FROM   dbo.tbl_TourStatusChange AS 
                                                  tbl_TourStatusChange_2
                                           WHERE  (TourId = dbo.tbl_Tour.TourId)
                                                  AND (TourStatus = 11)
                                       )  AS ReviewTime,
                                       0  AS OrderNum,
                                       --TourIncome + TourOtherIncome - TourPay AS GrossProfit
                                       TourSettlement - TourPay AS GrossProfit
                                       ,TourId
                                       ,TourType
                                       ,'' AS OrderId
                                       ,'' AS BuyCompanyId
                                FROM   dbo.tbl_Tour
                                WHERE  (IsDelete = '0')
                                       AND (TourStatus = 11)
                                       AND (TourType IN (1, 3, 5))
                                UNION ALL
                                SELECT A.SellerId,
                                       A.DeptId,
                                       A.TourCode,
                                       B.OrderCode,
                                       A.RouteName,
                                       B.BuyCompanyName,
                                       A.LDate,
                                       B.Adults + B.Childs AS PeopleNum,
                                       B.ConfirmMoney + A.TourOtherIncome AS 
                                       InCome,
                                       A.TourPay AS Pay,
                                       B.Operator,
                                       (
                                           SELECT TOP(1) IssueTime
                                           FROM   dbo.tbl_TourStatusChange AS 
                                                  tbl_TourStatusChange_1
                                           WHERE  (TourId = A.TourId)
                                                  AND (TourStatus = 11)
                                       )  AS ReviewTime,
                                       1  AS OrderNum,
                                       B.ConfirmMoney + A.TourOtherIncome - A.TourPay AS 
                                       GrossProfit
                                       ,A.TourId
                                       ,A.TourType
                                       ,B.OrderId
                                       ,B.BuycompanyId
                                FROM   dbo.tbl_Tour AS A
                                       LEFT OUTER JOIN dbo.tbl_TourOrder AS B
                                            ON  A.TourId = B.TourId
                                WHERE  (A.IsDelete = '0')
                                       AND (B.[Status] = 4)
                                       AND (B.IsDelete = '0')
                                       AND (A.TourStatus = 11)
                                       AND (A.TourType IN (0, 2, 4, 6))
                            ) AS C
                            ON  dbo.tbl_ComUser.UserId = C.SellerId
            ) AS D
            ON  dbo.tbl_ComDepartment.DepartId = D.DeptId



GO
--以上日志已更新 汪奇志 01 23 2015  9:35AM
GO
