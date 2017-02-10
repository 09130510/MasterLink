SETLOCAL ENABLEDELAYEDEXPANSION
@ECHO OFF
FOR /F "tokens=1,2 delims=#" %%a IN ('"PROMPT #$H#$E# & ECHO ON & FOR %%b IN (1) DO REM"') DO (
  SET "DEL=%%a"
)
CALL :DateTime
CLS


:UtilityPath
SET MSBuildPath=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
SET DEVPath="C:\Program Files (x86)\MicrosOft Visual Studio 10.0\Common7\IDE\"
SET ILMergePath="C:\Program Files (x86)\Microsoft\ILMerge\"
SET Assembly="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0"


:CommPath
SET ProjectName=PriceCalculator2015.sln
SET RootDestinationPath=E:\SVNProjects\Release\SpecializeCalculator\
SET SLNSource=E:\SVNProjects\trunk\Pricing\
SET Source40=E:\SVNProjects\trunk\Pricing\SpecializeCalculator\
SET Source45=E:\SVNProjects\trunk\Pricing\SpecializeCalculator\
SET Destination40=%RootDestinationPath%.NET4.0\
SET Destination45=%RootDestinationPath%.NET4.5\
SET Bak=%RootDestinationPath%Bak\

:DynamicPath
CLS	
ECHO.
SET Ver=%1
SET /P Ver=Version :
IF NOT '%Ver%'=='' SET Ver=%Ver%

:Prepare
ECHO.
CALL :ColorText 0B "==============="
CALL :ColorText B0 "[Destination Prepare]"
CALL :ColorText 0B "==============="
ECHO.
IF NOT EXIST %RootDestinationPath% MD %RootDestinationPath%
IF EXIST %Destination40%%Ver%\ (	
	XCOPY /E /Y /I %Destination40%%Ver%\* %Bak%%Ver%_%Today%_%Now%\.NET4.0\ 
	DEL /Q %Destination40%%Ver%\*	
)ELSE MD %Destination40%%Ver%
REM IF EXIST %Destination45% (	
REM 	XCOPY /E /Y /I %Destination45%* %Bak%%Today%_%Now%\.NET4.5\ 
REM 	DEL /Q %Destination45%*	
REM )ELSE MD %Destination45%

IF NOT EXIST %Destination40%%Ver%\Debug\  MD %Destination40%%Ver%\Debug\
IF NOT EXIST %Destination40%%Ver%\Release\  MD %Destination40%%Ver%\Release\

:Framework40
REM ECHO.
REM CALL :ColorText 0D "==============="
REM CALL :ColorText D0 "[Build .NET Framework 4.0]"
REM CALL :ColorText 0D "==============="
REM ECHO.
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Release /v:q /fl /nologo
REM ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[ILMerge .NET Framework 4.0 Debug]"
CALL :ColorText 0D "==============="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:SpecializeCalculator.exe %Source40%bin\x86\Debug\SpecializeCalculator.exe %Source40%bin\x86\Debug\PriceLib.dll %Source40%bin\x86\Debug\ServiceStack.Common.dll %Source40%bin\x86\Debug\ServiceStack.Interfaces.dll %Source40%bin\x86\Debug\ServiceStack.Redis.dll %Source40%bin\x86\Debug\ServiceStack.Text.dll %Source40%bin\x86\Debug\SourceCell.dll %Source40%bin\x86\Debug\SourceGrid.dll %Source40%bin\x86\Debug\ThemeVS2003.dll %Source40%bin\x86\Debug\WeifenLuo.WinFormsUI.Docking.dll %Source40%bin\x86\Debug\AxInterop.IPUSHXLib.dll %Source40%bin\x86\Debug\AxInterop.XQUOTELib.dll %Source40%bin\x86\Debug\Dapper.dll %Source40%bin\x86\Debug\INIFileParser.dll %Source40%bin\x86\Debug\Interop.IPUSHXLib.dll %Source40%bin\x86\Debug\Interop.XQUOTELib.dll %Source40%bin\x86\Debug\log4net.dll  
ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[Move .NET Framework 4.0 Debug]"
CALL :ColorText 0D "==============="
ECHO.
Copy SpecializeCalculator.exe %Destination40%%Ver%\Debug\
COPY %Source40%bin\x86\Debug\Config.ini %Destination40%%Ver%\Debug\ /Y
COPY %Source40%bin\x86\Debug\LogConfig.xml %Destination40%%Ver%\Debug\ /Y
REM COPY %Source40%bin\x86\Debug\PATSAPI.dll %Destination40%%Ver%\Debug\ /Y
REM COPY %Source40%bin\x86\Debug\SKOSQuoteLib.dll %Destination40%%Ver%\Debug\ /Y
REM COPY %Source40%bin\x86\Debug\SSLSocketLib.dll %Destination40%%Ver%\Debug\ /Y

REM ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[ILMerge .NET Framework 4.0 Release]"
CALL :ColorText 0D "==============="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:SpecializeCalculator.exe %Source40%bin\x86\Release\SpecializeCalculator.exe %Source40%bin\x86\Release\PriceLib.dll %Source40%bin\x86\Release\ServiceStack.Common.dll %Source40%bin\x86\Release\ServiceStack.Interfaces.dll %Source40%bin\x86\Release\ServiceStack.Redis.dll %Source40%bin\x86\Release\ServiceStack.Text.dll %Source40%bin\x86\Release\SourceCell.dll %Source40%bin\x86\Release\SourceGrid.dll %Source40%bin\x86\Release\ThemeVS2003.dll %Source40%bin\x86\Release\WeifenLuo.WinFormsUI.Docking.dll %Source40%bin\x86\Release\AxInterop.IPUSHXLib.dll %Source40%bin\x86\Release\AxInterop.XQUOTELib.dll %Source40%bin\x86\Release\Dapper.dll %Source40%bin\x86\Release\INIFileParser.dll %Source40%bin\x86\Release\Interop.IPUSHXLib.dll %Source40%bin\x86\Release\Interop.XQUOTELib.dll %Source40%bin\x86\Release\log4net.dll  

ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[Move .NET Framework 4.0 Release]"
CALL :ColorText 0D "==============="
ECHO.
Copy SpecializeCalculator.exe %Destination40%%Ver%\Release\
COPY %Source40%bin\x86\Release\Config.ini %Destination40%%Ver%\Release\ /Y
COPY %Source40%bin\x86\Release\LogConfig.xml %Destination40%%Ver%\Release\ /Y
REM COPY %Source40%bin\x86\Release\PATSAPI.dll %Destination40%%Ver%\Release\ /Y
REM COPY %Source40%bin\x86\Release\SKOSQuoteLib.dll %Destination40%%Ver%\Release\ /Y
REM COPY %Source40%bin\x86\Release\SSLSocketLib.dll %Destination40%%Ver%\Release\ /Y

REM :Framework45
REM ECHO.
REM CALL :ColorText 0A "==============="
REM CALL :ColorText A0 "[Build .NET Framework 4.5]"
REM CALL :ColorText 0A "==============="
REM ECHO.
REM %MSBuildPath%MSBuild.exe "%Source45%%ProjectName%" /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Release /v:q /fl /nologo
REM ECHO.
REM CALL :ColorText 0A "==============="
REM CALL :ColorText A0 "[ILMerge .NET Framework 4.5]"
REM CALL :ColorText 0A "==============="
REM ECHO.
REM %ILMergePath%ILMerge.exe /log /target:library /targetplatform:v4,%Assembly% /out:NuComponent.dll /xmldocs %Source45%SourceGrid_4_30_src\SourceGrid\bin\Release\SourceGrid.dll %Source45%SourceCell\bin\Release\SourceCell.dll %Source45%Extension\bin\Release\ObjectListView.dll %Source45%Extension\bin\Release\WeifenLuo.WinFormsUI.Docking.dll %Source45%Extension\bin\Release\Extension.dll %Source45%Extension\bin\Release\clrzmq.dll %Source45%Extension\bin\Release\ThemeVS2003.dll %Source45%Extension\bin\Release\ThemeVS2012Light.dll %Source45%Extension\bin\Release\ThemeVS2013Blue.dll
REM ECHO.
REM CALL :ColorText 0A "==============="
REM CALL :ColorText A0 "[Move .NET Framework 4.5]"
REM CALL :ColorText 0A "==============="
REM ECHO.
REM MOVE NuComponent.* %Destination45%
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
