--Should be functional could not effectively test due
--to invalid data

DECLARE @BeginDate 	datetime
DECLARE @EndDate 	datetime

SET @BeginDate 	= CAST('1/1/2005' AS datetime)
SET @EndDate 	= CAST('12/30/2005' AS datetime)

BEGIN
	--dange ol get that data man
	SELECT  DISTINCT
		tblContractDomestic.ContractDomesticID,
		tblContractDomestic.RiverRouteID,
		tblContractDomestic.FromTruckLoadOrderNbr,
		tblContractDomestic.ToTruckLoadOrderNbr,
		tblContractDomestic.isGuarantee,
		tblContractDomestic.isNoProbe,
		tblContractDomestic.ContractID,
		tblContractDomestic.DemurrageScheduleID,
		tblContractDomestic.LeasedCarPremiumDiscountAmt,
		tblContractDomestic.LeasedCarPremiumDiscountAmtUOMID,
		tblContractDomestic.SecondaryCustomer,
		tblContractDomestic.IsPOS,
		tblContractDomestic.NumberLoads,
		tblContractDomestic.IsSacks,
		tblContractDomestic.LoadOrderNbr,
		tblContractDomestic.IsValidPOS,
		tblCommodityGroup.CommodityGroupID, 
		tblCommodity.CommodityID, 
		tblLoadOrderSack.SackCount
	FROM
		tblContract
	INNER JOIN
		tblContractDomestic
	ON
		tblContract.ContractID = tblContractDomestic.ContractID
	INNER JOIN
		tblContractShippingPeriod
	ON
		tblContract.ContractID = tblContractShippingPeriod.ContractID
	INNER JOIN
		tblCommodity
	ON
		tblContract.CommodityID = tblCommodity.CommodityID
	INNER JOIN 
		tblCommodityGroup
	ON
		tblCommodity.CommodityGroupID = tblCommodityGroup.CommodityGroupID
	LEFT JOIN
		tblLoadOrder
	ON
		tblContractDomestic.LoadOrderNbr = tblLoadOrder.LoadOrderNbr
	LEFT JOIN
		tblLoadOrderSack
	ON
		tblLoadOrder.LoadOrderID = tblLoadOrderSack.LoadOrderID
	WHERE
		tblContractDomestic.IsPOS = 1
	AND
		tblContractDomestic.IsSacks = 1
	AND
		(NULL != tblLoadOrderSack.SackCount AND tblLoadOrderSack.SackCount > 0)
	AND
		tblLoadOrderSack.IsReady = 0
	AND
	(
		(tblContractShippingPeriod.ShippingPeriodStartDate BETWEEN @BeginDate AND @EndDate)
			OR
		(tblContractShippingPeriod.ShippingPeriodEndDate BETWEEN @BeginDate AND @EndDate)
	)
	ORDER BY tblCommodityGroup.CommodityGroupID, tblCommodity.CommodityID, tblLoadOrderSack.SackCount

END