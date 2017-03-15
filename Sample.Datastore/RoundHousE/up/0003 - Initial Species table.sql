BEGIN TRAN

--
-- Species table
--

CREATE TABLE [Sample].[Species](
	[Id] INT NOT NULL,
	[Description] NVARCHAR(255) NOT NULL,
	[UpdateTime] DATETIMEOFFSET(2) NOT NULL CONSTRAINT [DF_Species_UpdateTime] DEFAULT SYSDATETIMEOFFSET(),
	CONSTRAINT [PK_Species] PRIMARY KEY CLUSTERED ([Id]),
)
GO

CREATE TRIGGER [Sample].[TR_Species_UpdateTime] ON [Sample].[Species]
AFTER UPDATE AS
BEGIN
	UPDATE [Sample].[Species]
	SET UpdateTime = SYSDATETIMEOFFSET()
	FROM inserted
	WHERE inserted.[Id] = [Species].[Id]
END
GO

DECLARE @v sql_variant
SET @v = N'Species table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Species', NULL, NULL
GO
DECLARE @v sql_variant
SET @v = N'Unique identifier of Species'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Species', N'COLUMN', N'Id'
GO
DECLARE @v sql_variant
SET @v = N'The description of Species'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Species', N'COLUMN', N'Description'
GO
DECLARE @v sql_variant
SET @v = N'Time when the record is last updated, populated by default value and trigger'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Species', N'COLUMN', N'UpdateTime'
GO

INSERT INTO [Sample].[Species] ([Id], [Description]) VALUES 
(1, 'Cattle'),
(2, 'Deer')
GO

COMMIT
