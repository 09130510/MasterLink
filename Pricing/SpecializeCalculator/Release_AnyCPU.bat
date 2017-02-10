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
SET Assembly="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1"


:CommPath
SET ProjectName=PriceCalculator2015.sln
SET RootDestinationPath=E:\MasterLink\Release\SpecializeCalculator\AnyCPU\
SET SLNSource=E:\MasterLink\Pricing\
SET Source=E:\MasterLink\Pricing\SpecializeCalculator\
SET DestinationRelease=%RootDestinationPath%Release\
SET DestinationDebug=%RootDestinationPath%Debug\
SET BakRelease=%RootDestinationPath%Bak\AnyCPU\Release\
SET BakDebug=%RootDestinationPath%Bak\AnyCPU\Debug\

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
IF EXIST %DestinationRelease%%Ver%\ (	
	XCOPY /E /Y /I %DestinationRelease%%Ver%\* %BakRelease%%Ver%_%Today%_%Now%\ 
	DEL /Q %DestinationRelease%%Ver%\*	
)ELSE MD %DestinationRelease%%Ver%

IF EXIST %DestinationDebug%%Ver%\ (	
	XCOPY /E /Y /I %DestinationDebug%%Ver%\* %BakDebug%%Ver%_%Today%_%Now%\ 
	DEL /Q %DestinationDebug%%Ver%\*	
)ELSE MD %DestinationDebug%%Ver%
REM IF EXIST %Destination45% (	
REM 	XCOPY /E /Y /I %Destination45%* %Bak%%Today%_%Now%\.NET4.5\ 
REM 	DEL /Q %Destination45%*	
REM )ELSE MD %Destination45%

IF NOT EXIST %DestinationRelease%%Ver%\  MD %DestinationRelease%%Ver%
IF NOT EXIST %DestinationDebug%%Ver%\  MD %DestinationDebug%%Ver%\

:Framework40
REM ECHO.
REM CALL :ColorText 0D "==============="
REM CALL :ColorText D0 "[Build .NET Framework 4.0]"
REM CALL :ColorText 0D "==============="
REM ECHO.
REM %MSBuildPath%MSBuild.exe %SLNSource%%ProjectName% /t:Clean;Rebuild /p:Platform="Any CPU";Configuration=Release /v:q /fl /nologo
REM ECHO.
REM *****DEBUG ¤£Merage
REM CALL :ColorText 0D "==============="
REM CALL :ColorText D0 "[ILMerge  Debug]"
REM CALL :ColorText 0D "==============="
REM ECHO.
REM %ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:SpecializeCalculator.exe %Source%bin\Debug\SpecializeCalculator.exe %Source%bin\Debug\AxInterop.IPUSHXLib.dll %Source%bin\Debug\AxInterop.XQUOTELib.dll %Source%bin\Debug\Dapper.dll %Source%bin\Debug\INIFileParser.dll %Source%bin\Debug\Interop.IPUSHXLib.dll %Source%bin\Debug\Interop.XQUOTELib.dll %Source%bin\Debug\log4net.dll %Source%bin\Debug\PriceLib.dll %Source%bin\Debug\ServiceStack.Common.dll %Source%bin\Debug\ServiceStack.Interfaces.dll %Source%bin\Debug\ServiceStack.Redis.dll %Source%bin\Debug\ServiceStack.Text.dll %Source%bin\Debug\SourceCell.dll %Source%bin\Debug\SourceGrid.dll %Source%bin\Debug\WeifenLuo.WinFormsUI.Docking.dll %Source%bin\Debug\WeifenLuo.WinFormsUI.Docking.ThemeVS2003.dll
  
ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[Move  Debug]"
CALL :ColorText 0D "==============="
ECHO.
REM *****DEBUG ¤£Merage
REM Copy SpecializeCalculator.exe %DestinationDebug%%Ver%\
Copy %Source%bin\Debug\SpecializeCalculator.exe %DestinationDebug%%Ver%\ /Y
Copy %Source%bin\Debug\*.dll %DestinationDebug%%Ver%\ /Y
COPY %Source%bin\Debug\Config.ini %DestinationDebug%%Ver%\ /Y
COPY %Source%bin\Debug\LogConfig.xml %DestinationDebug%%Ver%\ /Y
REM COPY %Source40%bin\x86\Debug\PATSAPI.dll %Destination40%%Ver%\Debug\ /Y
REM COPY %Source40%bin\x86\Debug\SKOSQuoteLib.dll %Destination40%%Ver%\Debug\ /Y
REM COPY %Source40%bin\x86\Debug\SSLSocketLib.dll %Destination40%%Ver%\Debug\ /Y

REM ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[ILMerge  Release]"
CALL :ColorText 0D "==============="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4,%Assembly% /out:SpecializeCalculator.exe %Source%bin\Release\SpecializeCalculator.exe %Source%bin\Release\AxInterop.IPUSHXLib.dll %Source%bin\Release\AxInterop.XQUOTELib.dll %Source%bin\Release\Dapper.dll %Source%bin\Release\INIFileParser.dll %Source%bin\Release\Interop.IPUSHXLib.dll %Source%bin\Release\Interop.XQUOTELib.dll %Source%bin\Release\log4net.dll %Source%bin\Release\PriceLib.dll %Source%bin\Release\ServiceStack.Common.dll %Source%bin\Release\ServiceStack.Interfaces.dll %Source%bin\Release\ServiceStack.Redis.dll %Source%bin\Release\ServiceStack.Text.dll %Source%bin\Release\SourceCell.dll %Source%bin\Release\SourceGrid.dll %Source%bin\Release\WeifenLuo.WinFormsUI.Docking.dll %Source%bin\Release\WeifenLuo.WinFormsUI.Docking.ThemeVS2003.dll  

ECHO.
CALL :ColorText 0D "==============="
CALL :ColorText D0 "[Move  Release]"
CALL :ColorText 0D "==============="
ECHO.
Copy SpecializeCalculator.exe %DestinationRelease%%Ver%\
COPY %Source%bin\Release\Config.ini %DestinationRelease%%Ver%\ /Y
COPY %Source%bin\Release\LogConfig.xml %DestinationRelease%%Ver%\ /Y
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
