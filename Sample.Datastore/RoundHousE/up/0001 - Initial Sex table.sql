BEGIN TRAN

--
-- Sex table
--

CREATE TABLE [Sample].[Sex](
	[Id] INT NOT NULL,
	[Description] NVARCHAR(255) NOT NULL,
	[UpdateTime] DATETIMEOFFSET(2) NOT NULL CONSTRAINT [DF_Sex_UpdateTime] DEFAULT SYSDATETIMEOFFSET(),
	CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED ([Id]),
)
GO

CREATE TRIGGER [Sample].[TR_Sex_UpdateTime] ON [Sample].[Sex]
AFTER UPDATE AS
BEGIN
	UPDATE [Sample].[Sex]
	SET UpdateTime = SYSDATETIMEOFFSET()
	FROM inserted
	WHERE inserted.[Id] = [Sex].[Id]
END
GO

DECLARE @v sql_variant
SET @v = N'Sex table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Sex', NULL, NULL
GO
DECLARE @v sql_variant
SET @v = N'Unique identifier of sexes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Sex', N'COLUMN', N'Id'
GO
DECLARE @v sql_variant
SET @v = N'The description of sexes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Sex', N'COLUMN', N'Description'
GO
DECLARE @v sql_variant
SET @v = N'Time when the record is last updated, populated by default value and trigger'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Sex', N'COLUMN', N'UpdateTime'
GO

INSERT INTO [Sample].[Sex] ([Id], [Description]) VALUES 
(1, 'Male'),
(2, 'Female')
GO

COMMIT
