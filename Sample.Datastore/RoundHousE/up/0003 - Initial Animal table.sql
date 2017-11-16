BEGIN TRAN

--
-- Animal table
--

CREATE TABLE [Sample].[Animal](
	[AnimalKey] INT NOT NULL,
	[BirthDate] DATETIMEOFFSET(2) NULL,
	[SireAnimalKey] INT NULL,
	[DamAnimalKey] INT NULL,
	[SexId] INT NOT NULL,
	[SpeciesId] INT NOT NULL,
	[UpdateTime] DATETIMEOFFSET(2) NOT NULL CONSTRAINT [DF_Animal_UpdateTime] DEFAULT SYSDATETIMEOFFSET(),
	CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED ([AnimalKey]),
	-- 与Sex和Species表做外键关联
	-- TODO: 是否需要做聚合索引??
	-- 先生成参考表, 再生成主表
	-- TODO: 考虑多建立几个表, 写一些不同的数据库脚本
	CONSTRAINT [FK_Animal_Sex] FOREIGN KEY ([SexId]) REFERENCES [Sample].[Sex]([Id]),
	CONSTRAINT [FK_Animal_Species] FOREIGN KEY ([SpeciesId]) REFERENCES [Sample].[Species]([Id])
)
GO

CREATE TRIGGER [Sample].[TR_Animal_UpdateTime] ON [Sample].[Animal]
AFTER UPDATE AS
BEGIN
	UPDATE [Sample].[Animal]
	SET UpdateTime = SYSDATETIMEOFFSET()
	FROM inserted
	WHERE inserted.[AnimalKey] = [Animal].[AnimalKey]
END
GO

DECLARE @v sql_variant
SET @v = N'Animal table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', NULL, NULL
GO
DECLARE @v sql_variant
SET @v = N'Unique identifier of animals'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'AnimalKey'
GO
DECLARE @v sql_variant
SET @v = N'BirthDate of the animal'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'BirthDate'
GO
DECLARE @v sql_variant
SET @v = N'The father of the animal'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'SireAnimalKey'
GO
DECLARE @v sql_variant
SET @v = N'The mother of the animal'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'DamAnimalKey'
GO
DECLARE @v sql_variant
SET @v = N'The reference id for Sex'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'SexId'
GO
DECLARE @v sql_variant
SET @v = N'The reference id for Species'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'SpeciesId'
GO
DECLARE @v sql_variant
SET @v = N'Time when the record is last updated, populated by default value and trigger'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Animal', N'COLUMN', N'UpdateTime'
GO

INSERT INTO [Sample].[Animal] ([AnimalKey], [SexId], [SpeciesId]) VALUES
(1, 1, 1),
(2, 2, 1)
GO

COMMIT
