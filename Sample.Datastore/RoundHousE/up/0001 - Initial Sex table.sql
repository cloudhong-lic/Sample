-- begin tran 可以理解成新建一个还原点
-- commit tran提交这个自begin tran开始的修改
-- rollback tran 表示还原到上个还原点
BEGIN TRAN

--
-- Sex table
--

-- 添加数据表
CREATE TABLE [Sample].[Sex](
	[Id] INT NOT NULL,
	[Description] NVARCHAR(255) NOT NULL,
	-- 使用 CONSTRAINT 来设定默认值约束的名称
	[UpdateTime] DATETIMEOFFSET(2) NOT NULL CONSTRAINT [DF_Sex_UpdateTime] DEFAULT SYSDATETIMEOFFSET(),
	-- 为主键添加聚合索引
	CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED ([Id]),
)
GO	--每个被GO分隔的语句都是一个单独的事务，一个语句执行失败不会影响其它语句执行。

-- 自动更新 UpdateTime
CREATE TRIGGER [Sample].[TR_Sex_UpdateTime] ON [Sample].[Sex]
AFTER UPDATE AS
BEGIN
	UPDATE [Sample].[Sex]
	SET UpdateTime = SYSDATETIMEOFFSET()
	FROM inserted
	WHERE inserted.[Id] = [Sex].[Id]
END
GO

-- 添加表的说明注释
-- TODO: [SCHEMA].[Sample]目前被手动添加到了Table里. 需要在此脚本之前再添加一个CREATE SCHEMA的脚本
DECLARE @v sql_variant
SET @v = N'Sex table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'Sample', N'TABLE', N'Sex', NULL, NULL
GO

-- 添加表内字段的说明注释
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

-- 插入数据
INSERT INTO [Sample].[Sex] ([Id], [Description]) VALUES
(1, 'Male'),
(2, 'Female')
GO

-- commit 之前，你修改的只是内存里的数据，commit是将你对内存所做的修改存入数据库里面。
-- rollback是将从上次commit以来所做的修改全部抹掉。下次commit也不会将这些修改存入数据库了。
COMMIT
