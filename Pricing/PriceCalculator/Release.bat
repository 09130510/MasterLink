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
SET Assembly40="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\"


:CommPath
SET ProjectName=PriceCalculator2015.sln
SET RootDestinationPath=E:\Masterlink\Release\PriceCalculator\
SET SLNSource=E:\Masterlink\Pricing\
SET x86Release=E:\Masterlink\Pricing\PriceCalculator\bin\x86\Release\
SET x86Debug=E:\Masterlink\Pricing\PriceCalculator\bin\x86\Debug\
SET x64Release=E:\Masterlink\Pricing\PriceCalculator\bin\Release\
SET x64Debug=E:\Masterlink\Pricing\PriceCalculator\bin\Debug\
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


:x86Release
CALL :ColorText 0D "====================="
CALL :ColorText D0 "[ILMerge x86 Release]"
CALL :ColorText 0D "====================="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4 /out:PriceCalculator.exe %x86Release%PriceCalculator.exe %x86Release%AxInterop.IPUSHXLib.dll %x86Release%AxInterop.XQUOTELib.dll %x86Release%Dapper.dll %x86Release%INIFileParser.dll %x86Release%Interop.IPUSHXLib.dll %x86Release%Interop.XQUOTELib.dll %x86Release%log4net.dll %x86Release%Newtonsoft.Json.dll %x86Release%ObjectListView.dll %x86Release%PriceLib.dll %x86Release%ServiceStack.Common.dll %x86Release%ServiceStack.Interfaces.dll %x86Release%ServiceStack.Redis.dll %x86Release%ServiceStack.Text.dll %x86Release%SourceCell.dll %x86Release%SourceGrid.dll %x86Release%WeifenLuo.WinFormsUI.Docking.dll %x86Release%WeifenLuo.WinFormsUI.Docking.ThemeVS2003.dll     
ECHO.
CALL :ColorText 0D "=================="
CALL :ColorText D0 "[Move x86 Release]"
CALL :ColorText 0D "=================="
ECHO.
Move PriceCalculator.exe %Destinationx86Release%%Ver%
COPY %x86Release%Config.ini %Destinationx86Release%%Ver% /Y
COPY %x86Release%LogConfig.xml %Destinationx86Release%%Ver% /Y

:x86Debug     
ECHO.
CALL :ColorText 0D "================"
CALL :ColorText D0 "[Move x86 Debug]"
CALL :ColorText 0D "================"
ECHO.
Copy %x86Debug%PriceCalculator.exe %Destinationx86Debug%%Ver%
COPY %x86Debug%Config.ini %Destinationx86Debug%%Ver% /Y
COPY %x86Debug%LogConfig.xml %Destinationx86Debug%%Ver% /Y
COPY %x86Debug%*.dll %Destinationx86Debug%%Ver% /Y

:x64Release
CALL :ColorText 0D "====================="
CALL :ColorText D0 "[ILMerge x64 Release]"
CALL :ColorText 0D "====================="
ECHO.
%ILMergePath%ILMerge.exe /log /target:winexe /targetplatform:v4 /out:PriceCalculator.exe %x64Release%PriceCalculator.exe %x64Release%AxInterop.IPUSHXLib.dll %x64Release%AxInterop.XQUOTELib.dll %x64Release%Dapper.dll %x64Release%INIFileParser.dll %x64Release%Interop.IPUSHXLib.dll %x64Release%Interop.XQUOTELib.dll %x64Release%log4net.dll %x64Release%Newtonsoft.Json.dll %x64Release%ObjectListView.dll %x64Release%PriceLib.dll %x64Release%ServiceStack.Common.dll %x64Release%ServiceStack.Interfaces.dll %x64Release%ServiceStack.Redis.dll %x64Release%ServiceStack.Text.dll %x64Release%SourceCell.dll %x64Release%SourceGrid.dll %x64Release%WeifenLuo.WinFormsUI.Docking.dll %x64Release%WeifenLuo.WinFormsUI.Docking.ThemeVS2003.dll     
ECHO.
CALL :ColorText 0D "=================="
CALL :ColorText D0 "[Move x64 Release]"
CALL :ColorText 0D "=================="
ECHO.
Move PriceCalculator.exe %Destinationx64Release%%Ver%
COPY %x64Release%Config.ini %Destinationx64Release%%Ver% /Y
COPY %x64Release%LogConfig.xml %Destinationx64Release%%Ver% /Y

:x64Debug     
ECHO.
CALL :ColorText 0D "================"
CALL :ColorText D0 "[Move x64 Debug]"
CALL :ColorText 0D "================"
ECHO.
Copy %x64Debug%PriceCalculator.exe %Destinationx64Debug%%Ver%
COPY %x64Debug%Config.ini %Destinationx64Debug%%Ver% /Y
COPY %x64Debug%LogConfig.xml %Destinationx64Debug%%Ver% /Y
COPY %x64Debug%*.dll %Destinationx64Debug%%Ver% /Y

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
