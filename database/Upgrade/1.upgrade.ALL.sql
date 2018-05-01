USE Ticket;
GO

IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('N_Merchant'))
BEGIN
CREATE TABLE N_Merchant
(
	Id INT IDENTITY Primary Key, --Id
	MerchantId NVARCHAR(50) NOT NULL, --�̻���
	Name NVARCHAR(50) NOT NULL, --�̻�����
	Code NVARCHAR(50), --�̻���ȫ��
	[DESC] NVARCHAR(200), --�̻�����
	State INT DEFAULT(1) NOT NULL, --״̬
	STime DATETIME DEFAULT(GETDATE()) --����ʱ��
);
END;
GO

INSERT INTO N_Merchant(MerchantId, Name, Code, [DESC]) 
SELECT N'111111', N'�����û�', N'test01', N'�����û�' 
WHERE NOT EXISTS(SELECT 1 FROM N_Merchant WHERE MerchantId = N'111111');
GO

IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_User') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_User') AND name = N'MerchantId')
	ALTER TABLE N_User ADD MerchantId NVARCHAR(50);
GO

IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_User') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_User') AND name = N'Token')
	ALTER TABLE N_User ADD Token NVARCHAR(250);
GO

IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_User') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_User') AND name = N'ExpirationTime')
	ALTER TABLE N_User ADD ExpirationTime DateTime;
GO

UPDATE N_User SET MerchantId = N'111111';
GO

IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_UserCharge') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_UserCharge') AND name = N'Ss3Id')
	ALTER TABLE N_UserCharge ADD Ss3Id NVARCHAR(50);
GO

IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_UserGetCash') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_UserGetCash') AND name = N'Ss3Id')
	ALTER TABLE N_UserGetCash ADD Ss3Id NVARCHAR(50);
GO