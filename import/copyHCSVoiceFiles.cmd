@ECHO OFF
SET vaSoundDir=C:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds

SET companionsFound=Null

IF NOT EXIST "%vaSoundDir%" (
  echo "%vaSoundDir%" not found
  pause
  exit
)

cd "%vaSoundDir%"
FOR /D %%G IN (hcspack-*) do call :processDir %%G

SET companionsFound

pause
ECHO ON
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

:processDir
  SET curDir=%1
  SET companionName=%curDir:hcspack-=%
  CALL :ucFirst companionName
  SET newDir=sf-i_%companionName%
  ECHO %curDir% copying to %newDir%
  SET companionsFound=%companionsFound%;%companionName%
  IF NOT EXIST "%vaSoundDir%\%newDir%\" mkdir "%vaSoundDir%\%newDir%"
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Non-Verbal Error\" mkdir "%vaSoundDir%\%newDir%\Non-Verbal Error"
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" "%vaSoundDir%\%newDir%\Non-Verbal Error\Error beep.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Non-Verbal Error\Error beep.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Non-Verbal Error is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\" mkdir "%vaSoundDir%\%newDir%\Switch Companion Target"
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\A smidgen of power.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\A smidgen of power.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\A smidgen of power.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\A smidgen of power.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\I’m number one does this mean I can boss the crew around.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\I’m number one does this mean I can boss the crew around.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\I’m number one does this mean I can boss the crew around.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\I’m number one does this mean I can boss the crew around.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose\Carina Online.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose\Carina Online.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Carina Online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\Carina Online.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Vega online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\Vega online.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" "%vaSoundDir%\%newDir%\Switch Companion Target\Jazz online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Target\Jazz online.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Switch Companion Target is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\" mkdir "%vaSoundDir%\%newDir%\Switch Companion Source"
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\I'm afraid I can't do that Dave Just kidding.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\I'm afraid I can't do that Dave Just kidding.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\I'm afraid I can't do that Dave Just kidding.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\I'm afraid I can't do that Dave Just kidding.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Relinquishing command reluctantly.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Relinquishing command reluctantly.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Relinquishing command reluctantly.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\Relinquishing command reluctantly.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose\Carina Offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose\Carina Offline.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Carina Offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\Carina Offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Vega offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\Vega offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" "%vaSoundDir%\%newDir%\Switch Companion Source\Jazz offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Switch Companion Source\Jazz offline.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Switch Companion Source is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Listening Enabled\" mkdir "%vaSoundDir%\%newDir%\Listening Enabled"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose\Carina Online.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose\Carina Online.mp3" "%vaSoundDir%\%newDir%\Listening Enabled\Carina Online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Enabled\Carina Online.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega online.mp3" "%vaSoundDir%\%newDir%\Listening Enabled\Vega online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Enabled\Vega online.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz online.mp3" "%vaSoundDir%\%newDir%\Listening Enabled\Jazz online.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Enabled\Jazz online.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Listening Enabled is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Listening Disabled\" mkdir "%vaSoundDir%\%newDir%\Listening Disabled"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose\Carina Offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose\Carina Offline.mp3" "%vaSoundDir%\%newDir%\Listening Disabled\Carina Offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Disabled\Carina Offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Vega offline.mp3" "%vaSoundDir%\%newDir%\Listening Disabled\Vega offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Disabled\Vega offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Jazz offline.mp3" "%vaSoundDir%\%newDir%\Listening Disabled\Jazz offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Listening Disabled\Jazz offline.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Listening Disabled is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Hello\" mkdir "%vaSoundDir%\%newDir%\Hello"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" "%vaSoundDir%\%newDir%\Hello\Hello.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hello\Hello.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3" "%vaSoundDir%\%newDir%\Hello\Hello.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hello\Hello.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Greetings.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Greetings.mp3" "%vaSoundDir%\%newDir%\Hello\Greetings.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hello\Greetings.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Hello alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Hello alt.mp3" "%vaSoundDir%\%newDir%\Hello\Hello alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hello\Hello alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Greetings to you too.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Greetings to you too.mp3" "%vaSoundDir%\%newDir%\Hello\Greetings to you too.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hello\Greetings to you too.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Hello is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\My name is\" mkdir "%vaSoundDir%\%newDir%\My name is"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is.mp3" "%vaSoundDir%\%newDir%\My name is\My name is.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My name is.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is Jazz.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is Jazz.mp3" "%vaSoundDir%\%newDir%\My name is\My name is Jazz.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My name is Jazz.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is Carina.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose\My name is Carina.mp3" "%vaSoundDir%\%newDir%\My name is\My name is Carina.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My name is Carina.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\Hello I'm Carina.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\Hello I'm Carina.mp3" "%vaSoundDir%\%newDir%\My name is\Hello I'm Carina.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\Hello I'm Carina.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am Carina.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am Carina.mp3" "%vaSoundDir%\%newDir%\My name is\I am Carina.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\I am Carina.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am known as Carina.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am known as Carina.mp3" "%vaSoundDir%\%newDir%\My name is\I am known as Carina.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\I am known as Carina.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is Carina.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is Carina.mp3" "%vaSoundDir%\%newDir%\My name is\My designation is Carina.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My designation is Carina.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is 2.mp3" "%vaSoundDir%\%newDir%\My name is\My designation is 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My designation is 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\My designation is.mp3" "%vaSoundDir%\%newDir%\My name is\My designation is.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\My designation is.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am known as.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am known as.mp3" "%vaSoundDir%\%newDir%\My name is\I am known as.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\I am known as.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am 2.mp3" "%vaSoundDir%\%newDir%\My name is\I am 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\I am 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose\I am.mp3" "%vaSoundDir%\%newDir%\My name is\I am.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\My name is\I am.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo My name is is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\I am\" mkdir "%vaSoundDir%\%newDir%\I am"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\My name is Jazz I'll be assisting you.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\My name is Jazz I'll be assisting you.mp3" "%vaSoundDir%\%newDir%\I am\My name is Jazz I'll be assisting you.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\I am\My name is Jazz I'll be assisting you.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\I am Vega an artificial intelligence.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\I am Vega an artificial intelligence.mp3" "%vaSoundDir%\%newDir%\I am\I am Vega an artificial intelligence.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\I am\I am Vega an artificial intelligence.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\The most pleasant cockpit voice assistant.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose\The most pleasant cockpit voice assistant.mp3" "%vaSoundDir%\%newDir%\I am\The most pleasant cockpit voice assistant.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\I am\The most pleasant cockpit voice assistant.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo I am is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\You're welcome\" mkdir "%vaSoundDir%\%newDir%\You're welcome"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose\You're welcome.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose\You're welcome.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're welcome.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're welcome.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose\You're welcome 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose\You're welcome 2.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're welcome 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're welcome 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're very welcome.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're very welcome.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're very welcome.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're very welcome.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome of course.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome of course.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're welcome of course.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're welcome of course.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome I suppose.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome I suppose.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're welcome I suppose.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're welcome I suppose.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You are most welcome.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You are most welcome.mp3" "%vaSoundDir%\%newDir%\You're welcome\You are most welcome.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You are most welcome.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose\You're welcome 2.mp3" "%vaSoundDir%\%newDir%\You're welcome\You're welcome 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\You're welcome\You're welcome 2.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo You're welcome is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Acknowledged\" mkdir "%vaSoundDir%\%newDir%\Acknowledged"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Alright then.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Alright then.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then done.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then done.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Alright then done.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Alright then done.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\As you wish.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\As you wish.mp3" "%vaSoundDir%\%newDir%\Acknowledged\As you wish.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\As you wish.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\As you wish 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\As you wish 2.mp3" "%vaSoundDir%\%newDir%\Acknowledged\As you wish 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\As you wish 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Alright then.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Alright then.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Alright then.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Acknowledged 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Acknowledged 2.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Acknowledged 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Acknowledged 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Affirmative 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Affirmative 2.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Affirmative 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Affirmative 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Certainly.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Certainly.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Certainly.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Certainly.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Complying.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Complying.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Making it so.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Making it so.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Making it so.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Making it so.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\No problem.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\No problem.mp3" "%vaSoundDir%\%newDir%\Acknowledged\No problem.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\No problem.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Okay 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Okay 2.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Okay 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Okay 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Acknowledged\That's affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\That's affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Understood.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Understood.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Understood.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Understood.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Understood 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Understood 2.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Understood 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Understood 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Okay.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Okay.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Okay.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Okay.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Affirmative.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Acknowledged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Acknowledged.mp3" "%vaSoundDir%\%newDir%\Acknowledged\Acknowledged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Acknowledged\Acknowledged.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Acknowledged is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Red Alert\" mkdir "%vaSoundDir%\%newDir%\Red Alert"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert 1))\non-verbose\Red alert.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert 1))\non-verbose\Red alert.mp3" "%vaSoundDir%\%newDir%\Red Alert\Red alert.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Red Alert\Red alert.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\non-verbose\Red alert.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\non-verbose\Red alert.mp3" "%vaSoundDir%\%newDir%\Red Alert\Red alert.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Red Alert\Red alert.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\Verbose\Red alert confirmed.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Red Alert))\Verbose\Red alert confirmed.mp3" "%vaSoundDir%\%newDir%\Red Alert\Red alert confirmed.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Red Alert\Red alert confirmed.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Red Alert is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Yellow Alert\" mkdir "%vaSoundDir%\%newDir%\Yellow Alert"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose\Yellow alert.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose\Yellow alert.mp3" "%vaSoundDir%\%newDir%\Yellow Alert\Yellow alert.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Yellow Alert\Yellow alert.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose\Yellow alert 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose\Yellow alert 2.mp3" "%vaSoundDir%\%newDir%\Yellow Alert\Yellow alert 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Yellow Alert\Yellow alert 2.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Yellow Alert is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\" mkdir "%vaSoundDir%\%newDir%\Evasive Maneuvers"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Concur))\non-verbose\I concur.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Concur))\non-verbose\I concur.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\I concur.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\I concur.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Engaging evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Engaging evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Engaging evasive manoeuvres.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\Engaging evasive manoeuvres.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres alt.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\Evasive manoeuvres.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Executing evasive manoeuvres.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Executing evasive manoeuvres.mp3" "%vaSoundDir%\%newDir%\Evasive Maneuvers\Executing evasive manoeuvres.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Evasive Maneuvers\Executing evasive manoeuvres.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Evasive Maneuvers is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Cargo\" mkdir "%vaSoundDir%\%newDir%\Display Cargo"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\non-verbose\Cargo hold.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Cargo hold))\non-verbose\Cargo hold.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Cargo hold.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Cargo\Cargo hold.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Our cargo.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Our cargo.mp3" "%vaSoundDir%\%newDir%\Display Cargo\Our cargo.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Cargo\Our cargo.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Display Cargo is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Ship Info\" mkdir "%vaSoundDir%\%newDir%\Display Ship Info"
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Our status.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Our status.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Our status.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Ship Info\Our status.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Planetary landings\Status report.mp3" (
    copy "%vaSoundDir%\%curDir%\Planetary landings\Status report.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Status report.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Ship Info\Status report.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Status.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Panels\Status.mp3" "%vaSoundDir%\%newDir%\Display Ship Info\Status.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Ship Info\Status.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Display Ship Info is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Display Objectives\" mkdir "%vaSoundDir%\%newDir%\Display Objectives"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Affirmative.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Objectives\Affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Acknowledged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose\Acknowledged.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Acknowledged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Objectives\Acknowledged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Complying.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Objectives\Complying.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\non-verbose\Missions.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\ED\Panels\((RS - Missions))\non-verbose\Missions.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Missions.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Objectives\Missions.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Station menus\General Services\Mission Board.mp3" (
    copy "%vaSoundDir%\%curDir%\Station menus\General Services\Mission Board.mp3" "%vaSoundDir%\%newDir%\Display Objectives\Mission Board.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Display Objectives\Mission Board.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Display Objectives is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Loading Configuration\" mkdir "%vaSoundDir%\%newDir%\Loading Configuration"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading requested configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading requested configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Loading Configuration\Loading configuration.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Loading requested configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Loading requested configuration.mp3" "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Loading Configuration\Loading requested configuration.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Loading Configuration is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Next Target\" mkdir "%vaSoundDir%\%newDir%\Next Target"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Target next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Target next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Target next hostile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Target next hostile.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting now nobody likes him.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Targeting now nobody likes him.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting now nobody likes this one.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Targeting now nobody likes this one.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\non-verbose\Targeting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\non-verbose\Targeting.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Targeting.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next target.mp3" "%vaSoundDir%\%newDir%\Next Target\Next target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Next target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Targeting next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Targeting next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Targeting next hostile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Targeting next hostile.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next hostile.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next hostile.mp3" "%vaSoundDir%\%newDir%\Next Target\Next hostile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Next hostile.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Next hostile selected.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Next hostile selected.mp3" "%vaSoundDir%\%newDir%\Next Target\Next hostile selected.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Next hostile selected.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Selecting target.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Selecting target.mp3" "%vaSoundDir%\%newDir%\Next Target\Selecting target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Selecting target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Switching targets.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Switching targets.mp3" "%vaSoundDir%\%newDir%\Next Target\Switching targets.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Switching targets.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Targeting\Switching targets alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Targeting\Switching targets alt.mp3" "%vaSoundDir%\%newDir%\Next Target\Switching targets alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Next Target\Switching targets alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Next Target is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cancel\" mkdir "%vaSoundDir%\%newDir%\Cancel"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Cancel))\non-verbose\Cancelled.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Cancel))\non-verbose\Cancelled.mp3" "%vaSoundDir%\%newDir%\Cancel\Cancelled.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cancel\Cancelled.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Cancelled.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Cancelled.mp3" "%vaSoundDir%\%newDir%\Cancel\Cancelled.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cancel\Cancelled.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Cancel is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Sending Message\" mkdir "%vaSoundDir%\%newDir%\Sending Message"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Sending))\non-verbose\Sending message.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Sending))\non-verbose\Sending message.mp3" "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Sending message.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Sending message.mp3" "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Sending Message\Sending message.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Message sent.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Message sent.mp3" "%vaSoundDir%\%newDir%\Sending Message\Message sent.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Sending Message\Message sent.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Sending Message is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Typing\" mkdir "%vaSoundDir%\%newDir%\Typing"
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 0.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 0.mp3" "%vaSoundDir%\%newDir%\Typing\Type 0.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Typing\Type 0.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 2.mp3" "%vaSoundDir%\%newDir%\Typing\Type 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Typing\Type 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Typing\Type 6.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Typing\Type 6.mp3" "%vaSoundDir%\%newDir%\Typing\Type 6.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Typing\Type 6.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Typing is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Screenshot\" mkdir "%vaSoundDir%\%newDir%\Screenshot"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo))\non-verbose\Capturing image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo))\non-verbose\Capturing image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high resolution image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high resolution image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high res image.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high res image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high res image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Capturing high res image.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Capturing image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Capturing image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Capturing image.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Recording high res image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Recording high res image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Recording high res image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Recording high res image.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Applications\Capturing high resolution image.mp3" (
    copy "%vaSoundDir%\%curDir%\Applications\Capturing high resolution image.mp3" "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Screenshot\Capturing high resolution image.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Screenshot is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Show Map\" mkdir "%vaSoundDir%\%newDir%\Show Map"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose\Displaying map.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose\Displaying map.mp3" "%vaSoundDir%\%newDir%\Show Map\Displaying map.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Show Map\Displaying map.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose\Opening map.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose\Opening map.mp3" "%vaSoundDir%\%newDir%\Show Map\Opening map.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Show Map\Opening map.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Maps\System map.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Maps\System map.mp3" "%vaSoundDir%\%newDir%\Show Map\System map.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Show Map\System map.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Maps\Displaying map.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Maps\Displaying map.mp3" "%vaSoundDir%\%newDir%\Show Map\Displaying map.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Show Map\Displaying map.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Show Map is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Autopilot Disengaged\" mkdir "%vaSoundDir%\%newDir%\Autopilot Disengaged"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto Off))\Verbose\Autopilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto Off))\Verbose\Autopilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot Disengaged\Autopilot disengaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot disengaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot disengaged.mp3" "%vaSoundDir%\%newDir%\Autopilot Disengaged\Auto pilot disengaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot Disengaged\Auto pilot disengaged.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Autopilot Disengaged is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Autopilot\" mkdir "%vaSoundDir%\%newDir%\Autopilot"
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto On))\Verbose\Autopilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto On))\Verbose\Autopilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Autopilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Autopilot engaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Auto pilot engaged.mp3" "%vaSoundDir%\%newDir%\Autopilot\Auto pilot engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Autopilot\Auto pilot engaged.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Autopilot is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Hyperspace\" mkdir "%vaSoundDir%\%newDir%\Hyperspace"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose\Engaging jump drive.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose\Engaging jump drive.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Engaging jump drive.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Engaging jump drive.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose\Hyperspace jump engaging.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose\Hyperspace jump engaging.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump engaging.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump engaging.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump 2.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump engaging.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Hyperspace jump engaging.mp3" "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump engaging.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Hyperspace\Hyperspace jump engaging.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Hyperspace is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cruise Control\" mkdir "%vaSoundDir%\%newDir%\Cruise Control"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Power to engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cruise Control\Power to engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise enabled.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise enabled.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise enabled.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cruise Control\Cruise enabled.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise engaged.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cruise Control\Cruise engaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode enabled.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode enabled.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode enabled.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode enabled.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Drives\Cruise mode engaged.mp3" "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cruise Control\Cruise mode engaged.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Cruise Control is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\" mkdir "%vaSoundDir%\%newDir%\Propulsion Enhancer"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Boosting engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Boosting engines.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Boosting engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\Boosting engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners 2.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners engaged.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners engaged.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners engaged.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners engaged.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners.mp3" "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Propulsion Enhancer\Afterburners.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Propulsion Enhancer is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Max\" mkdir "%vaSoundDir%\%newDir%\Engines Max"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Afterburners maxing engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Afterburners maxing engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\Verbose\All power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\Verbose\All power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\All power to engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Max\All power to engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines full burn.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines full burn.mp3" "%vaSoundDir%\%newDir%\Engines Max\Engines full burn.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Max\Engines full burn.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners maxing engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Thrusters\Afterburners maxing engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Max\Afterburners maxing engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Max engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Max engines.mp3" "%vaSoundDir%\%newDir%\Engines Max\Max engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Max\Max engines.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Engines Max is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Full Power\" mkdir "%vaSoundDir%\%newDir%\Engines Full Power"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Power to engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Power\Power to engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Engaging Impulse engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Engaging Impulse engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Engaging Impulse engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Power\Engaging Impulse engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Impulse engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Impulse engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Impulse engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Power\Impulse engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\All power to engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\All power to engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\All power to engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Power\All power to engines.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\non-verbose\Powering engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Engines))\non-verbose\Powering engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Power\Powering engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Power\Powering engines.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Engines Full Power is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\" mkdir "%vaSoundDir%\%newDir%\Engines Full Stop"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative slowing to a full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative slowing to a full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative slowing to a full engine stop.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative slowing to a full engine stop.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Affirmative full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative full engine stop.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Affirmative full engine stop.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Acknowledged full engine stop.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Acknowledged full engine stop.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Acknowledged full engine stop.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Acknowledged full engine stop.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Cutting engines stopping here.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose\Cutting engines stopping here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cut engines stop here.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cut engines stop here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cut engines stop here.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Cut engines stop here.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here alt.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines stopping here.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines stopping here.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines.mp3" (
    copy "%vaSoundDir%\%curDir%\Engines Thrusters and Drives\Engines\Cutting engines.mp3" "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Engines Full Stop\Cutting engines.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Engines Full Stop is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\General Error\" mkdir "%vaSoundDir%\%newDir%\General Error"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" (
    copy "%vaSoundDir%\%curDir%\Effects\Error beep\Error beep.mp3" "%vaSoundDir%\%newDir%\General Error\Error beep.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\General Error\Error beep.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\General Error\Unable to comply.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" "%vaSoundDir%\%newDir%\General Error\Not an option.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\General Error\Not an option.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo General Error is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Equip Weapon\" mkdir "%vaSoundDir%\%newDir%\Equip Weapon"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons deployed.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons deployed.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Weapons deployed.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Weapons deployed.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Deploying weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Deploying weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying and readying weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Deploying and readying weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Deploying and readying weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Deploying and readying weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Arming weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Arming weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Powering weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Powering weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Powering weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Powering weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Arming weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Arming weapons.mp3" "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Equip Weapon\Arming weapons.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Equip Weapon is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\" mkdir "%vaSoundDir%\%newDir%\Configure Weapon Group"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Loading configuration.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\Loading configuration.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Configuring weapon group.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Configuring weapon group.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon group.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon group.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Configuring weapon groups.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Configuring weapon groups.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Configuring weapon groups.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Configuring weapon groups.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapon groups.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Configuring weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Configuring weapons.mp3" "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configure Weapon Group\Configuring weapons.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Configure Weapon Group is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Configuration Complete\" mkdir "%vaSoundDir%\%newDir%\Configuration Complete"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Pre Launch 3))\non-verbose\Request complete.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Take Off-Docking\((RS - Pre Launch 3))\non-verbose\Request complete.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Request complete.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configuration Complete\Request complete.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Configuration has been applied.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Configuration has been applied.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been applied.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been applied.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Role\Configuration has been changed.mp3" (
    copy "%vaSoundDir%\%curDir%\Role\Configuration has been changed.mp3" "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been changed.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Configuration Complete\Configuration has been changed.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Configuration Complete is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Error\" mkdir "%vaSoundDir%\%newDir%\Firing Error"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Nope sorry.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Nope sorry.mp3" "%vaSoundDir%\%newDir%\Firing Error\Nope sorry.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\Nope sorry.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\That's a negative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\That's a negative.mp3" "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Acknowledgements\Unable to comply.mp3" "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3" "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\Unable to comply.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\That's a negative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\That's a negative.mp3" "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\That's a negative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3" "%vaSoundDir%\%newDir%\Firing Error\Not an option.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Error\Not an option.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Error is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Cease Fire\" mkdir "%vaSoundDir%\%newDir%\Cease Fire"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Stowing weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Stowing weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Stowing weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Stowing weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting all weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting all weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Weapons Off))\Verbose\Weapons offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Weapons Off))\Verbose\Weapons offline.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retracting weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retracting weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Retracting weapons.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Power Management\Weapons offline.mp3" (
    copy "%vaSoundDir%\%curDir%\Power Management\Weapons offline.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Weapons offline.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Retracting all weapons.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Retracting all weapons.mp3" "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Cease Fire\Retracting all weapons.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Cease Fire is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group Generic\" mkdir "%vaSoundDir%\%newDir%\Firing Group Generic"
  IF EXIST "%vaSoundDir%\%curDir%\Fighter\Firing volley.mp3" (
    copy "%vaSoundDir%\%curDir%\Fighter\Firing volley.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Firing volley.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group Generic\Firing volley.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Weapons hot.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group Generic\Weapons hot.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group Generic\Weapons free.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group Generic\Weapons free.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Group Generic is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 1\" mkdir "%vaSoundDir%\%newDir%\Firing Group 1"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons hot.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Weapons hot.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free engage the target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free engage the target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Weapons free.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Divert power to weapons happy hunting.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Divert power to weapons happy hunting.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt 2.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 1 alt.mp3" "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 1\Fire group 1 alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Group 1 is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 2\" mkdir "%vaSoundDir%\%newDir%\Firing Group 2"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free engage the target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free engage the target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Weapons free.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Weapons hot.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Weapons hot.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Divert power to weapons happy hunting.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Divert power to weapons happy hunting.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 2.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 2.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 2 alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 2 alt.mp3" "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2 alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 2\Fire group 2 alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Group 2 is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Group 3\" mkdir "%vaSoundDir%\%newDir%\Firing Group 3"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free engage the target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free engage the target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 3\Weapons free.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Weapons hot.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 3\Weapons hot.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Divert power to weapons happy hunting.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 3\Divert power to weapons happy hunting.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire group 3.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire group 3.mp3" "%vaSoundDir%\%newDir%\Firing Group 3\Fire group 3.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Group 3\Fire group 3.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Group 3 is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Counter\" mkdir "%vaSoundDir%\%newDir%\Firing Counter"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasures.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasures.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Launching countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Launching countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launching countermeasures.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Launching countermeasures.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Deploying countermeasure.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures alt.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasures.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Countermeasures.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Countermeasures.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Countermeasures.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Launch countermeasure.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Launch countermeasure.mp3" "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasure.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Counter\Launch countermeasure.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Counter is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Missile\" mkdir "%vaSoundDir%\%newDir%\Firing Missile"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Launch Missile))\non-verbose\Firing missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Launch Missile))\non-verbose\Firing missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile away alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile away alt.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile away alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Missile away alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missiles away.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missiles away.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missiles away.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Missiles away.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile launch.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile launch.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile launch.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Missile launch.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Missile away.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Missile away.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Missile away.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Missile away.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire missile alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire missile alt.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Fire missile alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Fire missile alt.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Fire missile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Fire missile.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Firing missile.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Firing missile.mp3" "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Missile\Firing missile.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Missile is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\" mkdir "%vaSoundDir%\%newDir%\Firing Torpedo"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free engage the target.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free engage the target.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons free.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons hot.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Weapons hot.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Divert power to weapons happy hunting.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Divert power to weapons happy hunting.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Fire torpedoes.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Fire torpedoes.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Fire torpedoes.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Fire torpedoes.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Firing torpedoes.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Firing torpedoes.mp3" "%vaSoundDir%\%newDir%\Firing Torpedo\Firing torpedoes.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Torpedo\Firing torpedoes.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Torpedo is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Mine\" mkdir "%vaSoundDir%\%newDir%\Firing Mine"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Mine\That's affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mine\That's affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Complying.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mine\Complying.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Deploying proximity mine alt.mp3" "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mine\Deploying proximity mine alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Mine is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\" mkdir "%vaSoundDir%\%newDir%\Firing Mining Laser"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\That's affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\That's affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Complying.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\Complying.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Systems and Displays\Mining laser deployed alt.mp3" "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Mining Laser\Mining laser deployed alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Mining Laser is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Beacon\" mkdir "%vaSoundDir%\%newDir%\Firing Beacon"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\non-verbose\Launch.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\non-verbose\Launch.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Launch.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Beacon\Launch.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\Verbose\Launching probe.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\Verbose\Launching probe.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Launching probe.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Beacon\Launching probe.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Wingman\Activating beacon now.mp3" (
    copy "%vaSoundDir%\%curDir%\Wingman\Activating beacon now.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Activating beacon now.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Beacon\Activating beacon now.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Wingman\Activating the beacon now.mp3" (
    copy "%vaSoundDir%\%curDir%\Wingman\Activating the beacon now.mp3" "%vaSoundDir%\%newDir%\Firing Beacon\Activating the beacon now.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Beacon\Activating the beacon now.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Beacon is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Escape\" mkdir "%vaSoundDir%\%newDir%\Firing Escape"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3" "%vaSoundDir%\%newDir%\Firing Escape\That's affirmative.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Escape\That's affirmative.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Complying.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Escape\Complying.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Configuration Commands\Retreating.mp3" (
    copy "%vaSoundDir%\%curDir%\Configuration Commands\Retreating.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Retreating.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Escape\Retreating.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Protocols\Executing escape protocol.mp3" (
    copy "%vaSoundDir%\%curDir%\Protocols\Executing escape protocol.mp3" "%vaSoundDir%\%newDir%\Firing Escape\Executing escape protocol.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Escape\Executing escape protocol.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Escape is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Highlight\" mkdir "%vaSoundDir%\%newDir%\Firing Highlight"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes this one.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes this one.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes him.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Highlight\Targeting now nobody likes him.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Flare.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Flare.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Flare.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Highlight\Flare.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Weapons\Flare alt.mp3" (
    copy "%vaSoundDir%\%curDir%\Weapons\Flare alt.mp3" "%vaSoundDir%\%newDir%\Firing Highlight\Flare alt.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Highlight\Flare alt.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Highlight is empty.
  )
  SET filesFound=0
  IF NOT EXIST "%vaSoundDir%\%newDir%\Firing Hacking\" mkdir "%vaSoundDir%\%newDir%\Firing Hacking"
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comm link open.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comm link open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comm link open.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Hacking\Comm link open.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comms open.mp3" (
    copy "%vaSoundDir%\%curDir%\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comms open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Comms.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Comms.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Hacking\Comms.mp3"  SET filesFound=1
  )
  IF EXIST "%vaSoundDir%\%curDir%\Communications\Comms open.mp3" (
    copy "%vaSoundDir%\%curDir%\Communications\Comms open.mp3" "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3" >nul
    IF EXIST "%vaSoundDir%\%newDir%\Firing Hacking\Comms open.mp3"  SET filesFound=1
  )
  IF "%filesFound%"=="0" (
    echo Firing Hacking is empty.
  )
  exit /b



