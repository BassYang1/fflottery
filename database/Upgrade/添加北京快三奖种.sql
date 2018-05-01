USE Ticket;
GO

--1, 添加彩种
DECLARE @sort INT, 
		@msort INT, 
		@ltid INT = 5007,
		@lttype INT = 5,
		@iss INT = 78

SELECT @sort = MAX(Sort), @msort = MAX(IphoneSort) FROM Sys_Lottery;

--新增彩种: Ltype 彩种类别, IndexType 显示顺序
INSERT INTO Sys_Lottery(Id, Title, Code, MinTimes, MaxTimes, IsOpen, CloseTime, second, Sort, Ltype, IsAuto, IndexType, Url, AutoUrl, 
	IphoneIsOpen, IphoneSort, IphoneRemark, IphoneImg, IssNum)
SELECT @ltid, N'北京快三', 'bjk3', 1, 99, 0, 0, 0, @sort, @lttype, 0, 7, N'', 0, 
	0, @msort, N'当日9点30分至当日22点40分', 89, @iss
WHERE NOT EXISTS(SELECT 1 FROM Sys_Lottery WHERE Code='bjk3');

DECLARE @num INT = 1, 
		@sn NVARCHAR(100) = '001', 
		@time NVARCHAR(10) = '09:10:00'

--2, 添加彩种时间
WHILE @num <= @iss
BEGIN	
	INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
	SELECT @ltid, @sn, @time, 0, GETDATE()
	WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=@ltid AND Sn=@sn)

	SET @num = @num + 1;
	SET @sn = '00000' + CAST(@num AS NVARCHAR(10))
	SET @sn = SUBSTRING(@sn, len(@sn) - 2, 3)
	SET @time = CONVERT(NVARCHAR(8), DATEADD(MI, 10, CONVERT(DATETIME,@time,103)), 108)
END
