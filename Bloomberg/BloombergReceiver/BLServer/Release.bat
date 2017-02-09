SETLOCAL ENABLEDELAYEDEXPANSION
@ECHO OFF
FOR /F "tokens=1,2 delims=#" %%a IN ('"PROMPT #$H#$E# & ECHO ON & FOR %%b IN (1) DO REM"') DO (
  SET "DEL=%%a"
)
CALL :DateTime
CLS


:UtilityPath
SET MSBuildPath=C:\Windows\Microsoft.NET\Framework\v4.0.30319\
SET DEVPath="C:\Program Files (x86)\MicrosOft Visual Studio 10.0\Common7\IDE\"
SET ILMergePath="C:\Program Files (x86)\Microsoft\ILMerge\"
SET Assembly="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0"


:CommPath
SET ProjectName=BloombergReceiver.sln
SET RootDestinationPath=E:\SVNProjects\Release\Bloomberg\BLPServer\
SET SLNSource=E:\SVNProjects\trunk\Bloomberg\BloombergReceiver\
SET Source=E:\SVNProjects\trunk\Bloomberg\BloombergReceiver\BLServer\
SET DestinationDebug=%RootDestinationPath%Debug\
SET DestinationRelease=%RootDestinationPath%Release\
SET BakDebug=%RootDestinationPath%Bak\Debug\
SET BakRelease=%RootDestinationPath%Bak\Release\

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
IF NOT EXIST %BakDebug% MD %BakDebug%
IF NOT EXIST %BakRelease% MD %BakRelease%

IF EXIST %DestinationDebug%%Ver%\ (	
	XCOPY /E /Y /I %DestinationDebug%%Ver%\* %BakDebug%%Ver%_%Today%_%Now%\ 
	DEL /Q %DestinationDebug%%Ver%\*	
)ELSE MD %DestinationDebug%%Ver%

IF EXIST %DestinationRelease%%Ver%\ (	
	XCOPY /E /Y /I %DestinationRelease%%Ver%\* %BakRelease%%Ver%_%Today%_%Now%\ 
	DEL /Q %DestinationRelease%%Ver%\*	
)ELSE MD %DestinationRelease%%Ver%



:Framework40
REM ECHO.
REM CALL :ColorText 0D "========="
REM CALL :ColorText D0 "[ReBuild]"
REM CALL :ColorText 0D "========="
REM ECHO.
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Debug;WindowsSDKVersionOverride=v7.0A /v:q /fl /nologo
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Release;WindowsSDKVersionOverride=v7.0A /v:q /fl /nologo
ECHO.
CALL :ColorText 0E "================="
CALL :ColorText E0 "[ILMerge Release]"
CALL :ColorText 0E "================="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:BLPServer.exe %Source%bin\Release\BLPServer.exe %Source%bin\Release\AxInterop.IPUSHXLib.dll %Source%bin\Release\AxInterop.XQUOTELib.dll  %Source%bin\Release\Bloomberglp.Blpapi.dll %Source%bin\Release\BLParser.dll %Source%bin\Release\BLSubscriber.dll %Source%bin\Release\clrzmq.dll %Source%bin\Release\Interop.IPUSHXLib.dll %Source%bin\Release\Interop.XQUOTELib.dll %Source%bin\Release\log4net.dll  %Source%bin\Release\PriceLib.dll %Source%bin\Release\ServiceStack.Common.dll %Source%bin\Release\ServiceStack.Interfaces.dll %Source%bin\Release\ServiceStack.Text.dll  %Source%bin\Release\ServiceStack.Redis.dll %Source%bin\Release\SourceCell.dll %Source%bin\Release\SourceGrid.dll %Source%bin\Release\WeifenLuo.WinFormsUI.Docking.dll %Source%bin\Release\WeifenLuo.WinFormsUI.Docking.ThemeVS2003.dll  
ECHO.
CALL :ColorText 0A "==================="
CALL :ColorText A0 "[Move Release File]"
CALL :ColorText 0A "==================="
ECHO.
Copy BLPServer.exe %DestinationRelease%%Ver%
COPY %Source%bin\Release\Config.ini %DestinationRelease%%Ver% /Y
ECHO.
CALL :ColorText 0A "==================="
CALL :ColorText A0 "[Move Debug File]"
CALL :ColorText 0A "==================="
Copy %Source%bin\Debug\*.exe %DestinationDebug%%Ver%
COPY %Source%bin\Debug\Config.ini %DestinationDebug%%Ver% /Y
COPY %Source%bin\Debug\*.dll %DestinationDebug%%Ver% /Y
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
