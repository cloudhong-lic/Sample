:: Make sure the file using Encode in ANSI, then to prevent the weird characters (´╗┐) at the start of a batch file
:: This file must run at the root path of this project

..\packages\roundhouse.0.8.6\bin\rh.exe --sc RoundhousE --cs "Data Source=.\SQLEXPRESS;Initial Catalog=Sample_local;Integrated Security=true" --files RoundhousE
pause