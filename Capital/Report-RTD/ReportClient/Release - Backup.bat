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
SET ProjectName=Report+RTD2015.sln
SET RootDestinationPath=E:\SVNProjects\Release\Capital\ReportClient\
SET SLNSource=E:\SVNProjects\trunk\Capital\Report+RTD\
SET Source=E:\SVNProjects\trunk\Capital\Report+RTD\ReportClient\
SET SourceStr="E:\SVNProjects\trunk\Capital\Report+RTD\ReportClient\"
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
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /allowDup /out:CapitalReportClient.exe %Source%bin\Release\CapitalReportClient.exe %Source%bin\Release\Component.dll %Source%bin\Release\OrderProcessor.dll  %Source%bin\Release\PriceProcessor.dll    
ECHO.
CALL :ColorText 0A "==================="
CALL :ColorText A0 "[Move Release File]"
CALL :ColorText 0A "==================="
ECHO.
Copy CapitalReportClient.exe %DestinationRelease%%Ver%
REM Copy %SourceStr%bin\Release\CapitalReportClient.exe %DestinationRelease%%Ver%
COPY %SourceStr%bin\Release\Config.ini %DestinationRelease%%Ver% /Y
REM COPY %SourceStr%bin\Release\*.dll %DestinationRelease%%Ver% /Y
ECHO.
CALL :ColorText 0A "==================="
CALL :ColorText A0 "[Move Debug File]"
CALL :ColorText 0A "==================="
Copy %SourceStr%bin\Debug\CapitalReportClient.exe %DestinationDebug%%Ver%
COPY %SourceStr%bin\Debug\Config.ini %DestinationDebug%%Ver% /Y
COPY %SourceStr%bin\Debug\*.dll %DestinationDebug%%Ver% /Y
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
