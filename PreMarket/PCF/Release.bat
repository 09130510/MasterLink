SETLOCAL ENABLEDELAYEDEXPANSION
@ECHO OFF
FOR /F "tokens=1,2 delims=#" %%a IN ('"PROMPT #$H#$E# & ECHO ON & FOR %%b IN (1) DO REM"') DO (
  SET "DEL=%%a"
)
CALL :DateTime
CLS


:UtilityPath
SET MSBuildPath=C:\Windows\Microsoft.NET\Framework\v4.0.30319\
SET MSBuildPath64=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
SET DEVPath="C:\Program Files (x86)\MicrosOft Visual Studio 10.0\Common7\IDE\"
SET ILMergePath="C:\Program Files (x86)\Microsoft\ILMerge\"
REM Assembly最後面不能有斜線
SET Assembly40="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0"


:CommPath
SET ProjectName=PCF.sln
SET RootDestinationPath=E:\Masterlink\Release\PCF\
SET SLNSource=E:\Masterlink\PreMarket\PCF\
REM SET Source=E:\Masterlink\PreMarket\PCF\PCF\
REM SET DestinationDebug=%RootDestinationPath%Debug\
REM SET DestinationRelease=%RootDestinationPath%Release\
REM SET BakDebug=%RootDestinationPath%Bak\Debug\
REM SET BakRelease=%RootDestinationPath%Bak\Release\
SET x86Release=E:\Masterlink\PreMarket\PCF\PCF\bin\x86\Release\
SET x86Debug=E:\Masterlink\PreMarket\PCF\PCF\bin\x86\Debug\
SET x64Release=E:\Masterlink\PreMarket\PCF\PCF\bin\Release\
SET x64Debug=E:\Masterlink\PreMarket\PCF\PCF\bin\Debug\
SET Destinationx86Debug=%RootDestinationPath%x86\Debug\
SET Destinationx86Release=%RootDestinationPath%x86\Release\
SET Destinationx64Debug=%RootDestinationPath%x64\Debug\
SET Destinationx64Release=%RootDestinationPath%x64\Release\
SET Bakx86Debug=%RootDestinationPath%Bak\x86\Debug\
SET Bakx86Release=%RootDestinationPath%Bak\x86\Release\
SET Bakx64Debug=%RootDestinationPath%Bak\x64\Debug\
SET Bakx64Release=%RootDestinationPath%Bak\x64\Release\

:DynamicPath
CLS	
ECHO.
SET Ver=%1
SET /P Ver=Version :
IF NOT '%Ver%'=='' SET Ver=%Ver%

:Prepare
ECHO.
CALL :ColorText 0B "====================="
CALL :ColorText B0 "[Destination Prepare]"
CALL :ColorText 0B "====================="
ECHO.
IF NOT EXIST %RootDestinationPath% MD %RootDestinationPath%
IF EXIST %Destinationx86Debug%%Ver%\ (	
	XCOPY /E /Y /I %Destinationx86Debug%%Ver%\* %Bakx86Debug%%Ver%_%Today%_%Now%\
	DEL /Q %Destinationx86Debug%%Ver%\*	
)ELSE MD %Destinationx86Debug%%Ver%
IF EXIST %Destinationx86Release%%Ver%\ (	
	XCOPY /E /Y /I %Destinationx86Release%%Ver%\* %Bakx86Release%%Ver%_%Today%_%Now%\ 
	DEL /Q %Destinationx86Release%%Ver%\*	
)ELSE MD %Destinationx86Release%%Ver%
IF EXIST %Destinationx64Debug%%Ver%\ (	
	XCOPY /E /Y /I %Destinationx64Debug%%Ver%\* %Bakx64Debug%%Ver%_%Today%_%Now%\ 
	DEL /Q %Destinationx64Debug%%Ver%\*	
)ELSE MD %Destinationx64Debug%%Ver%
IF EXIST %Destinationx64Release%%Ver%\ (	
	XCOPY /E /Y /I %Destinationx64Release%%Ver%\* %Bakx64Release%%Ver%_%Today%_%Now%\ 
	DEL /Q %Destinationx64Release%%Ver%\*	
)ELSE MD %Destinationx64Release%%Ver%


:Framework40
REM ECHO.
REM CALL :ColorText 0D "========="
REM CALL :ColorText D0 "[ReBuild]"
REM CALL :ColorText 0D "========="
REM ECHO.
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Debug;WindowsSDKVersionOverride=v7.0A /v:q /fl /nologo
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Release;WindowsSDKVersionOverride=v7.0A /v:q /fl /nologo




:x86Release
CALL :ColorText 0D "====================="
CALL :ColorText D0 "[ILMerge x86 Release]"
CALL :ColorText 0D "====================="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly40% /out:PCF.exe %x86Release%PCF.exe %x86Release%AxInterop.IPUSHXLib.dll %x86Release%AxInterop.XQUOTELib.dll %x86Release%Dapper.dll %x86Release%HtmlAgilityPack.dll %x86Release%INIFileParser.dll %x86Release%Interop.IPUSHXLib.dll %x86Release%Interop.XQUOTELib.dll %x86Release%log4net.dll %x86Release%MySql.Data.dll %x86Release%Newtonsoft.Json.dll %x86Release%NuComponent.dll %x86Release%PriceLib.dll %x86Release%ServiceStack.Common.dll %x86Release%ServiceStack.Interfaces.dll %x86Release%ServiceStack.Redis.dll %x86Release%ServiceStack.Text.dll      
ECHO.
CALL :ColorText 0D "=================="
CALL :ColorText D0 "[Move x86 Release]"
CALL :ColorText 0D "=================="
ECHO.
Move PCF.exe %Destinationx86Release%%Ver%
COPY %x86Release%Config.ini %Destinationx86Release%%Ver% /Y

:x86Debug     
ECHO.
CALL :ColorText 0D "================"
CALL :ColorText D0 "[Move x86 Debug]"
CALL :ColorText 0D "================"
ECHO.
Copy %x86Debug%PCF.exe %Destinationx86Debug%%Ver%
COPY %x86Debug%Config.ini %Destinationx86Debug%%Ver% /Y
COPY %x86Debug%*.dll %Destinationx86Debug%%Ver% /Y

:x64Release
CALL :ColorText 0D "====================="
CALL :ColorText D0 "[ILMerge x64 Release]"
CALL :ColorText 0D "====================="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly40%\ /out:PCF.exe %x64Release%PCF.exe %x64Release%AxInterop.IPUSHXLib.dll %x64Release%AxInterop.XQUOTELib.dll %x64Release%Dapper.dll %x64Release%HtmlAgilityPack.dll %x64Release%INIFileParser.dll %x64Release%Interop.IPUSHXLib.dll %x64Release%Interop.XQUOTELib.dll %x64Release%log4net.dll %x64Release%MySql.Data.dll %x64Release%Newtonsoft.Json.dll %x64Release%NuComponent.dll %x64Release%PriceLib.dll %x64Release%ServiceStack.Common.dll %x64Release%ServiceStack.Interfaces.dll %x64Release%ServiceStack.Redis.dll %x64Release%ServiceStack.Text.dll     
ECHO.
CALL :ColorText 0D "=================="
CALL :ColorText D0 "[Move x64 Release]"
CALL :ColorText 0D "=================="
ECHO.
Move PCF.exe %Destinationx64Release%%Ver%
COPY %x64Release%Config.ini %Destinationx64Release%%Ver% /Y

:x64Debug     
ECHO.
CALL :ColorText 0D "================"
CALL :ColorText D0 "[Move x64 Debug]"
CALL :ColorText 0D "================"
ECHO.
Copy %x64Debug%PCF.exe %Destinationx64Debug%%Ver%
COPY %x64Debug%Config.ini %Destinationx64Debug%%Ver% /Y
COPY %x64Debug%*.dll %Destinationx64Debug%%Ver% /Y



REM ECHO.
REM CALL :ColorText 0E "================="
REM CALL :ColorText E0 "[ILMerge Release]"
REM CALL :ColorText 0E "================="
REM ECHO.
REM %ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:PCF.exe %Source%bin\Release\PCF.exe %Source%bin\Release\Dapper.dll %Source%bin\Release\HtmlAgilityPack.dll %Source%bin\Release\INIFileParser.dll %Source%bin\Release\log4net.dll %Source%bin\Release\Mysql.Data.dll %Source%bin\Release\Newtonsoft.Json.dll %Source%bin\Release\NuComponent.dll %Source%bin\Release\PriceLib.dll %Source%bin\Release\ServiceStack.Common.dll %Source%bin\Release\ServiceStack.Interfaces.dll %Source%bin\Release\ServiceStack.Redis.dll %Source%bin\Release\ServiceStack.Text.dll      
REM ECHO.
REM CALL :ColorText 0A "==================="
REM CALL :ColorText A0 "[Move Release File]"
REM CALL :ColorText 0A "==================="
REM ECHO.
REM Copy PCF.exe %DestinationRelease%%Ver%
REM COPY %Source%bin\Release\Config.ini %DestinationRelease%%Ver% /Y
REM ECHO.
REM CALL :ColorText 0A "==================="
REM CALL :ColorText A0 "[Move Debug File]"
REM CALL :ColorText 0A "==================="
REM Copy %Source%bin\Debug\*.exe %DestinationDebug%%Ver%
REM COPY %Source%bin\Debug\Config.ini %DestinationDebug%%Ver% /Y
REM COPY %Source%bin\Debug\*.dll %DestinationDebug%%Ver% /Y
GOTO End


:ColorText
ECHO OFF
<nul SET /P ".=%DEL%" > "%~2"
FINDSTR /V /A:%1 /R "^$" "%~2" nul
DEL "%~2" > nul 2>&1
GOTO :EOF


:DateTime
FOR /F "tokens=1-4 delims=/ " %%a IN ("%DATE%") DO (
	SET Today=%%a%%b%%c
)
FOR /F "tokens=1-4 delims=:. " %%a IN ("%TIME%") DO (
	SET Now=%%a%%b%%c
)
SET String=%Now%
SET /A Length=0
:Loop
IF DEFINED String (
	SET String=%String:~1%
	SET /A Length+=1
	GOTO Loop
)
IF %Length%==5 SET Now=0%Now%
GOTO :EOF


:End
PAUSE
EXIT
