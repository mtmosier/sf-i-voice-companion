@ECHO OFF
SET vaSoundDir1=C:\ProgramFiles (x86)\VoiceAttack\Sounds
SET vaSoundDir2=C:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds
SET vaSoundDir3=D:\ProgramFiles (x86)\VoiceAttack\Sounds
SET vaSoundDir4=D:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds
SET vaSoundDir5=E:\ProgramFiles (x86)\VoiceAttack\Sounds
SET vaSoundDir6=E:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds
SET vaSoundDir=""

FOR %%i IN (1 2 3 4 5 6) DO CALL :checkSoundDir "%%vaSoundDir%%i%%"
SET vaSoundDir=%vaSoundDir:"=%
IF "%vaSoundDir%" == "" (
  echo Unable to find VoiceAttack Sounds directory. Please refer to the documentation for instructions on configuring the path correctly.
  echo https://github.com/mtmosier/sf-i-voice-companion/tree/master/import
  pause
  @ECHO ON
  @exit /b
) else echo Found VA Sounds directory at %vaSoundDir%

SET companionsFound=Null

IF NOT EXIST "%vaSoundDir%" (
  echo "%vaSoundDir%" not found
  pause
  @ECHO ON
  @exit /b
)

cd "%vaSoundDir%"
FOR /D %%G IN (hcspack-*) do ( IF NOT "%%G" == "hcspack-SWSCUSTOM" call :processDir %%G )

SET companionsFound

pause
@ECHO ON
@exit /b


:strToLower
  FOR %%i IN ("A=a" "B=b" "C=c" "D=d" "E=e" "F=f" "G=g" "H=h" "I=i" "J=j" "K=k" "L=l" "M=m" "N=n" "O=o" "P=p" "Q=q" "R=r" "S=s" "T=t" "U=u" "V=v" "W=w" "X=x" "Y=y" "Z=z") DO CALL SET "%1=%%%1:%%~i%%"
  exit /b

:strToUpper
  FOR %%i IN ("a=A" "b=B" "c=C" "d=D" "e=E" "f=F" "g=G" "h=H" "i=I" "j=J" "k=K" "l=L" "m=M" "n=N" "o=O" "p=P" "q=Q" "r=R" "s=S" "t=T" "u=U" "v=V" "w=W" "x=X" "y=Y" "z=Z") DO CALL SET "%1=%%%1:%%~i%%"
  exit /b

:ucFirst
  CALL SET "str=%%%1%%"
  CALL :strToLower str
  SET first=%str:~0,1%
  CALL :strToUpper first
  CALL SET "%1=%first%%str:~1%"
  exit /b

:checkSoundDir
  IF %vaSoundDir% == "" (
    IF EXIST %1 SET vaSoundDir=%1
  )
  exit /b

:processDir
  SET curDir=%1
  SET /A MISSING_COUNT=0
  SET companionName=%curDir:hcspack-=%
  CALL :ucFirst companionName
  SET newDir=sf-i_%companionName%
  ECHO %curDir% copying to %newDir%
  SET companionsFound=%companionsFound%;%companionName%
  IF NOT EXIST "%vaSoundDir%\%newDir%\" mkdir "%vaSoundDir%\%newDir%"
  IF NOT EXIST "%vaSoundDir%\%newDir%\Non-Verbal Error\" mkdir "%vaSoundDir%\%newDir%\Non-Verbal Error"
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" "%vaSoundDir%\%newDir%\Non-Verbal Error\Error beep.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Non-Verbal Error\*" || (
    echo - Missing Non-Verbal Error
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Non-Verbal Confirmation\" mkdir "%vaSoundDir%\%newDir%\Non-Verbal Confirmation"
  IF NOT EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\" mkdir "%vaSoundDir%\%newDir%\Switch Companion Target"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose" "%vaSoundDir%\%newDir%\Switch Companion Target\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\A smidgen of power.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\A smidgen of power.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\A smidgen of power.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\I’m number one does this mean I can boss the crew around.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\I’m number one does this mean I can boss the crew around.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\I’m number one does this mean I can boss the crew around.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Vega online.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Jazz online.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Crew commands\Reporting.mp3" (
    copy "%vaSoundDir%\%curDir%\Crew commands\Reporting.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Reporting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Crew commands\Reporting for duty.mp3" (
    copy "%vaSoundDir%\%curDir%\Crew commands\Reporting for duty.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Reporting for duty.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Crew commands\Reporting for duty 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Crew commands\Reporting for duty 2.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Reporting for duty 2.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Switch Companion Target\*" || (
    echo - Missing Switch Companion Target
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\" mkdir "%vaSoundDir%\%newDir%\Switch Companion Source"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose" "%vaSoundDir%\%newDir%\Switch Companion Source\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\I'm afraid I can't do that Dave Just kidding.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\I'm afraid I can't do that Dave Just kidding.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\I'm afraid I can't do that Dave Just kidding.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Relinquishing command reluctantly.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Relinquishing command reluctantly.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Relinquishing command reluctantly.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Crew commands\Relinquishing command.mp3" (
    copy "%vaSoundDir%\%curDir%\Crew commands\Relinquishing command.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Relinquishing command.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\That's a wrap.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\That's a wrap.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\That's a wrap.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Vega offline.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Jazz offline.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Switch Companion Source\*" || (
    echo - Missing Switch Companion Source
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Listening Enabled\" mkdir "%vaSoundDir%\%newDir%\Listening Enabled"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\non-verbose" "%vaSoundDir%\%newDir%\Listening Enabled\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose" "%vaSoundDir%\%newDir%\Listening Enabled\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" "%vaSoundDir%\%newDir%\Listening Enabled\Vega online.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" "%vaSoundDir%\%newDir%\Listening Enabled\Jazz online.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Listening Enabled\*" || (
    echo - Missing Listening Enabled
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Listening Disabled\" mkdir "%vaSoundDir%\%newDir%\Listening Disabled"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose" "%vaSoundDir%\%newDir%\Listening Disabled\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" "%vaSoundDir%\%newDir%\Listening Disabled\Vega offline.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" "%vaSoundDir%\%newDir%\Listening Disabled\Jazz offline.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Listening Disabled\*" || (
    echo - Missing Listening Disabled
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Annoyed Response\" mkdir "%vaSoundDir%\%newDir%\Annoyed Response"
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Wise guys detected.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Wise guys detected.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Wise guys detected.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\You need to relax my friend.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\You need to relax my friend.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\You need to relax my friend.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\You need to relax.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\You need to relax.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\You need to relax.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\Was that French or something.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\Was that French or something.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Was that French or something.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\Was that French for something.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\Was that French for something.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Was that French for something.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\What was that I didn't hear you.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Additional dialogue extra\What was that I didn't hear you.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\What was that I didn't hear you.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\fine.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\fine.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\fine.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Fine I won't disturb you again.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Fine I won't disturb you again.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Fine I won't disturb you again.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Losing your temper with things.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Losing your temper with things.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Losing your temper with things.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Losing your temper with things alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Losing your temper with things alt.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Losing your temper with things alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\How rude.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\How rude.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\How rude.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\I'm not laughing.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\I'm not laughing.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\I'm not laughing.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\I'm not laughing 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\I'm not laughing 2.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\I'm not laughing 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\I'm not sure what to say to that.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\I'm not sure what to say to that.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\I'm not sure what to say to that.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\I'm not sure what to say to that 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\I'm not sure what to say to that 2.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\I'm not sure what to say to that 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Oh do grow up.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Oh do grow up.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Oh do grow up.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Oh grow up.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Oh grow up.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Oh grow up.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional Dialogue\Feel like to be folded in half.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional Dialogue\Feel like to be folded in half.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Feel like to be folded in half.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Sorry what did you say I was watching the football.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Sorry what did you say I was watching the football.mp3" "%vaSoundDir%\%newDir%\Annoyed Response\Sorry what did you say I was watching the football.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Annoyed Response\*" || (
    echo - Missing Annoyed Response
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Hello\" mkdir "%vaSoundDir%\%newDir%\Hello"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose" "%vaSoundDir%\%newDir%\Hello\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose" "%vaSoundDir%\%newDir%\Hello\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" "%vaSoundDir%\%newDir%\Hello\Hello.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" "%vaSoundDir%\%newDir%\Hello\Hello.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Greetings.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Greetings.mp3" "%vaSoundDir%\%newDir%\Hello\Greetings.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Hello alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Hello alt.mp3" "%vaSoundDir%\%newDir%\Hello\Hello alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Greetings to you too.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Greetings to you too.mp3" "%vaSoundDir%\%newDir%\Hello\Greetings to you too.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Hello\*" || (
    echo - Missing Hello
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\My name is\" mkdir "%vaSoundDir%\%newDir%\My name is"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose" "%vaSoundDir%\%newDir%\My name is\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose" "%vaSoundDir%\%newDir%\My name is\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\My name is\*" || (
    echo - Missing My name is
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\I am\" mkdir "%vaSoundDir%\%newDir%\I am"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose" "%vaSoundDir%\%newDir%\I am\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\I am\*" || (
    echo - Missing I am
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\You're welcome\" mkdir "%vaSoundDir%\%newDir%\You're welcome"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose" "%vaSoundDir%\%newDir%\You're welcome\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose" "%vaSoundDir%\%newDir%\You're welcome\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\You're welcome\*" || (
    echo - Missing You're welcome
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Acknowledged\" mkdir "%vaSoundDir%\%newDir%\Acknowledged"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" "%vaSoundDir%\%newDir%\Acknowledged\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" "%vaSoundDir%\%newDir%\Acknowledged\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Acknowledged\*" || (
    echo - Missing Acknowledged
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Red Alert\" mkdir "%vaSoundDir%\%newDir%\Red Alert"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert 1))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert 1))\non-verbose" "%vaSoundDir%\%newDir%\Red Alert\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\non-verbose" "%vaSoundDir%\%newDir%\Red Alert\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\Verbose" "%vaSoundDir%\%newDir%\Red Alert\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Red Alert\*" || (
    echo - Missing Red Alert
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Yellow Alert\" mkdir "%vaSoundDir%\%newDir%\Yellow Alert"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose" "%vaSoundDir%\%newDir%\Yellow Alert\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\Verbose" "%vaSoundDir%\%newDir%\Yellow Alert\" > NUL
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Yellow Alert\*" || (
    echo - Missing Yellow Alert
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\" mkdir "%vaSoundDir%\%newDir%\Evasive Maneuvers"
  IF EXIST "%vaSoundDir%\%curDir%\Fighter\Watch our six.mp3" (
    copy "%vaSoundDir%\%curDir%\Fighter\Watch our six.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Watch our six.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Concur))\non-verbose\I concur.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Concur))\non-verbose\I concur.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\I concur.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Engaging evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Engaging evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Engaging evasive manoeuvres.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres alt.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Manoeuvring.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Manoeuvring.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Manoeuvring.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Manoeuvring alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Manoeuvring alt.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Manoeuvring alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Executing evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Executing evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Executing evasive manoeuvres.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Evasive Maneuvers\*" || (
    echo - Missing Evasive Maneuvers
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Cargo\" mkdir "%vaSoundDir%\%newDir%\Display Cargo"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\non-verbose" "%vaSoundDir%\%newDir%\Display Cargo\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\Verbose" "%vaSoundDir%\%newDir%\Display Cargo\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Acknowledged.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Acknowledged.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Acknowledged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Acknowledged 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Acknowledged 2.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Acknowledged 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Our cargo.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Our cargo.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Our cargo.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Cargo.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo alt.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Cargo alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo hold.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Cargo hold.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Cargo hold.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Display Cargo\*" || (
    echo - Missing Display Cargo
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Ship Info\" mkdir "%vaSoundDir%\%newDir%\Display Ship Info"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" "%vaSoundDir%\%newDir%\Display Ship Info\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" "%vaSoundDir%\%newDir%\Display Ship Info\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Ship))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Ship))\non-verbose" "%vaSoundDir%\%newDir%\Display Ship Info\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Our status.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Our status.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Our status.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Planetary landings\Status report.mp3" (
    copy "%vaSoundDir%\%curDir%\Planetary landings\Status report.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Status report.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Status.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Status.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Status.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Display Ship Info\*" || (
    echo - Missing Display Ship Info
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Objectives\" mkdir "%vaSoundDir%\%newDir%\Display Objectives"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" "%vaSoundDir%\%newDir%\Display Objectives\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" "%vaSoundDir%\%newDir%\Display Objectives\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\non-verbos" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\non-verbos" "%vaSoundDir%\%newDir%\Display Objectives\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\Verbose" "%vaSoundDir%\%newDir%\Display Objectives\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Station menus\General Services\Mission Board.mp3" (
    copy "%vaSoundDir%\%curDir%\Station menus\General Services\Mission Board.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Mission Board.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Display Objectives\*" || (
    echo - Missing Display Objectives
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Loading Configuration\" mkdir "%vaSoundDir%\%newDir%\Loading Configuration"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading requested configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading requested configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Loading requested configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Loading requested configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Loading Configuration\*" || (
    echo - Missing Loading Configuration
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Next Target\" mkdir "%vaSoundDir%\%newDir%\Next Target"
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Targeting wise guy.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Targeting wise guy.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting wise guy.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next target.mp3" "%vaSoundDir%\%newDir%\Next Target\Next target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Target next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Target next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Target next hostile.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Targeting next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Targeting next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting next hostile.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Next hostile.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next hostile selected.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next hostile selected.mp3" "%vaSoundDir%\%newDir%\Next Target\Next hostile selected.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Selecting target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Selecting target.mp3" "%vaSoundDir%\%newDir%\Next Target\Selecting target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Switching targets.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Switching targets.mp3" "%vaSoundDir%\%newDir%\Next Target\Switching targets.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Switching targets alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Switching targets alt.mp3" "%vaSoundDir%\%newDir%\Next Target\Switching targets alt.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Next Target\*" || (
    echo - Missing Next Target
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cancel\" mkdir "%vaSoundDir%\%newDir%\Cancel"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Cancel))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Cancel))\non-verbose" "%vaSoundDir%\%newDir%\Cancel\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Cancelled.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Cancelled.mp3" "%vaSoundDir%\%newDir%\Cancel\Cancelled.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Cancel\*" || (
    echo - Missing Cancel
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Sending Message\" mkdir "%vaSoundDir%\%newDir%\Sending Message"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Sending))\non-verbose\Sending message.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Sending))\non-verbose\Sending message.mp3" "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Sending message.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Sending message.mp3" "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Message sent.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Message sent.mp3" "%vaSoundDir%\%newDir%\Sending Message\Message sent.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Sending Message\*" || (
    echo - Missing Sending Message
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Typing\" mkdir "%vaSoundDir%\%newDir%\Typing"
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 0.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 0.mp3" "%vaSoundDir%\%newDir%\Typing\Type 0.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 2.mp3" "%vaSoundDir%\%newDir%\Typing\Type 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 6.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 6.mp3" "%vaSoundDir%\%newDir%\Typing\Type 6.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Typing\*" || (
    echo - Missing Typing
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Screenshot\" mkdir "%vaSoundDir%\%newDir%\Screenshot"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo))\non-verbose\Capturing image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo))\non-verbose\Capturing image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high resolution image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high resolution image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high res image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high res image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high res image.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Capturing image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Capturing image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Recording high res image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Recording high res image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Recording high res image.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Capturing high resolution image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Capturing high resolution image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Screenshot\*" || (
    echo - Missing Screenshot
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Show Map\" mkdir "%vaSoundDir%\%newDir%\Show Map"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose" "%vaSoundDir%\%newDir%\Show Map\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\Verbose" "%vaSoundDir%\%newDir%\Show Map\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Maps\System map.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Maps\System map.mp3" "%vaSoundDir%\%newDir%\Show Map\System map.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Maps\Displaying map.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Maps\Displaying map.mp3" "%vaSoundDir%\%newDir%\Show Map\Displaying map.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Completed.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Completed.mp3" "%vaSoundDir%\%newDir%\Show Map\Completed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Completed 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Completed 2.mp3" "%vaSoundDir%\%newDir%\Show Map\Completed 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Complying.mp3" "%vaSoundDir%\%newDir%\Show Map\Complying.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Show Map\*" || (
    echo - Missing Show Map
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Autopilot Disengaged\" mkdir "%vaSoundDir%\%newDir%\Autopilot Disengaged"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose" "%vaSoundDir%\%newDir%\Autopilot Disengaged\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose" "%vaSoundDir%\%newDir%\Autopilot Disengaged\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto Off))\Verbose\Autopilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto Off))\Verbose\Autopilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Auto pilot disengaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Affirmative.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Affirmative.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Autopilot Disengaged\*" || (
    echo - Missing Autopilot Disengaged
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Autopilot\" mkdir "%vaSoundDir%\%newDir%\Autopilot"
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Affirmative.mp3" "%vaSoundDir%\%newDir%\Autopilot\Affirmative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Confirmed.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Confirmed.mp3" "%vaSoundDir%\%newDir%\Autopilot\Confirmed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto On))\Verbose\Autopilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto On))\Verbose\Autopilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Auto pilot engaged.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Autopilot\*" || (
    echo - Missing Autopilot
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Hyperspace\" mkdir "%vaSoundDir%\%newDir%\Hyperspace"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose" "%vaSoundDir%\%newDir%\Hyperspace\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose" "%vaSoundDir%\%newDir%\Hyperspace\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Power Boost Engines))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Power Boost Engines))\non-verbose" "%vaSoundDir%\%newDir%\Hyperspace\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Power Boost Engines))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Power Boost Engines))\Verbose" "%vaSoundDir%\%newDir%\Hyperspace\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Executing now.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Executing now.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Executing now.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Getting us out of here.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Getting us out of here.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Getting us out of here.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump 2.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump engaging.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump engaging.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump engaging.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Hyperspace\*" || (
    echo - Missing Hyperspace
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cruise Control\" mkdir "%vaSoundDir%\%newDir%\Cruise Control"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Power to engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise enabled.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise enabled.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise enabled.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise engaged.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise engaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode enabled.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode enabled.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode enabled.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode engaged.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode engaged.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Cruise Control\*" || (
    echo - Missing Cruise Control
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\" mkdir "%vaSoundDir%\%newDir%\Propulsion Enhancer"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Boosting engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Boosting engines.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Boosting engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners 2.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners engaged.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners engaged.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Propulsion Enhancer\*" || (
    echo - Missing Propulsion Enhancer
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Max\" mkdir "%vaSoundDir%\%newDir%\Engines Max"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Afterburners maxing engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Afterburners maxing engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\Verbose\All power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\Verbose\All power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\All power to engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines full burn.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines full burn.mp3" "%vaSoundDir%\%newDir%\Engines Max\Engines full burn.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners maxing engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners maxing engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Max engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Max engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Max engines.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Engines Max\*" || (
    echo - Missing Engines Max
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Full Power\" mkdir "%vaSoundDir%\%newDir%\Engines Full Power"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Power to engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Engaging Impulse engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Engaging Impulse engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Engaging Impulse engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Impulse engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Impulse engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Impulse engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\All power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\All power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\All power to engines.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\non-verbose\Powering engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\non-verbose\Powering engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Powering engines.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Engines Full Power\*" || (
    echo - Missing Engines Full Power
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\" mkdir "%vaSoundDir%\%newDir%\Engines Full Stop"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative slowing to a full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative slowing to a full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative slowing to a full engine stop.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative full engine stop.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Acknowledged full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Acknowledged full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Acknowledged full engine stop.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Cutting engines stopping here.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Cutting engines stopping here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cut engines stop here.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cut engines stop here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cut engines stop here.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here alt.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Engines Full Stop\*" || (
    echo - Missing Engines Full Stop
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\General Error\" mkdir "%vaSoundDir%\%newDir%\General Error"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" "%vaSoundDir%\%newDir%\General Error\Error beep.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" "%vaSoundDir%\%newDir%\General Error\Not an option.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\General Error\*" || (
    echo - Missing General Error
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Equip Weapon\" mkdir "%vaSoundDir%\%newDir%\Equip Weapon"
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Next weapon.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Next weapon.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Next weapon.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Weapon changed.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Weapon changed.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Weapon changed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Weapons deployed.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Weapons deployed.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Weapons deployed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons deployed.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons deployed.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Weapons deployed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Deploying weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying and readying weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying and readying weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Deploying and readying weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Arming weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Arming weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Powering weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Powering weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Powering weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Arming weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Arming weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Equip Weapon\*" || (
    echo - Missing Equip Weapon
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\" mkdir "%vaSoundDir%\%newDir%\Configure Weapon Group"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Loading configuration.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Configuring weapon group.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Configuring weapon group.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon group.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Configuring weapon groups.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Configuring weapon groups.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Configuring weapon groups.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Configuring weapon groups.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Configuring weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Configuring weapons.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapons.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Configure Weapon Group\*" || (
    echo - Missing Configure Weapon Group
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Configuration Complete\" mkdir "%vaSoundDir%\%newDir%\Configuration Complete"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Pre Launch 3))\non-verbose\Request complete.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Pre Launch 3))\non-verbose\Request complete.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Request complete.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Configuration has been applied.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Configuration has been applied.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been applied.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Configuration has been changed.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Configuration has been changed.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been changed.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Configuration Complete\*" || (
    echo - Missing Configuration Complete
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Error\" mkdir "%vaSoundDir%\%newDir%\Firing Error"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Nope sorry.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Nope sorry.mp3" "%vaSoundDir%\%newDir%\Firing Error\Nope sorry.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\That's a negative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\That's a negative.mp3" "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\That's a negative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\That's a negative.mp3" "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" "%vaSoundDir%\%newDir%\Firing Error\Not an option.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Error\*" || (
    echo - Missing Firing Error
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cease Fire\" mkdir "%vaSoundDir%\%newDir%\Cease Fire"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Stowing weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Stowing weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Stowing weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting all weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting all weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Weapons Off))\Verbose\Weapons offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Weapons Off))\Verbose\Weapons offline.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retracting armaments.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retracting armaments.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting armaments.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retracting weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retracting weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retract weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retract weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retract weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retracting all weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retracting all weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Weapons retracted.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Weapons retracted.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons retracted.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Weapons offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Weapons offline.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Weapons offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Weapons offline.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Cease Fire\*" || (
    echo - Missing Cease Fire
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group Generic\" mkdir "%vaSoundDir%\%newDir%\Firing Group Generic"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose" "%vaSoundDir%\%newDir%\Firing Group Generic\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose" "%vaSoundDir%\%newDir%\Firing Group Generic\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Weapons hot.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Fire at will.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Fire at will.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Fire at will.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Fighter\Firing volley.mp3" (
    copy "%vaSoundDir%\%curDir%\Fighter\Firing volley.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Firing volley.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Group Generic\*" || (
    echo - Missing Firing Group Generic
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 1\" mkdir "%vaSoundDir%\%newDir%\Firing Group 1"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons hot.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free engage the target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Divert power to weapons happy hunting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt 2.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Group 1\*" || (
    echo - Missing Firing Group 1
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 2\" mkdir "%vaSoundDir%\%newDir%\Firing Group 2"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free engage the target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons hot.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Divert power to weapons happy hunting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 2.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 2 alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 2 alt.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2 alt.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Group 2\*" || (
    echo - Missing Firing Group 2
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 3\" mkdir "%vaSoundDir%\%newDir%\Firing Group 3"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free engage the target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons hot.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Divert power to weapons happy hunting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 3.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 3.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Fire group 3.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Group 3\*" || (
    echo - Missing Firing Group 3
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Ambush\" mkdir "%vaSoundDir%\%newDir%\Firing Ambush"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose" "%vaSoundDir%\%newDir%\Firing Ambush\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Configuring for attack.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Configuring for attack.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Configuring for attack.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Strike Target))\non-verbose\Attacking.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Strike Target))\non-verbose\Attacking.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Attacking.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Previous Hostile))\Verbose\Target locked.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Previous Hostile))\Verbose\Target locked.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Target locked.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Engage the target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engaging target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engaging target.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Engaging target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Course Headings\((RS - CH setting course))\Verbose\Locking target and setting course.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\ED\Course Headings\((RS - CH setting course))\Verbose\Locking target and setting course.mp3" "%vaSoundDir%\%newDir%\Firing Ambush\Locking target and setting course.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Ambush\*" || (
    echo - Missing Firing Ambush
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Counter\" mkdir "%vaSoundDir%\%newDir%\Firing Counter"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasures.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Launching countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Launching countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launching countermeasures.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures alt.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasure.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Counter\*" || (
    echo - Missing Firing Counter
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Missile\" mkdir "%vaSoundDir%\%newDir%\Firing Missile"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Launch Missile))\non-verbose\Firing missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Launch Missile))\non-verbose\Firing missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile away alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile away alt.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile away alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missiles away.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missiles away.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missiles away.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile launch.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile launch.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile launch.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile away.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile away.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile away.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire missile alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire missile alt.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Fire missile alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Fire missile.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Firing missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Firing missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Missile\*" || (
    echo - Missing Firing Missile
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\" mkdir "%vaSoundDir%\%newDir%\Firing Torpedo"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free engage the target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons hot.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Divert power to weapons happy hunting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire torpedoes.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire torpedoes.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Fire torpedoes.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Firing torpedoes.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Firing torpedoes.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Firing torpedoes.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Torpedo\*" || (
    echo - Missing Firing Torpedo
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Mine\" mkdir "%vaSoundDir%\%newDir%\Firing Mine"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Mine\That's affirmative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Complying.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine alt.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine alt.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Mine\*" || (
    echo - Missing Firing Mine
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\" mkdir "%vaSoundDir%\%newDir%\Firing Mining Laser"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\That's affirmative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Complying.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed alt.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed alt.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Mining Laser\*" || (
    echo - Missing Firing Mining Laser
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Beacon\" mkdir "%vaSoundDir%\%newDir%\Firing Beacon"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\SWS\((RS - Beacons))\Verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\SWS\((RS - Beacons))\Verbose" "%vaSoundDir%\%newDir%\Firing Beacon\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\SWS\((RS - Beacons))\non-verbose" (
    Xcopy /E /Y /Q "%vaSoundDir%\%curDir%\Profile Sounds\SWS\((RS - Beacons))\non-verbose" "%vaSoundDir%\%newDir%\Firing Beacon\" > NUL
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\non-verbose\Launch.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\non-verbose\Launch.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Launch.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\Verbose\Launching probe.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\Verbose\Launching probe.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Launching probe.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Wingman\Activating beacon now.mp3" (
    copy "%vaSoundDir%\%curDir%\Wingman\Activating beacon now.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Activating beacon now.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Wingman\Activating the beacon now.mp3" (
    copy "%vaSoundDir%\%curDir%\Wingman\Activating the beacon now.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Activating the beacon now.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Beacon\*" || (
    echo - Missing Firing Beacon
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Escape\" mkdir "%vaSoundDir%\%newDir%\Firing Escape"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Escape\That's affirmative.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Complying.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Retreating.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Retreating.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Retreating.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Protocols\Executing escape protocol.mp3" (
    copy "%vaSoundDir%\%curDir%\Protocols\Executing escape protocol.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Executing escape protocol.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Escape\*" || (
    echo - Missing Firing Escape
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Highlight\" mkdir "%vaSoundDir%\%newDir%\Firing Highlight"
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Locking on to target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Locking on to target.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Locking on to target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Focusing target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Focusing target.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Focusing target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes this one.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes him.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Flare.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Flare.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Flare.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Flare alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Flare alt.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Flare alt.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Targeting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Targeting.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Targeting.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Locking target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Locking target.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Locking target.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focused.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focused.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Target focused.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focus.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focus.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Target focus.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Focus target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Focus target.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Focus target.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Highlight\*" || (
    echo - Missing Firing Highlight
  )
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Hacking\" mkdir "%vaSoundDir%\%newDir%\Firing Hacking"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comm link open.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comm link open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comm link open.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comms open.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comms open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Comms.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Comms.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms.mp3" >nul
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Comms open.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Comms open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3" >nul
  )
  >nul 2>nul dir /a-d "%vaSoundDir%\%newDir%\Firing Hacking\*" || (
    echo - Missing Firing Hacking
  )
  IF EXIST "%vaSoundDir%\sf-i_Null\" (
    IF EXIST "%vaSoundDir%\sf-i_Null\Non-Verbal Confirmation\" Xcopy /E /Y /Q "%vaSoundDir%\sf-i_Null\Non-Verbal Confirmation" "%vaSoundDir%\%newDir%\Non-Verbal Confirmation\" > NUL
  )
  IF %MISSING_COUNT% GTR 0 (
    echo - Missing %MISSING_COUNT% entries
  )
  exit /b



