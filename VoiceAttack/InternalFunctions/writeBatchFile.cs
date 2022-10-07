using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class VAInline
{

	private string getSavePath(bool includeFilename = true) {
		string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		path = Path.Combine(path, "VoiceAttack SF-I Companion");

		try {
			Directory.CreateDirectory(path);
			if (includeFilename)  path = Path.Combine(path, "copyHCSVoiceFiles.cmd");
		} catch (Exception e) {
			path = null;
		}

		return path;
	}

	private bool writeCopyVoiceFilesContentsToFile(string fileContents) {
		bool rtnVal = false;
		string filePath = getSavePath(true);

		if (!string.IsNullOrEmpty(filePath)) {
			try {
				System.IO.File.WriteAllText(filePath, fileContents);
				rtnVal = true;
			} catch (Exception e) {}
		}

		return rtnVal;
	}

	private bool writeSoundFileGroupContentsToFile(string fileContents) {
		bool rtnVal = false;
		string filePath = getSavePath(false);

		if (!string.IsNullOrEmpty(filePath)) {
			filePath = Path.Combine(filePath, "setSoundFileGroupInfo_func.cs");
			try {
				System.IO.File.WriteAllText(filePath, fileContents);
				rtnVal = true;
			} catch (Exception e) {}
		}

		return rtnVal;
	}



	public void main()
	{
		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
		bool verbose = true;

		string[] fileList;
		string[] dirList;
		string[] backupDirList;
		Dictionary<string, string[]> soundFileGroupList = new Dictionary<string, string[]>();
		Dictionary<string, string[]> soundDirGroupList = new Dictionary<string, string[]>();
		Dictionary<string, string[]> soundBackupDirGroupList = new Dictionary<string, string[]>();
		List<string> groupsToCopyFromNull = new List<string>();

		string batchFileContents = "";
		string soundFileGroupContents = "";
		string sourceFilePath, destFileName, sourceDirPath;

		Dictionary<string, string> soundGroupSynonymList = new Dictionary<string, string>();
		soundGroupSynonymList.Add("Firing Countermeasure", "Firing Counter");  //*** Firing Counter must be defined below
		soundGroupSynonymList.Add("Firing Flare", "Firing Highlight");  //*** Firing Highlight must be defined below
		soundGroupSynonymList.Add("Firing Hack", "Firing Hacking");  //*** Firing Hacking must be defined below


		//*** Non-Verbal Sounds
		fileList = new string[] {
			@"{TXT:>>voiceDir}\Effects\Error beep\Error beep.mp3"
		};
		soundFileGroupList.Add("Non-Verbal Error", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Effects\Alerts\Beep.mp3"
		};
		soundFileGroupList.Add("Non-Verbal Confirmation", fileList);
		groupsToCopyFromNull.Add("Non-Verbal Confirmation");


		//*** Companion
		fileList = new string[] {
			@"{TXT:>>voiceDir}\Additional dialogue\Today is a good day to die short.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\A smidgen of power.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\I’m number one does this mean I can boss the crew around.mp3",
			@"{TXT:>>voiceDir}\Applications\Vega online.mp3",
			@"{TXT:>>voiceDir}\Applications\Jazz online.mp3",
			@"{TXT:>>voiceDir}\Crew commands\Reporting.mp3",
			@"{TXT:>>voiceDir}\Crew commands\Reporting for duty.mp3",
			@"{TXT:>>voiceDir}\Crew commands\Reporting for duty 2.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose"
		};
		soundFileGroupList.Add("Switch Companion Target", fileList);
		soundDirGroupList.Add("Switch Companion Target", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Additional dialogue\I'm afraid I can't do that Dave Just kidding.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Relinquishing command reluctantly.mp3",
			@"{TXT:>>voiceDir}\Crew commands\Relinquishing command.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Additional dialogue extra\That's a wrap.mp3",
			@"{TXT:>>voiceDir}\Applications\Vega offline.mp3",
			@"{TXT:>>voiceDir}\Applications\Jazz offline.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose"
		};
		soundFileGroupList.Add("Switch Companion Source", fileList);
		soundDirGroupList.Add("Switch Companion Source", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Applications\Vega online.mp3",
			@"{TXT:>>voiceDir}\Applications\Jazz online.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Profile\((RS - Voice On 1))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Profile\((RS - Voice On 1))\Verbose"
		};
		soundFileGroupList.Add("Listening Enabled", fileList);
		soundDirGroupList.Add("Listening Enabled", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Applications\Vega offline.mp3",
			@"{TXT:>>voiceDir}\Applications\Jazz offline.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Profile\((RS - Voice Off))\Verbose"
		};
		soundFileGroupList.Add("Listening Disabled", fileList);
		soundDirGroupList.Add("Listening Disabled", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Targeting\Wise guys detected.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\Dialogue 1\Tell me are you whining.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\Dialogue 1\Your discontent.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\Dialogue 1\Your discontent.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\Dialogue 2\You are becoming impatient.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\You need to relax my friend.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\You need to relax.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Additional dialogue extra\Was that French or something.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Additional dialogue extra\Was that French for something.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Additional dialogue extra\What was that I didn't hear you.mp3",
			@"{TXT:>>voiceDir}\Role\Somebody should teach you some manners.mp3",
			@"{TXT:>>voiceDir}\Role\Doing my best.mp3",
			@"{TXT:>>voiceDir}\Role\fine.mp3",
			@"{TXT:>>voiceDir}\Role\Fine I won't disturb you again.mp3",
			@"{TXT:>>voiceDir}\Role\Losing your temper with things.mp3",
			@"{TXT:>>voiceDir}\Role\Losing your temper with things alt.mp3",
			@"{TXT:>>voiceDir}\Role\How rude.mp3",
			@"{TXT:>>voiceDir}\Role\Please grow up.mp3",
			@"{TXT:>>voiceDir}\Role\Please grow up 2.mp3",
			@"{TXT:>>voiceDir}\Role\I'm not laughing.mp3",
			@"{TXT:>>voiceDir}\Role\I'm not laughing 2.mp3",
			@"{TXT:>>voiceDir}\Role\I'm not sure what to say to that.mp3",
			@"{TXT:>>voiceDir}\Role\I'm not sure what to say to that 2.mp3",
			@"{TXT:>>voiceDir}\Role\Oh do grow up.mp3",
			@"{TXT:>>voiceDir}\Role\Oh grow up.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\Feel like to be folded in half.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Sorry what did you say I was watching the football.mp3"
		};
		soundFileGroupList.Add("Annoyed Response", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Hello.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose\Greetings.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Hello alt.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose\Greetings to you too.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Hello))\Verbose"
		};
		soundFileGroupList.Add("Hello", fileList);
		soundDirGroupList.Add("Hello", dirList);

		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - My name))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - My name))\Verbose"
		};
		soundFileGroupList.Add("My name is", fileList);
		soundDirGroupList.Add("My name is", dirList);

		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Introduction))\non-verbose"
		};
		soundFileGroupList.Add("I am", fileList);
		soundDirGroupList.Add("I am", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Role\You're welcome.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - You are welcome))\Verbose"
		};
		soundFileGroupList.Add("You're welcome", fileList);
		soundDirGroupList.Add("You're welcome", dirList);


		//*** Random Facts
		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Handover))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Handover))\Verbose"
		};
		// backupDirList = new string[] {
		// };
		soundFileGroupList.Add("Handover", fileList);
		soundDirGroupList.Add("Handover", dirList);
		// soundBackupDirGroupList.Add("Handover", dirList);

		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Constellations"
		};
		soundFileGroupList.Add("Constellations", fileList);
		soundDirGroupList.Add("Constellations", dirList);

		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Quantum Theory"
		};
		soundFileGroupList.Add("Quantum Theory", fileList);
		soundDirGroupList.Add("Quantum Theory", dirList);

		fileList = new string[] {
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Galaxapedia"
		};
		soundFileGroupList.Add("Galaxapedia", fileList);
		soundDirGroupList.Add("Galaxapedia", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode on))\Verbose"
		};
		soundFileGroupList.Add("Enable Constellations", fileList);
		soundDirGroupList.Add("Enable Constellations", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode on))\Verbose"
		};
		soundFileGroupList.Add("Enable Quantum Theory", fileList);
		soundDirGroupList.Add("Enable Quantum Theory", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode on))\Verbose"
		};
		soundFileGroupList.Add("Enable Galaxapedia", fileList);
		soundDirGroupList.Add("Enable Galaxapedia", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode on))\Verbose"
		};
		soundFileGroupList.Add("Enable Codex", fileList);
		soundDirGroupList.Add("Enable Codex", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode off))\Verbose"
		};
		soundFileGroupList.Add("Disable Constellations", fileList);
		soundDirGroupList.Add("Disable Constellations", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode off))\Verbose"
		};
		soundFileGroupList.Add("Disable Quantum Theory", fileList);
		soundDirGroupList.Add("Disable Quantum Theory", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode off))\Verbose"
		};
		soundFileGroupList.Add("Disable Galaxapedia", fileList);
		soundDirGroupList.Add("Disable Galaxapedia", dirList);

		fileList = new string[] {};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Extra Content\((RS - Interaction mode off))\Verbose"
		};
		soundFileGroupList.Add("Disable Codex", fileList);
		soundDirGroupList.Add("Disable Codex", dirList);


		//*** Somewhat Related To Random Facts - Scanning Codex Objects
		fileList = new string[] {
			@"{TXT:>>voiceDir}\Sensors Radar\Scanning now.mp3",
			@"{TXT:>>voiceDir}\Sensors Radar\Scanning for information.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - System Scan))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose"
		};
		soundFileGroupList.Add("Scanning", fileList);
		soundDirGroupList.Add("Scanning", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Sensors Radar\Scanning planet.mp3",
			@"{TXT:>>voiceDir}\Sensors Radar\Scanning planet alt.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Target Scan Planet))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Target Scan Planet))\Verbose"
		};
		backupDirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose"
		};
		soundFileGroupList.Add("Scanning Planet", fileList);
		soundDirGroupList.Add("Scanning Planet", dirList);
		soundBackupDirGroupList.Add("Scanning Planet", backupDirList);




		//*** Non-Verbal Sounds
		fileList = new string[] {
		};
		soundFileGroupList.Add("Songs", fileList);
		groupsToCopyFromNull.Add("Songs");


		//*** General Sounds
		fileList = new string[] {
			@"{TXT:>>voiceDir}\Role\Obliging.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Accepted.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Acknowledged.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Acknowledged 2.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Certainly.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Complete.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Completed.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Complying.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose"
		};
		soundFileGroupList.Add("Acknowledged", fileList);
		soundDirGroupList.Add("Acknowledged", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Role\Red alert.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Red Alert 1))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Red Alert))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Red Alert))\Verbose"
		};
		soundFileGroupList.Add("Red Alert", fileList);
		soundDirGroupList.Add("Red Alert", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Role\Yellow alert.mp3",
			@"{TXT:>>voiceDir}\Role\Yellow alert 2.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Yellow Alert))\Verbose"
		};
		soundFileGroupList.Add("Yellow Alert", fileList);
		soundDirGroupList.Add("Yellow Alert", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Fighter\Watch our six.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Concur))\non-verbose\I concur.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Exit route engaged.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Engaging evasive manoeuvres.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Evasive manoeuvres alt.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Evasive manoeuvres.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Manoeuvring.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Manoeuvring alt.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Executing evasive manoeuvres.mp3"
		};
		soundFileGroupList.Add("Evasive Maneuvers", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Acknowledgements\Acknowledged.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Acknowledged 2.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Our cargo.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Panels\Cargo.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Panels\Cargo alt.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Panels\Cargo hold.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Panels\((RS - Cargo hold))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Panels\((RS - Cargo hold))\Verbose"
		};
		soundFileGroupList.Add("Display Cargo", fileList);
		soundDirGroupList.Add("Display Cargo", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Additional dialogue\Our status.mp3",
			@"{TXT:>>voiceDir}\Planetary landings\Status report.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Panels\Status.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Panels\((RS - Ship))\non-verbose"
		};
		soundFileGroupList.Add("Display Ship Info", fileList);
		soundDirGroupList.Add("Display Ship Info", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Station menus\General Services\Mission Board.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Panels\((RS - Missions))\non-verbos",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Panels\((RS - Missions))\Verbose"
		};
		soundFileGroupList.Add("Display Objectives", fileList);
		soundDirGroupList.Add("Display Objectives", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading requested configuration.mp3",
			@"{TXT:>>voiceDir}\Power Management\Loading configuration.mp3",
			@"{TXT:>>voiceDir}\Power Management\Loading requested configuration.mp3"
		};
		soundFileGroupList.Add("Loading Configuration", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Targeting\Targeting wise guy.mp3",
			@"{TXT:>>voiceDir}\Targeting\Next target.mp3",
			@"{TXT:>>voiceDir}\Targeting\Target next hostile.mp3",
			@"{TXT:>>voiceDir}\Targeting\Targeting next hostile.mp3",
			@"{TXT:>>voiceDir}\Targeting\Next hostile.mp3",
			@"{TXT:>>voiceDir}\Targeting\Next hostile selected.mp3",
			@"{TXT:>>voiceDir}\Targeting\Selecting target.mp3",
			@"{TXT:>>voiceDir}\Targeting\Switching targets.mp3",
			@"{TXT:>>voiceDir}\Targeting\Switching targets alt.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\non-verbose"
		};
		soundFileGroupList.Add("Next Target", fileList);
		soundDirGroupList.Add("Next Target", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Acknowledgements\Cancelled.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Cancel))\non-verbose"
		};
		soundFileGroupList.Add("Cancel", fileList);
		soundDirGroupList.Add("Cancel", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Comms\((RS - Sending))\non-verbose\Sending message.mp3",
			@"{TXT:>>voiceDir}\Communications\Sending message.mp3",
			@"{TXT:>>voiceDir}\Communications\Message sent.mp3"
		};
		soundFileGroupList.Add("Sending Message", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Effects\Typing\Type 0.mp3",
			@"{TXT:>>voiceDir}\Effects\Typing\Type 2.mp3",
			@"{TXT:>>voiceDir}\Effects\Typing\Type 6.mp3"
		};
		soundFileGroupList.Add("Typing", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Photo))\non-verbose\Capturing image.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high resolution image.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Photo High))\non-verbose\Capturing high res image.mp3",
			@"{TXT:>>voiceDir}\Applications\Capturing image.mp3",
			@"{TXT:>>voiceDir}\Applications\Recording high res image.mp3",
			@"{TXT:>>voiceDir}\Applications\Capturing high resolution image.mp3"
		};
		soundFileGroupList.Add("Screenshot", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Systems and Displays\Maps\System map.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Maps\Displaying map.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Completed.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Completed 2.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Complying.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\NMS\((RS - Galaxy Map On))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\NMS\((RS - Galaxy Map On))\Verbose"
		};
		soundFileGroupList.Add("Show Map", fileList);
		soundDirGroupList.Add("Show Map", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto Off))\Verbose\Autopilot disengaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Autopilot disengaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Auto pilot disengaged.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Affirmative.mp3",
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose"
		};
		soundFileGroupList.Add("Autopilot Disengaged", fileList);
		soundDirGroupList.Add("Autopilot Disengaged", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Acknowledgements\Affirmative.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Confirmed.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\You want moi to helm controls.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\You want moi to helm controls 2.mp3",
			@"{TXT:>>voiceDir}\Additional Dialogue\You want moi to helm controls 3.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Oh my god! I’m getting to drive the ship.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it.mp3",
			@"{TXT:>>voiceDir}\Additional dialogue\Oh my god! I’m getting to drive the ship don’t worry I won’t crash it 2.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Take Off-Docking\((RS - Landing Auto On))\Verbose\Autopilot engaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Autopilot engaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Auto pilot engaged.mp3"
		};
		soundFileGroupList.Add("Autopilot", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Acknowledgements\Executing now.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Getting us out of here.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Hyperspace.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Hyperspace jump.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Hyperspace jump 2.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Hyperspace jump engaging.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\NMS\((RS - Pulse Engine Engage))\non-verbose",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Power Boost Engines))\non-verbose",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Power Boost Engines))\Verbose"
		};
		soundFileGroupList.Add("Hyperspace", fileList);
		soundDirGroupList.Add("Hyperspace", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Cruise enabled.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Cruise engaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Cruise mode enabled.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives\Cruise mode engaged.mp3"
		};
		soundFileGroupList.Add("Cruise Control", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Thrusters\Afterburners 2.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Thrusters\Afterburners engaged.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Thrusters\Afterburners.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - APS))\non-verbose",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - APS))\Verbose"
		};
		soundFileGroupList.Add("Propulsion Enhancer", fileList);
		soundDirGroupList.Add("Propulsion Enhancer", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Main Engines\((RS - Afterburner))\Verbose\Afterburners maxing engines.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Engines))\Verbose\All power to engines.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines full burn.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Full burn.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Full throttle.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives engaging .mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives engaging.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Thrusters\Afterburners maxing engines.mp3",
			@"{TXT:>>voiceDir}\Power Management\Max engines.mp3"
		};
		soundFileGroupList.Add("Engines Max", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Crew Commands\((RS - Power to Engines))\non-verbose\Power to engines.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Engaging Impulse engines.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Impulse engines.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives engaging .mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Drives engaging.mp3",
			@"{TXT:>>voiceDir}\Power Management\All power to engines.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Engines))\non-verbose\Powering engines.mp3"
		};
		soundFileGroupList.Add("Engines Full Power", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Cut engines stop here.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Cutting engines stopping here alt.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Cutting engines stopping here.mp3",
			@"{TXT:>>voiceDir}\Engines Thrusters and Drives\Engines\Cutting engines.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Main Engines\((RS - Full Stop))\Verbose"
		};
		soundFileGroupList.Add("Engines Full Stop", fileList);
		soundDirGroupList.Add("Engines Full Stop", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Effects\Error beep\Error beep.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Unable to comply.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose"
		};
		soundFileGroupList.Add("General Error", fileList);
		soundDirGroupList.Add("General Error", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Weapons\Next weapon.mp3",
			@"{TXT:>>voiceDir}\Weapons\Weapon changed.mp3",
			@"{TXT:>>voiceDir}\Weapons\Weapons deployed.mp3",
			@"{TXT:>>voiceDir}\Power Management\Powering weapons.mp3",
			@"{TXT:>>voiceDir}\Weapons\Arming weapons.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose"
		};
		soundFileGroupList.Add("Equip Weapon", fileList);
		soundDirGroupList.Add("Equip Weapon", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power 2 Systems))\Verbose\Loading configuration.mp3",
			@"{TXT:>>voiceDir}\Weapons\Configuring weapon group.mp3",
			@"{TXT:>>voiceDir}\Weapons\Configuring weapon groups.mp3",
			@"{TXT:>>voiceDir}\Power Management\Configuring weapon groups.mp3",
			@"{TXT:>>voiceDir}\Power Management\Configuring weapons.mp3"
		};
		soundFileGroupList.Add("Configure Weapon Group", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Take Off-Docking\((RS - Pre Launch 3))\non-verbose\Request complete.mp3",
			@"{TXT:>>voiceDir}\Role\Configuration has been applied.mp3",
			@"{TXT:>>voiceDir}\Role\Configuration has been changed.mp3"
		};
		soundFileGroupList.Add("Configuration Complete", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Nope sorry.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\That's a negative.mp3",
			@"{TXT:>>voiceDir}\Acknowledgements\Unable to comply.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\Unable to comply.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\non-verbose\That's a negative.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Unable to comply))\Verbose\Not an option.mp3"
		};
		soundFileGroupList.Add("Firing Error", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Stowing weapons.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting weapons.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Retract))\non-verbose\Retracting all weapons.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Weapons Off))\Verbose\Weapons offline.mp3",
			@"{TXT:>>voiceDir}\Weapons\Retracting armaments.mp3",
			@"{TXT:>>voiceDir}\Weapons\Retracting weapons.mp3",
			@"{TXT:>>voiceDir}\Weapons\Retract weapons.mp3",
			@"{TXT:>>voiceDir}\Weapons\Retracting all weapons.mp3",
			@"{TXT:>>voiceDir}\Weapons\Weapons retracted.mp3",
			@"{TXT:>>voiceDir}\Weapons\Weapons offline.mp3",
			@"{TXT:>>voiceDir}\Power Management\Weapons offline.mp3"
		};
		soundFileGroupList.Add("Cease Fire", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Weapons\Weapons hot.mp3",
			@"{TXT:>>voiceDir}\Targeting\Fire at will.mp3",
			@"{TXT:>>voiceDir}\Fighter\Firing volley.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3"
		};
		soundFileGroupList.Add("Firing Group Generic", fileList);
		soundDirGroupList.Add("Firing Group Generic", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 1.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 1 alt 2.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 1 alt.mp3"
		};
		soundFileGroupList.Add("Firing Group 1", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 2.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 2 alt.mp3"
		};
		soundFileGroupList.Add("Firing Group 2", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire group 3.mp3"
		};
		soundFileGroupList.Add("Firing Group 3", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Power Management\Configuring for attack.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\NMS\((RS - Strike Target))\non-verbose\Attacking.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Targeting\((RS - Target Previous Hostile))\Verbose\Target locked.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engage the target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Fighters\((RS - Eliminate the threat))\Verbose\Engaging target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\ED\Course Headings\((RS - CH setting course))\Verbose\Locking target and setting course.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\non-verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\non-verbose\Weapons free.mp3"
		};
		soundFileGroupList.Add("Firing Ambush", fileList);
		soundDirGroupList.Add("Firing Ambush", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Countermeasures.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasure.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Deploying countermeasures.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Chaff))\Verbose\Launching countermeasures.mp3",
			@"{TXT:>>voiceDir}\Weapons\Deploying countermeasure.mp3",
			@"{TXT:>>voiceDir}\Weapons\Launch countermeasures alt.mp3",
			@"{TXT:>>voiceDir}\Weapons\Launch countermeasures.mp3",
			@"{TXT:>>voiceDir}\Weapons\Countermeasures.mp3",
			@"{TXT:>>voiceDir}\Weapons\Launch countermeasure.mp3"
		};
		soundFileGroupList.Add("Firing Counter", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Launch Missile))\non-verbose\Firing missile.mp3",
			@"{TXT:>>voiceDir}\Weapons\Missile away alt.mp3",
			@"{TXT:>>voiceDir}\Weapons\Missiles away.mp3",
			@"{TXT:>>voiceDir}\Weapons\Missile launch.mp3",
			@"{TXT:>>voiceDir}\Weapons\Missile away.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire missile alt.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire missile.mp3",
			@"{TXT:>>voiceDir}\Weapons\Firing missile.mp3"
		};
		soundFileGroupList.Add("Firing Missile", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free engage the target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons free.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Combat\((RS - Hardpoints Deploy))\Verbose\Weapons hot.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Power Management\((RS - Power Weapons))\Verbose\Divert power to weapons happy hunting.mp3",
			@"{TXT:>>voiceDir}\Weapons\Fire torpedoes.mp3",
			@"{TXT:>>voiceDir}\Weapons\Firing torpedoes.mp3"
		};
		soundFileGroupList.Add("Firing Torpedo", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3",
			@"{TXT:>>voiceDir}\Weapons\Deploying proximity mine.mp3",
			@"{TXT:>>voiceDir}\Weapons\Deploying proximity mine alt.mp3"
		};
		soundFileGroupList.Add("Firing Mine", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Mining laser deployed.mp3",
			@"{TXT:>>voiceDir}\Systems and Displays\Mining laser deployed alt.mp3"
		};
		soundFileGroupList.Add("Firing Mining Laser", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\non-verbose\Launch.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Ship Functions\((RS - Probe launched))\Verbose\Launching probe.mp3",
			@"{TXT:>>voiceDir}\Wingman\Activating beacon now.mp3",
			@"{TXT:>>voiceDir}\Wingman\Activating the beacon now.mp3"
		};
		dirList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\SWS\((RS - Beacons))\Verbose",
			@"{TXT:>>voiceDir}\Profile Sounds\SWS\((RS - Beacons))\non-verbose"
		};
		soundFileGroupList.Add("Firing Beacon", fileList);
		soundDirGroupList.Add("Firing Beacon", dirList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\That's affirmative.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Acknowledgements\((RS - Acknowledgements))\Verbose\Complying.mp3",
			@"{TXT:>>voiceDir}\Configuration Commands\Retreating.mp3",
			@"{TXT:>>voiceDir}\Protocols\Executing escape protocol.mp3"
		};
		soundFileGroupList.Add("Firing Escape", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Targeting\Locking on to target.mp3",
			@"{TXT:>>voiceDir}\Targeting\Focusing target.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes this one.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Targeting\((RS - Target Highest Threat))\Verbose\Targeting now nobody likes him.mp3",
			@"{TXT:>>voiceDir}\Weapons\Flare.mp3",
			@"{TXT:>>voiceDir}\Weapons\Flare alt.mp3",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Targeting.mp3",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Next Enemy))\non-verbose\Locking target.mp3",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focused.mp3",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Target focus.mp3",
			@"{TXT:>>voiceDir}\Profile sounds\SWS\((RS - Target Next Ally))\non-verbose\Focus target.mp3"
		};
		soundFileGroupList.Add("Firing Highlight", fileList);

		fileList = new string[] {
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comm link open.mp3",
			@"{TXT:>>voiceDir}\Profile Sounds\Generic\Comms\((RS - Comms Open))\non-verbose\Comms open.mp3",
			@"{TXT:>>voiceDir}\Communications\Comms.mp3",
			@"{TXT:>>voiceDir}\Communications\Comms open.mp3"
		};
		soundFileGroupList.Add("Firing Hacking", fileList);



		soundFileGroupContents = "private Dictionary<string, string> getSynonymList() {\n";
		soundFileGroupContents += "\tDictionary<string, string> soundGroupSynonymList = new Dictionary<string, string>();\n";
		foreach(KeyValuePair<string, string> synonymPair in soundGroupSynonymList) {
			soundFileGroupContents += "\tsoundGroupSynonymList.Add(\"" + synonymPair.Key + "\", \"" + synonymPair.Value + "\");\n";
		}
		soundFileGroupContents += "\treturn soundGroupSynonymList;\n";
		soundFileGroupContents += "}\n\n";
		soundFileGroupContents += "private string[] getSoundFileGroupList() {\n";
		soundFileGroupContents += "\treturn new string[] {\n";

		batchFileContents = "@ECHO OFF\n";
		batchFileContents += @"SET vaSoundDir1=C:\ProgramFiles (x86)\VoiceAttack\Sounds" + "\n";
		batchFileContents += @"SET vaSoundDir2=C:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds" + "\n";
		batchFileContents += @"SET vaSoundDir3=D:\ProgramFiles (x86)\VoiceAttack\Sounds" + "\n";
		batchFileContents += @"SET vaSoundDir4=D:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds" + "\n";
		batchFileContents += @"SET vaSoundDir5=E:\ProgramFiles (x86)\VoiceAttack\Sounds" + "\n";
		batchFileContents += @"SET vaSoundDir6=E:\Program Files (x86)\Steam\steamapps\common\VoiceAttack\Sounds" + "\n";
		batchFileContents += "SET vaSoundDir=\"\"\n\n";

		batchFileContents += "FOR %%i IN (1 2 3 4 5 6) DO CALL :checkSoundDir \"%%vaSoundDir%%i%%\"\n";
		batchFileContents += "SET vaSoundDir=%vaSoundDir:\"=%\n";  // Removes double quotes from vaSoundDir
		batchFileContents += "IF \"%vaSoundDir%\" == \"\" (\n";
		batchFileContents += "  echo Unable to find VoiceAttack Sounds directory. Please refer to the documentation for instructions on configuring the path correctly.\n";
		batchFileContents += "  echo https://github.com/mtmosier/sf-i-voice-companion/tree/master/import\n";
		batchFileContents += "  pause\n";
		batchFileContents += "  @ECHO ON\n";
		batchFileContents += "  @exit /b\n";
		batchFileContents += ") else echo Found VA Sounds directory at %vaSoundDir%\n\n";

		batchFileContents += "SET companionsFound=Null\n\n";
		batchFileContents += "IF NOT EXIST \"%vaSoundDir%\" (\n  echo \"%vaSoundDir%\" not found\n  pause\n  @ECHO ON\n  @exit /b\n)\n\n";

		batchFileContents += "cd \"%vaSoundDir%\"\n";
		batchFileContents += "FOR /D %%G IN (hcspack-*) do ( IF NOT \"%%G\" == \"hcspack-SWSCUSTOM\" call :processDir %%G )\n\n";

		// Output the companions list
		batchFileContents += "SET companionsFound\n\n";

		batchFileContents += "pause\n";
		batchFileContents += "@ECHO ON\n";
		batchFileContents += "@exit /b\n\n\n";

		batchFileContents += ":strToLower\n";
		batchFileContents += "  FOR %%i IN (\"A=a\" \"B=b\" \"C=c\" \"D=d\" \"E=e\" \"F=f\" \"G=g\" \"H=h\" \"I=i\" \"J=j\" \"K=k\" \"L=l\" \"M=m\" \"N=n\" \"O=o\" \"P=p\" \"Q=q\" \"R=r\" \"S=s\" \"T=t\" \"U=u\" \"V=v\" \"W=w\" \"X=x\" \"Y=y\" \"Z=z\") DO CALL SET \"%1=%%%1:%%~i%%\"\n";
		batchFileContents += "  exit /b\n\n";

		batchFileContents += ":strToUpper\n";
		batchFileContents += "  FOR %%i IN (\"a=A\" \"b=B\" \"c=C\" \"d=D\" \"e=E\" \"f=F\" \"g=G\" \"h=H\" \"i=I\" \"j=J\" \"k=K\" \"l=L\" \"m=M\" \"n=N\" \"o=O\" \"p=P\" \"q=Q\" \"r=R\" \"s=S\" \"t=T\" \"u=U\" \"v=V\" \"w=W\" \"x=X\" \"y=Y\" \"z=Z\") DO CALL SET \"%1=%%%1:%%~i%%\"\n";
		batchFileContents += "  exit /b\n\n";

		batchFileContents += ":ucFirst\n";
		batchFileContents += "  CALL SET \"str=%%%1%%\"\n";
		batchFileContents += "  CALL :strToLower str\n";
		batchFileContents += "  SET first=%str:~0,1%\n";
		batchFileContents += "  CALL :strToUpper first\n";
		batchFileContents += "  CALL SET \"%1=%first%%str:~1%\"\n";
		batchFileContents += "  exit /b\n\n";

		batchFileContents += ":checkSoundDir\n";
		batchFileContents += "  IF %vaSoundDir% == \"\" (\n";
		batchFileContents += "    IF EXIST %1 SET vaSoundDir=%1\n";
		batchFileContents += "  )\n";
		batchFileContents += "  exit /b\n\n";

		int j = 1;
		batchFileContents += ":processDir\n";
		batchFileContents += "  SET curDir=%1\n";
		batchFileContents += "  SET /A MISSING_COUNT=0\n";
		batchFileContents += "  SET companionName=%curDir:hcspack-=%\n";
		batchFileContents += "  CALL :ucFirst companionName\n";
		batchFileContents += "  SET newDir=sf-i_%companionName%\n";
		batchFileContents += "  ECHO %curDir% copying to %newDir%\n";
		batchFileContents += "  SET companionsFound=%companionsFound%;%companionName%\n";

		batchFileContents += "  IF NOT EXIST \"%vaSoundDir%\\%newDir%\\\" mkdir \"%vaSoundDir%\\%newDir%\"\n";

		int i = 1;
		foreach(KeyValuePair<string, string[]> soundGroup in soundFileGroupList) {
			soundFileGroupContents += "\t\t\"" + soundGroup.Key + "\",\n";

			batchFileContents += "  IF NOT EXIST \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\\" mkdir \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\"\n";
			if (soundDirGroupList.ContainsKey(soundGroup.Key)) {
				foreach (string dirPath in soundDirGroupList[soundGroup.Key]) {
					sourceDirPath = dirPath.Replace("{TXT:>>voiceDir}\\", "%vaSoundDir%\\%curDir%\\");
					batchFileContents += "  IF EXIST \"" + sourceDirPath + "\" (\n";
					batchFileContents += "    Xcopy /E /Y /Q \"" + sourceDirPath + "\" \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\\" > NUL\n";
					batchFileContents += "  )\n";
				}
			}
			foreach (string filePath in soundGroup.Value) {
				sourceFilePath = filePath.Replace("{TXT:>>voiceDir}\\", "%vaSoundDir%\\%curDir%\\");
				destFileName = Path.GetFileName(sourceFilePath);

				batchFileContents += "  IF EXIST \"" + sourceFilePath + "\" (\n";
				batchFileContents += "    copy \"" + sourceFilePath + "\" \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\" + destFileName + "\" >nul\n";
				batchFileContents += "  )\n";
			}

			if (soundBackupDirGroupList.ContainsKey(soundGroup.Key)) {
				batchFileContents += "  >nul 2>nul dir /a-d \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\*\" || (\n";
				foreach (string dirPath in soundBackupDirGroupList[soundGroup.Key]) {
					sourceDirPath = dirPath.Replace("{TXT:>>voiceDir}\\", "%vaSoundDir%\\%curDir%\\");
					batchFileContents += "    IF EXIST \"" + sourceDirPath + "\" (\n";
					batchFileContents += "      Xcopy /E /Y /Q \"" + sourceDirPath + "\" \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\\" > NUL\n";
					batchFileContents += "    )\n";
				}
				batchFileContents += "  )\n";
			}

			if (!groupsToCopyFromNull.Contains(soundGroup.Key)) {
				// Source - https://stackoverflow.com/questions/10813943/check-if-any-type-of-files-exist-in-a-directory-using-batch-script
				batchFileContents += "  >nul 2>nul dir /a-d \"%vaSoundDir%\\%newDir%\\" + soundGroup.Key + "\\*\" || (\n";
				if (verbose) {
					batchFileContents += "    echo - Missing " + soundGroup.Key + "\n";
				} else {
					batchFileContents += "    SET /A MISSING_COUNT+=1\n";
				}
				batchFileContents += "  )\n";
			}
		}

		//*** Copy specified groups from Null to other companions
		batchFileContents += "  IF EXIST \"%vaSoundDir%\\sf-i_Null\\\" (\n";
		foreach (string gn in groupsToCopyFromNull) {
			batchFileContents += "    IF EXIST \"%vaSoundDir%\\sf-i_Null\\" + gn + "\\\" Xcopy /E /Y /Q \"%vaSoundDir%\\sf-i_Null\\" + gn + "\" \"%vaSoundDir%\\%newDir%\\" + gn + "\\\" > NUL\n";
		}
		batchFileContents += "  )\n";





		batchFileContents += "  IF %MISSING_COUNT% GTR 0 (\n";
		batchFileContents += "    echo - Missing %MISSING_COUNT% entries\n";
		batchFileContents += "  )\n";

		batchFileContents += "  exit /b\n\n";
		batchFileContents += "\n\n";

		writeCopyVoiceFilesContentsToFile(batchFileContents);


		soundFileGroupContents = soundFileGroupContents.Substring(0, soundFileGroupContents.Length - 2);
		soundFileGroupContents += "\n\t};\n";
		soundFileGroupContents += "}\n\n";

		writeSoundFileGroupContentsToFile(soundFileGroupContents);
	}
}
