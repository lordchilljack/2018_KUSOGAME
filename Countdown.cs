/// Kamikaze's Simple Countdown Timer v1.1 ////////

// Added Features:
// 1) Script now allows to display countdown timer on multiple LCDs as long as their names are all configured
// with the same name on this script under 'CONFIGURE TEXT/BLOCK NAMES HERE'.


// HOW TO SETUP COUNTDOWN TIMER:
// 1. PLACE A PROGRAM BLOCK
// 2. PLACE AN LCD/TEXT PANEL (NAME MUST MATCH WITH CONFIG BELOW)
// 3. PLACE BUTTON PANEL AND SETUP BUTTONS 1-4 TO RUN PROGRAM BLOCK WITH ARGUMENTS LISTED:
//     Start
//     Hour
//     Minute
//     Reset
// 4. IF YOU WANT AN ACTION TO HAPPEN AFTER TIME HAS EXPIRED, ADD A TIMER BLOCK (NAME MUST MATCH WITH CONFIG BELOW)
//    CONFIGURE TIMER BLOCK WITH WHATEVER ACTION YOU WANT TO HAPPEN WHEN TIME EXPIRES


// WHAT DOES EACH ARGUMENT DO???
// 	Start- Will start the countdown, press button again will stop the countdown
// 	Hour- Adds one hour to the timer (Can also add addition hours while timer is running)
// 	Minute- Adds 1 minute to the timer (Can also add addtion minutes while timer is running)
// 	Reset- If there is time present on the countdown, this will reset the time to 0:00:00
//         If timer is already at 0:00:00, this will reset timer to previous time set
//         If timer reaches 0:00:00, pressing this button will automatically revert time to previous setting


//////////////////////////////////////////////////////
////// CONFIGURE TEXT/BLOCK NAMES HERE ///////////////

string textpanelname = "ENTER TEXT/LCD BLOCK NAME HERE"; // CONFIGURE TEXT/LCD PANEL NAME HERE
string timerblock = "TIMER BLOCK NAME HERE"; // CONFIGURE TIMER BLOCK NAME *OPTIONAL TO USE
string timercount = "Remaining Time: "; // CONFIGURE TEXT SHOWN ON LCD
string textdisplayed = "Time Is Up!"; // CONFIGURE WHAT IS DISPLAYED AFTER TIMER REACHES 0







///////////////////////////////////////////////////////
/////// DO NOT ALTER CODE BELOW HERE /////////////////
/////////////////////////////////////////////////////
int timer = 0;
int tickcounter = 0;
int buttonpress = 0;
bool start = false;
bool scriptInit = false;

List<IMyProgrammableBlock> pbList = new List<IMyProgrammableBlock>();
List<IMyTextPanel> txList = new List<IMyTextPanel>();

void Main(string argument){

	IMyTimerBlock timerBlock = GridTerminalSystem.GetBlockWithName(timerblock) as IMyTimerBlock;
	GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(txList);
	bool foundBlock = false;
	
	foreach(var textpanel in txList){
		
		if(textpanel != null && textpanel.CustomName == (textpanelname)){
			
			foundBlock = true;
		}
	}
	
	if(foundBlock == false){
		
		Echo("No Text/LCD Panel Found. Or check text/lcd name.");
		return;
	}
	
	if(scriptInit == false){
		
		GridTerminalSystem.GetBlocksOfType<IMyProgrammableBlock>(pbList);
		scriptInit = true;
	}
	
	if(pbList.Count == 0){
					
		return;
	}
	
	var pb = pbList[0];
	
	if(argument == "Start" && timer > 0){
		
		start = true;
		buttonpress++;
	}

	if(argument == "Start" && buttonpress == 2){
		
		start = false;
		buttonpress = 0;
	}
	
	
	if(argument == "Hour"){
		
		timer += 3600;
		pb.CustomData = timer.ToString() + "\n";
		
		foreach(var textpanel in txList){
		
			if(textpanel != null && textpanel.CustomName == (textpanelname)){
			
				textpanel.WritePublicText(timercount + TimeSpan.FromSeconds(timer).ToString());
			}
		}
	}
	
	if(argument == "Minute"){
		
		timer += 60;
		pb.CustomData = timer.ToString() + "\n";
		
		foreach(var textpanel in txList){
		
			if(textpanel != null && textpanel.CustomName == (textpanelname)){
				
				textpanel.WritePublicText(timercount + TimeSpan.FromSeconds(timer).ToString());
			}
		}
	}
	
	if(argument == "Reset" && timer <= 0){
		
		string [] data_split = pb.CustomData.Split('\n');
		Int32.TryParse(data_split[0], out timer);
		
		foreach(var textpanel in txList){
		
			if(textpanel != null && textpanel.CustomName == (textpanelname)){
				
				textpanel.WritePublicText(timercount + TimeSpan.FromSeconds(timer).ToString());				
			}
		}
		
		return;
	}
	
	if(argument == "Reset" && timer > 0){
		
		timer = 0;
		start = false;
		buttonpress = 0;
		
		foreach(var textpanel in txList){
		
			if(textpanel != null && textpanel.CustomName == (textpanelname)){
				
				textpanel.WritePublicText(timercount + TimeSpan.FromSeconds(timer).ToString());
			}
		}
		
		return;
	}	
	
	if(start == true && timer > 0){

		Runtime.UpdateFrequency = UpdateFrequency.Update1;	
		int delayinseconds = 1;
		tickcounter++;
		
			if(tickcounter >= delayinseconds * 60){
				
				timer -= 1;
				tickcounter = 0;
				
				foreach(var textpanel in txList){
		
					if(textpanel != null && textpanel.CustomName == (textpanelname)){
						
						textpanel.WritePublicText(timercount + TimeSpan.FromSeconds(timer).ToString());
					}
				}
			}
	}
	
	if(start == true && timer <= 0){
		
		foreach(var textpanel in txList){
		
			if(textpanel != null && textpanel.CustomName == (textpanelname)){
				
				textpanel.WritePublicText(textdisplayed);
			}
		}
		
		start = false;
		buttonpress = 0;
		
		if(timerBlock != null && timerBlock.CustomName == (timerblock)){
			
			timerBlock.ApplyAction("TriggerNow");
		}
		
		return;
	} 	
}