SETLOCAL ENABLEDELAYEDEXPANSION
@ECHO OFF
FOR /F "tokens=1,2 delims=#" %%a IN ('"PROMPT #$H#$E# & ECHO ON & FOR %%b IN (1) DO REM"') DO (
	SET "DEL=%%a"
)

SET OfficePath="C:\Program Files\Microsoft Office\"

IF EXIST %OfficePath%Office10\ (
  IF EXIST %OfficePath%Office10\config.ini (
    CALL :ColorText 0E "===============[Office10 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office10 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office10\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office10 Not Exist]==============="
  ECHO.
)

IF EXIST %OfficePath%Office11\ (
  IF EXIST %OfficePath%Office11\config.ini (
    CALL :ColorText 0E "===============[Office11 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office11 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office11\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office11 Not Exist]==============="
  ECHO.
)

IF EXIST %OfficePath%Office12\ (
  IF EXIST %OfficePath%Office12\config.ini (
    CALL :ColorText 0E "===============[Office12 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office12 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office12\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office12 Not Exist]==============="
  ECHO.
)
IF EXIST %OfficePath%Office13\ (
  IF EXIST %OfficePath%Office13\config.ini (
    CALL :ColorText 0E "===============[Office13 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office13 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office13\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office13 Not Exist]==============="
  ECHO.
)
IF EXIST %OfficePath%Office14\ (
  IF EXIST %OfficePath%Office14\config.ini (
    CALL :ColorText 0E "===============[Office14 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office14 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office14\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office14 Not Exist]==============="
  ECHO.
)
IF EXIST %OfficePath%Office15\ (
  IF EXIST %OfficePath%Office15\config.ini (
    CALL :ColorText 0E "===============[Office15 Exist]==============="
    ECHO.
  ) ELSE (
    CALL :ColorText 0E "===============[Office15 Copy]==============="
    ECHO.
    COPY  config.ini %OfficePath%Office15\
  )
) ELSE (
  CALL :ColorText 0D "===============[Office15 Not Exist]==============="
  ECHO.
)

CALL :ColorText 0E "===============[Regasm Unregister]==============="
ECHO.
regasm /u RedisRTD.dll

CALL :ColorText 0E "===============[Regasm Register]==============="
ECHO.
regasm RedisRTD.dll /codebase /t RedisRTD.tlb

pause
GOTO :EOF

:ColorText
ECHO OFF
<nul SET /P ".=%DEL%" > "%~2"
FINDSTR /V /A:%1 /R "^$" "%~2" nul
DEL "%~2" > nul 2>&1
GOTO :EOF


