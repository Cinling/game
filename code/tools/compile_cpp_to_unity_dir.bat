
REM compaile C++
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" "..\srv\srv\srv.vcxproj" /p:Configuration=Debug /m

copy ..\srv\srv\Debug\srv.exe ..\unity3d\

pause