-- 添加SCHEMA
-- 只有添加了SCHEMA后, 才能操作表集合, 例如[Sample].[Sex]
-- TODO: CREATE SCHEMA不能使用BEGIN TRAN和COMMIT, 不知道什么原因

CREATE SCHEMA [Sample] AUTHORIZATION [dbo]
GO
