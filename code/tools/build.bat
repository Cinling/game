
REM update git
git pull

REM packaging Unity3d
"C:\Program Files\Unity\Editor\Unity.exe" -batchmode -projectPath "D:\workspace\repository\u3d_package\code\unity3d" -nographics -executeMethod Build.Test -logFile "D:\workspace\repository\u3d_package\code\unity3d\bat_build.log"  -quit

REM compaile C++
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" "D:\workspace\repository\u3d_package\code\srv\srv\srv.vcxproj" /p:Configuration=Debug /m

REM copy srv.exe tu Unity3d folder
copy D:\workspace\repository\u3d_package\code\srv\srv\srv.exe E:\u3d_package\test\