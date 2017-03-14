:: 要确保文件被设置为 Encode in ANSI, 这样在运行时就不会在命令行开头出现奇怪的字符 (´╗┐) 了
:: 使用RoundHousE nuget package, 这样就可以不必将rh.exe纳入项目中
:: TODO: 此批文件必须放在项目的根目录中, 否则运行有问题, 原因不明(可能是因为最后一个--files的配置问题)
:: rh.exe的使用参见 https://github.com/chucknorris/roundhouse/wiki/ConfigurationOptions

..\packages\roundhouse.0.8.6\bin\rh.exe --sc RoundhousE --cs "Data Source=.\SQLEXPRESS;Initial Catalog=Sample_local;Integrated Security=true" --files RoundhousE
pause
