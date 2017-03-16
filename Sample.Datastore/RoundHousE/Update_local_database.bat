:: 要确保文件被设置为 Encode in ANSI, 这样在运行时就不会在命令行开头出现奇怪的字符 (´╗┐) 了
:: 使用RoundHousE nuget package, 这样就可以不必将rh.exe纳入项目中
:: rh.exe的使用参见 https://github.com/chucknorris/roundhouse/wiki/ConfigurationOptions

:: TODO: 注意rh.exe所在的目录路径, 以及最后一个--files参数的路径. 可以省略??

:: --sc后面是RoundHousE在数据库中自动生成的table的前置名词: 例如Sample.ScriptsRun
:: 只要--sc后面的前置名词设置地与SCHEMA一致, 就不需要添加CREATE SCHEMA [Sample] AUTHORIZATION [dbo], 因为RoundHousE在创建三个内置脚本表的时候会创建的

..\..\packages\roundhouse.0.8.6\bin\rh.exe --sc "Sample" --cs "Data Source=.\SQLEXPRESS;Initial Catalog=Sample_local;Integrated Security=true" --files ..\RoundhousE
pause
